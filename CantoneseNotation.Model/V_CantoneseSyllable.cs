﻿namespace CantoneseNotation.Model
{
    public class V_CantoneseSyllable : V_SyllableBase
    {
        public string Consonant_GP { get; set; }
        public string Vowel_GP { get; set; }
        public string Syllable_GP { get; set; }
        public bool UseThisIfMultiple { get; set; }
        public bool Inclusive { get; set; }
        public bool Fixed { get; set; }
        public int? MandarinSyllableId { get; set; }
        public bool? HasSpecial { get; set; }
    }
}
