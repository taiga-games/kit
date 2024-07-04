using System.Text.RegularExpressions;

namespace TaigaGames.Kit
{
    public class StringUtils
    {
        public static string ToCamelCase(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            str = Regex.Replace(str, @"(\W+)(\w?)", m => m.Groups[2].Value.ToUpper(), RegexOptions.Compiled);
            return char.ToLowerInvariant(str[0]) + str[1..];
        }
    }
}