using System;
using System.Collections.Generic;
using UnityEngine;

namespace TaigaGames.Kit.Localization
{
    public static class LocalizationUtils
    {
        public static string Localize(LocalizableString localizableString)
        {
            return localizableString != null ? localizableString.Localize() ?? "<null>" : "<null>";
        }
        
        public static SystemLanguage GetSystemLanguageByCode(string code)
        {
            return code switch
            {
                "en" => SystemLanguage.English,
                "ru" => SystemLanguage.Russian,
                "cs" => SystemLanguage.Czech,
                "da" => SystemLanguage.Danish,
                "nl" => SystemLanguage.Dutch,
                "de" => SystemLanguage.German,
                "el" => SystemLanguage.Greek,
                "fi" => SystemLanguage.Finnish,
                "fr" => SystemLanguage.French,
                "it" => SystemLanguage.Italian,
                "ja" => SystemLanguage.Japanese,
                "ko" => SystemLanguage.Korean,
                "nb" => SystemLanguage.Norwegian,
                "nn" => SystemLanguage.Norwegian,
                "no" => SystemLanguage.Norwegian,
                "pl" => SystemLanguage.Polish,
                "pt" => SystemLanguage.Portuguese,
                "ro" => SystemLanguage.Romanian,
                "es" => SystemLanguage.Spanish,
                "sv" => SystemLanguage.Swedish,
                "tr" => SystemLanguage.Turkish,
                "zh-Hans" => SystemLanguage.ChineseSimplified,
                "zh-CN" => SystemLanguage.ChineseSimplified,
                "zh-Hant" => SystemLanguage.ChineseTraditional,
                "zh-TW" => SystemLanguage.ChineseTraditional,
                _ => SystemLanguage.Unknown
            };
        }

        public static LocalizableString[] ParseTsv(string tsv)
        {
            var lines = tsv.Split(new []{'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);

            var keys = lines[0].Split("\t", StringSplitOptions.RemoveEmptyEntries);
            var languages = new Dictionary<int, SystemLanguage>();
            for (int i = 1; i < keys.Length; i++)
            {
                var systemLanguage = GetSystemLanguageByCode(keys[i]);
                if (systemLanguage == SystemLanguage.Unknown)
                    throw new Exception($"Unknown language code: \"{keys[i]}\"");
                languages.Add(i, systemLanguage);
            }

            var strings = new LocalizableString[lines.Length - 1];
            var reusableLocalizationValues = new Dictionary<SystemLanguage, string>();

            for (var i = 1; i < lines.Length; ++i)
            {
                var values = lines[i].Split("\t", StringSplitOptions.RemoveEmptyEntries);

                if (values.Length == 0)
                    continue;

                reusableLocalizationValues.Clear();
                for (var j = 1; j < values.Length; j++) 
                    reusableLocalizationValues.Add(languages[j], values[j]);

                var localizationString = ScriptableObject.CreateInstance<LocalizableString>();
                localizationString.name = values[0];
                
                localizationString.English = reusableLocalizationValues[SystemLanguage.English];
                localizationString.Russian = reusableLocalizationValues[SystemLanguage.Russian];
                localizationString.Czech = reusableLocalizationValues[SystemLanguage.Czech];
                localizationString.Danish = reusableLocalizationValues[SystemLanguage.Danish];
                localizationString.Dutch = reusableLocalizationValues[SystemLanguage.Dutch];
                localizationString.German = reusableLocalizationValues[SystemLanguage.German];
                localizationString.Greek = reusableLocalizationValues[SystemLanguage.Greek];
                localizationString.Finnish = reusableLocalizationValues[SystemLanguage.Finnish];
                localizationString.French = reusableLocalizationValues[SystemLanguage.French];
                localizationString.Italian = reusableLocalizationValues[SystemLanguage.Italian];
                localizationString.Japanese = reusableLocalizationValues[SystemLanguage.Japanese];
                localizationString.Korean = reusableLocalizationValues[SystemLanguage.Korean];
                localizationString.Norwegian = reusableLocalizationValues[SystemLanguage.Norwegian];
                localizationString.Polish = reusableLocalizationValues[SystemLanguage.Polish];
                localizationString.Portuguese = reusableLocalizationValues[SystemLanguage.Portuguese];
                localizationString.Romanian = reusableLocalizationValues[SystemLanguage.Romanian];
                localizationString.Spanish = reusableLocalizationValues[SystemLanguage.Spanish];
                localizationString.Swedish = reusableLocalizationValues[SystemLanguage.Swedish];
                localizationString.Turkish = reusableLocalizationValues[SystemLanguage.Turkish];
                localizationString.SimplifiedChinese = reusableLocalizationValues[SystemLanguage.ChineseSimplified];
                localizationString.TraditionalChinese = reusableLocalizationValues[SystemLanguage.ChineseTraditional];

                strings[i - 1] = localizationString;
            }

            return strings;
        }
    }
}