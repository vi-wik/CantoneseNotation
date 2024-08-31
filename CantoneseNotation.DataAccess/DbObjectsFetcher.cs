using CantoneseNotation.Model;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CantoneseNotation.DataAccess
{
    public class DbObjectsFetcher
    {

        public static async Task<(IEnumerable<V_CantoneseSyllable> CantoneseSyllables, IEnumerable<V_MandarinSyllable> MandarinSyllables)> GetSyllables(string content)
        {
            var words = content.Select(item => item.ToString()).ToArray();      
            string wordsIn = string.Join(',', words.Select(item => $"'{DbUtitlity.GetSafeValue(item)}'"));

            string cantoneseSql = $"select * from v_CantoneseSyllables where Word in({wordsIn}) and Hidden=0";
            string mandarinSql = $"select * from v_MandarinSyllables where Word in({wordsIn})";          

            using (var connection = DbUtitlity.CreateDbConnection())
            {
                var cantoneseSyllables = (await connection.QueryAsync<V_CantoneseSyllable>(cantoneseSql));
                var mandarinSyllables = (await connection.QueryAsync<V_MandarinSyllable>(mandarinSql));

                return (cantoneseSyllables, mandarinSyllables);
            }           
        }       

        public static async Task<IEnumerable<MandarinVowel>> GetMandarinVowels()
        {
            string sql = "select * from MandarinVowel";

            using (var connection = DbUtitlity.CreateDbConnection())
            {
                return (await connection.QueryAsync<MandarinVowel>(sql));
            }
        }      
    }
}
