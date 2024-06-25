#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace TaigaGames.Kit.Localization
{
    public class LocalizationUrlEditorWindow : EditorWindow
    {
        private string _urlToLocalizationTable;
        private string _pathToLocalizableObjects;

        private UnityWebRequest _unityWebRequest;

        [MenuItem("Tools/Localization URL")]
        private static void ShowWindow()
        {
            var window = GetWindow<LocalizationUrlEditorWindow>();
            window.titleContent = new GUIContent("Localization URL");
            window.Show();
        }

        private void OnEnable()
        {
            _urlToLocalizationTable = EditorPrefs.GetString($"{nameof(LocalizationUrlEditorWindow)}/{nameof(_urlToLocalizationTable)}", string.Empty);
            _pathToLocalizableObjects = EditorPrefs.GetString($"{nameof(LocalizationUrlEditorWindow)}/{nameof(_pathToLocalizableObjects)}", string.Empty);
            if (string.IsNullOrEmpty(_pathToLocalizableObjects))
            {
                _pathToLocalizableObjects = "Assets/Localization/Text";
            }
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("URL to localization table (tsv):");
            _urlToLocalizationTable = EditorGUILayout.TextField(_urlToLocalizationTable);

            if (!string.IsNullOrEmpty(_urlToLocalizationTable))
            {
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Folder to Localizable Text:");
                _pathToLocalizableObjects = EditorGUILayout.TextField(_pathToLocalizableObjects);

                EditorGUILayout.Space();
                if (GUILayout.Button("Load"))
                {
                    EditorPrefs.SetString($"{nameof(LocalizationUrlEditorWindow)}/{nameof(_urlToLocalizationTable)}", _urlToLocalizationTable);
                    EditorPrefs.SetString($"{nameof(LocalizationUrlEditorWindow)}/{nameof(_pathToLocalizableObjects)}", _pathToLocalizableObjects);

                    StartDownload();
                }
            }
        }

        private void StartDownload()
        {
            Debug.Log("Downloading localization table...");
            var url = _urlToLocalizationTable.Replace("edit", "export").Replace("#", "?") + "&format=tsv";
            _unityWebRequest = UnityWebRequest.Get(url);
            _unityWebRequest.SendWebRequest();
            EditorApplication.update += WaitForWebRequestResult;
        }

        private void WaitForWebRequestResult()
        {
            if (!_unityWebRequest.isDone)
            {
                if (!string.IsNullOrEmpty(_unityWebRequest.error))
                {
                    Debug.LogError(_unityWebRequest.error);
                    EditorApplication.update -= WaitForWebRequestResult;
                    return;
                }

                Debug.Log("Localization table downloaded");
                EditorApplication.update -= WaitForWebRequestResult;
                Localize(_unityWebRequest.downloadHandler.text);
                Release();
            }

            return;

            void Release()
            {
                _unityWebRequest.Dispose();
                _unityWebRequest = null;
            }
        }

        private void Localize(string text)
        {
            var localizableStrings = FindObjects<LocalizableString>();

            var lines = text.Split("\n", StringSplitOptions.RemoveEmptyEntries);

            var keys = lines[0].Split("\t", StringSplitOptions.RemoveEmptyEntries);
            var languages = new Dictionary<int, SystemLanguage>();
            for (int i = 1; i < keys.Length; i++) 
                languages.Add(i, LocalizationUtils.GetSystemLanguageByCode(keys[i].Replace("\n", "").Trim()));

            for (int i = 1; i < lines.Length; ++i)
            {
                var values = lines[i].Split("\t", StringSplitOptions.RemoveEmptyEntries);

                if (values.Length == 0)
                {
                    continue;
                }

                var assetName = values[0];

                var assetLanguages = new Dictionary<SystemLanguage, string>();
                for (int j = 1; j < values.Length; j++)
                {
                    var languageValue = values[j].Trim();
                    assetLanguages.Add(languages[j], languageValue);
                }

                var localizationString = localizableStrings.FirstOrDefault(x => x.name == assetName);
                if (!localizationString)
                {
                    localizationString = CreateInstance<LocalizableString>();

                    if (!Directory.Exists(_pathToLocalizableObjects))
                        Directory.CreateDirectory(_pathToLocalizableObjects);

                    AssetDatabase.CreateAsset(localizationString, Path.Combine(_pathToLocalizableObjects, assetName + ".asset"));
                }

                var serializedObject = new SerializedObject(localizationString);
                var english = serializedObject.FindProperty("English");
                var russian = serializedObject.FindProperty("Russian");
                var czech = serializedObject.FindProperty("Czech");
                var danish = serializedObject.FindProperty("Danish");
                var dutch = serializedObject.FindProperty("Dutch");
                var german = serializedObject.FindProperty("German");
                var greek = serializedObject.FindProperty("Greek");
                var finnish = serializedObject.FindProperty("Finnish");
                var french = serializedObject.FindProperty("French");
                var italian = serializedObject.FindProperty("Italian");
                var japanese = serializedObject.FindProperty("Japanese");
                var korean = serializedObject.FindProperty("Korean");
                var norwegian = serializedObject.FindProperty("Norwegian");
                var polish = serializedObject.FindProperty("Polish");
                var portuguese = serializedObject.FindProperty("Portuguese");
                var romanian = serializedObject.FindProperty("Romanian");
                var spanish = serializedObject.FindProperty("Spanish");
                var swedish = serializedObject.FindProperty("Swedish");
                var turkish = serializedObject.FindProperty("Turkish");
                var simplifiedChinese = serializedObject.FindProperty("SimplifiedChinese");
                var traditionalChinese = serializedObject.FindProperty("TraditionalChinese");

                if (assetLanguages.TryGetValue(SystemLanguage.English, out var englishValue))
                    english.stringValue = englishValue;
                else
                    Debug.LogError($"No value for key {assetName}: {SystemLanguage.English}");

                if (assetLanguages.TryGetValue(SystemLanguage.Russian, out var russianValue))
                    russian.stringValue = russianValue;
                else
                    Debug.LogError($"No value for key {assetName}: {SystemLanguage.Russian}");

                if (assetLanguages.TryGetValue(SystemLanguage.Czech, out var czechValue))
                    czech.stringValue = czechValue;
                else
                    Debug.LogError($"No value for key {assetName}: {SystemLanguage.Czech}");

                if (assetLanguages.TryGetValue(SystemLanguage.Danish, out var danishValue))
                    danish.stringValue = danishValue;
                else
                    Debug.LogError($"No value for key {assetName}: {SystemLanguage.Danish}");

                if (assetLanguages.TryGetValue(SystemLanguage.Dutch, out var dutchValue))
                    dutch.stringValue = dutchValue;
                else
                    Debug.LogError($"No value for key {assetName}: {SystemLanguage.Dutch}");

                if (assetLanguages.TryGetValue(SystemLanguage.German, out var germanValue))
                    german.stringValue = germanValue;
                else
                    Debug.LogError($"No value for key {assetName}: {SystemLanguage.German}");

                if (assetLanguages.TryGetValue(SystemLanguage.Greek, out var greekValue))
                    greek.stringValue = greekValue;
                else
                    Debug.LogError($"No value for key {assetName}: {SystemLanguage.Greek}");
                
                if (assetLanguages.TryGetValue(SystemLanguage.Finnish, out var finnishValue))
                    finnish.stringValue = finnishValue;
                else
                    Debug.LogError($"No value for key {assetName}: {SystemLanguage.Finnish}");
                
                if (assetLanguages.TryGetValue(SystemLanguage.French, out var frenchValue))
                    french.stringValue = frenchValue;
                else
                    Debug.LogError($"No value for key {assetName}: {SystemLanguage.French}");
                
                if (assetLanguages.TryGetValue(SystemLanguage.Italian, out var italianValue))
                    italian.stringValue = italianValue;
                else
                    Debug.LogError($"No value for key {assetName}: {SystemLanguage.Italian}");
                
                if (assetLanguages.TryGetValue(SystemLanguage.Japanese, out var japaneseValue))
                    japanese.stringValue = japaneseValue;
                else
                    Debug.LogError($"No value for key {assetName}: {SystemLanguage.Japanese}");
                
                if (assetLanguages.TryGetValue(SystemLanguage.Korean, out var koreanValue))
                    korean.stringValue = koreanValue;
                else
                    Debug.LogError($"No value for key {assetName}: {SystemLanguage.Korean}");
                
                if (assetLanguages.TryGetValue(SystemLanguage.Norwegian, out var norwegianValue))
                    norwegian.stringValue = norwegianValue;
                else
                    Debug.LogError($"No value for key {assetName}: {SystemLanguage.Norwegian}");
                
                if (assetLanguages.TryGetValue(SystemLanguage.Polish, out var polishValue))
                    polish.stringValue = polishValue;
                else
                    Debug.LogError($"No value for key {assetName}: {SystemLanguage.Polish}");
                
                if (assetLanguages.TryGetValue(SystemLanguage.Portuguese, out var portugueseValue))
                    portuguese.stringValue = portugueseValue;
                else
                    Debug.LogError($"No value for key {assetName}: {SystemLanguage.Portuguese}");
                
                if (assetLanguages.TryGetValue(SystemLanguage.Romanian, out var romanianValue))
                    romanian.stringValue = romanianValue;
                else
                    Debug.LogError($"No value for key {assetName}: {SystemLanguage.Romanian}");
                
                if (assetLanguages.TryGetValue(SystemLanguage.Spanish, out var spanishValue))
                    spanish.stringValue = spanishValue;
                else
                    Debug.LogError($"No value for key {assetName}: {SystemLanguage.Spanish}");
                
                if (assetLanguages.TryGetValue(SystemLanguage.Swedish, out var swedishValue))
                    swedish.stringValue = swedishValue;
                else
                    Debug.LogError($"No value for key {assetName}: {SystemLanguage.Swedish}");
                
                if (assetLanguages.TryGetValue(SystemLanguage.Turkish, out var turkishValue))
                    turkish.stringValue = turkishValue;
                else
                    Debug.LogError($"No value for key {assetName}: {SystemLanguage.Turkish}");
                
                if (assetLanguages.TryGetValue(SystemLanguage.ChineseSimplified, out var simplifiedChineseValue))
                    simplifiedChinese.stringValue = simplifiedChineseValue;
                else
                    Debug.LogError($"No value for key {assetName}: {SystemLanguage.ChineseSimplified}");
                
                if (assetLanguages.TryGetValue(SystemLanguage.ChineseTraditional, out var traditionalChineseValue))
                    traditionalChinese.stringValue = traditionalChineseValue;
                else
                    Debug.LogError($"No value for key {assetName}: {SystemLanguage.ChineseTraditional}");

                serializedObject.ApplyModifiedPropertiesWithoutUndo();

                EditorUtility.SetDirty(localizationString);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static T[] FindObjects<T>(string folder = null) where T : UnityEngine.Object
        {
            var assetGuids = string.IsNullOrEmpty(folder)
                ? AssetDatabase.FindAssets("t:" + typeof(T).Name)
                : AssetDatabase.FindAssets("t:" + typeof(T).Name, new[] { folder });

            var assets = new T[assetGuids.Length];
            for (var i = 0; i < assetGuids.Length; i++)
            {
                var assetGuid = assetGuids[i];
                assets[i] = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(assetGuid));
            }

            return assets;
        }
    }
}
#endif