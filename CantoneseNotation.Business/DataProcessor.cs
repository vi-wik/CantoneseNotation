﻿using CantoneseNotation.Business.Model;
using CantoneseNotation.DataAccess;
using CantoneseNotation.Model;
using System.Text.RegularExpressions;

namespace CantoneseNotation.Business
{
    public class DataProcessor
    {
        internal static IEnumerable<MandarinVowel> MandarinVowels { get; private set; }

        static DataProcessor()
        {
            DbUtitlity.DataFilePath = DataFileManager.DataFilePath;

            Init();
        }

        static async void Init()
        {
            if (MandarinVowels == null)
            {
                MandarinVowels = await GetMandarinVowels();
            }
        }

        public static async Task<IEnumerable<SyllableDisplay>> GetSyllables(string content)
        {
            var words = content.Select(item => item.ToString()).ToArray();
            int wordsCount = words.Length;
            bool isMutiple = wordsCount > 1;

            List<SyllableDisplay> syllables = new List<SyllableDisplay>();

            Action<string, V_CantoneseSyllable, IEnumerable<V_MandarinSyllable>, int> addSyllable = (matchedExample, cantoneseSyllable, mandarinWordSyllables, originalWordIndex) =>
            {
                V_MandarinSyllable mandarinSyllable = null;

                int? mandarinSyllableId = cantoneseSyllable.MandarinSyllableId;
                bool? hasSpecial = cantoneseSyllable.HasSpecial;

                if (hasSpecial.HasValue && !string.IsNullOrEmpty(matchedExample))
                {
                    var dictExamples = GetExamplesDictionary(mandarinWordSyllables);

                    foreach (var dictExample in dictExamples)
                    {
                        if (dictExample.Value.Contains(matchedExample))
                        {
                            mandarinSyllableId = dictExample.Key;
                            break;
                        }
                    }
                }

                if (mandarinSyllableId.HasValue)
                {
                    mandarinSyllable = mandarinWordSyllables.FirstOrDefault(item => item.Id == mandarinSyllableId.Value);
                }

                Model.SyllableDisplay syllable = GetSyllableDisplay(cantoneseSyllable, mandarinSyllable);
                syllable.OriginalWordIndex = originalWordIndex;

                syllables.Add(syllable);
            };

            var result = await DbObjectsFetcher.GetSyllables(content);
            var cantoneseSyllables = result.CantoneseSyllables;
            var mandarinSyllables = result.MandarinSyllables;

            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];

                var cantoneseWordSyllables = cantoneseSyllables.Where(item => item.Word == word);
                var mandarinWordSyllables = mandarinSyllables.Where(item => item.Word == word);

                int cantoneseSyllableCount = cantoneseWordSyllables.Count();

                int? cantoneseSyllableId = null;
                V_CantoneseSyllable cantoneseSyllable = null;
                int? mandarinSyllableId = null;

                string matchedExample = GetMatchedExample(content, i, cantoneseWordSyllables, out cantoneseSyllableId);

                bool noRelation = false;

                if (string.IsNullOrEmpty(matchedExample))
                {
                    matchedExample = GetMatchedExample(content, i, mandarinWordSyllables, out mandarinSyllableId);

                    if (mandarinSyllableId.HasValue && !cantoneseSyllableId.HasValue)
                    {
                        cantoneseSyllableId = cantoneseWordSyllables.FirstOrDefault(item => item.MandarinSyllableId == mandarinSyllableId)?.Id;

                        if (!cantoneseSyllableId.HasValue)
                        {
                            int noRelationCount = cantoneseWordSyllables.Count(item => item.MandarinSyllableId == null);

                            if (noRelationCount == 0)
                            {
                                if (!cantoneseWordSyllables.Any(item => item.UseThisIfMultiple))
                                {
                                    noRelation = true;
                                }
                            }
                        }
                    }
                }

                if (cantoneseSyllableCount == 1)
                {
                    var firstCantoneseSyllable = cantoneseWordSyllables.First();

                    if (firstCantoneseSyllable.Inclusive)
                    {
                        cantoneseSyllableId = firstCantoneseSyllable.Id;
                        noRelation = false;
                    }
                }

                if (!noRelation)
                {
                    if (!cantoneseSyllableId.HasValue)
                    {
                        if (cantoneseSyllableCount == 1)
                        {
                            cantoneseSyllableId = cantoneseWordSyllables.FirstOrDefault()?.Id;
                        }
                    }

                    if (cantoneseSyllableId.HasValue)
                    {
                        cantoneseSyllable = cantoneseWordSyllables.FirstOrDefault(item => item.Id == cantoneseSyllableId);
                    }
                    else
                    {
                        if (isMutiple && cantoneseWordSyllables.Any(item => item.UseThisIfMultiple))
                        {
                            cantoneseSyllable = cantoneseWordSyllables.FirstOrDefault(item => item.UseThisIfMultiple);
                        }
                    }

                    if (cantoneseSyllable == null)
                    {
                        var notFixedSyllables = cantoneseWordSyllables.Where(item => item.Fixed == false);
                        var defaultSyllable = cantoneseWordSyllables.FirstOrDefault(item => item.IsDefault == true);

                        if (isMutiple && notFixedSyllables.Count() == 1)
                        {
                            cantoneseSyllable = notFixedSyllables.FirstOrDefault();
                        }
                        else if (isMutiple && defaultSyllable != null && string.IsNullOrEmpty(defaultSyllable.Examples))
                        {
                            cantoneseSyllable = defaultSyllable;
                        }
                        else
                        {
                            foreach (var cs in cantoneseWordSyllables)
                            {
                                addSyllable(matchedExample, cs, mandarinWordSyllables, i);
                            }
                        }
                    }

                    if (cantoneseSyllable != null)
                    {
                        addSyllable(matchedExample, cantoneseSyllable, mandarinWordSyllables, i);
                    }
                }
                else
                {
                    if (mandarinSyllableId.HasValue)
                    {
                        V_MandarinSyllable mandarinSyllable = mandarinWordSyllables.FirstOrDefault(item => item.Id == mandarinSyllableId);

                        Model.SyllableDisplay syllable = GetSyllableDisplay(null, mandarinSyllable);
                        syllable.OriginalWordIndex = i;

                        syllables.Add(syllable);
                    }
                }
            }

            return syllables;
        }

        private static string GetMatchedExample(string content, int wordIndex,
                              IEnumerable<V_SyllableBase> wordSyllables, out int? syllableId)
        {
            syllableId = null;
            string matchedEample = null;

            var dictExamples = GetExamplesDictionary(wordSyllables.OrderBy(item => item.Priority));

            //完全匹配
            foreach (var dictExample in dictExamples)
            {
                var examples = dictExample.Value;

                foreach (string example in examples)
                {
                    if (content.Trim() == example)
                    {
                        syllableId = dictExample.Key;
                        matchedEample = example;
                        break;
                    }
                }
            }

            //模糊匹配
            if (!syllableId.HasValue)
            {
                foreach (var dictExample in dictExamples)
                {
                    var examples = dictExample.Value;

                    foreach (string example in examples)
                    {
                        Regex regex = new Regex(example);

                        var matches = regex.Matches(content);

                        foreach (Match match in matches)
                        {
                            int startIndex = match.Index;
                            int stopIndex = startIndex + match.Length;

                            if (wordIndex >= startIndex && wordIndex <= stopIndex)
                            {
                                syllableId = dictExample.Key;
                                matchedEample = match.Value;
                                break;
                            }
                        }

                        if (syllableId.HasValue)
                        {
                            break;
                        }
                    }
                }
            }

            return matchedEample;
        }

        private static Model.SyllableDisplay GetSyllableDisplay(V_CantoneseSyllable cantoneseSyllable, V_MandarinSyllable mandarinSyllable)
        {
            Model.SyllableDisplay syllableDisplay = new Model.SyllableDisplay()
            {
                Word = cantoneseSyllable?.Word ?? mandarinSyllable?.Word,
                Consonant = cantoneseSyllable?.Consonant,
                Medial = cantoneseSyllable?.Medial,
                Vowel = cantoneseSyllable?.Vowel,
                Tone = cantoneseSyllable == null ? 0 : cantoneseSyllable.Tone,
                Consonant_GP = cantoneseSyllable?.Consonant_GP,
                Vowel_GP = cantoneseSyllable?.Vowel_GP,
                Syllable = cantoneseSyllable?.Syllable,
                Syllable_GP = cantoneseSyllable?.Syllable_GP,
                Syllable_M = mandarinSyllable?.Syllable,
                Tone_M = mandarinSyllable?.Tone,
                Priority = cantoneseSyllable?.Priority ?? 0,
                IsDefault = cantoneseSyllable?.IsDefault ?? false,
                Syllable_M_Full_ToneMark = GetMandarinSyllableToneMarkDisplay(mandarinSyllable)
            };

            return syllableDisplay;
        }

        private static string GetMandarinSyllableToneMarkDisplay(V_MandarinSyllable mandarinSyllable)
        {
            if (mandarinSyllable == null)
            {
                return string.Empty;
            }

            return GetMandarinSyllableToneMarkDisplay(mandarinSyllable.Consonant, mandarinSyllable.Medial, mandarinSyllable.Vowel, mandarinSyllable.Tone);
        }

        private static string GetMandarinSyllableToneMarkDisplay(string consonant, string medial, string vowel, int tone)
        {
            string prefix = string.Concat(consonant ?? "", medial ?? "");
            var toneMarkRecord = MandarinVowels.Where(item => item.Vowel == vowel).FirstOrDefault();
            string toneMark = string.Empty;

            switch (tone)
            {
                case 1:
                    toneMark = toneMarkRecord?.ToneMark1;
                    break;
                case 2:
                    toneMark = toneMarkRecord?.ToneMark2;
                    break;
                case 3:
                    toneMark = toneMarkRecord?.ToneMark3;
                    break;
                case 4:
                    toneMark = toneMarkRecord?.ToneMark4;
                    break;
                default:
                    toneMark = vowel ?? "";
                    break;
            }

            return string.Concat(prefix, toneMark ?? "");
        }

        private static Dictionary<int, List<string>> GetExamplesDictionary(IEnumerable<V_SyllableBase> syllables)
        {
            Dictionary<int, List<string>> examples = new Dictionary<int, List<string>>();

            foreach (var syllable in syllables)
            {
                if (!string.IsNullOrEmpty(syllable.Examples))
                {
                    examples.Add(syllable.Id, GetExampleList(syllable.Examples));
                }
            }

            return examples;
        }

        private static List<string> GetExampleList(string examples)
        {
            if (!string.IsNullOrEmpty(examples))
            {
                return examples.Split(new char[] { ',', '，' }).Select(item => item.Trim()).ToList();
            }

            return new List<string>();
        }        

        public static async Task<IEnumerable<MandarinVowel>> GetMandarinVowels()
        {
            return await DbObjectsFetcher.GetMandarinVowels();
        }        
    }
}