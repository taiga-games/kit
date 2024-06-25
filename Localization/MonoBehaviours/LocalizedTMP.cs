using TMPro;
using UnityEngine;

namespace TaigaGames.Kit.Localization
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizedTMP : LocalizedObject
    {
        [SerializeField] private LocalizableString _localizableString;
        [SerializeField] private TMP_Text _label;
        [SerializeField] private string _stringFormat = "";
        
        private void Reset()
        {
            _label = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            if (LocalizationManager.Instance)
            {
                Localize();
            }
        }
        
        public void SetLocalizableString(LocalizableString localization)
        {
            _localizableString = localization;
            Localize();
        }

        public override void Localize()
        {
            if (_localizableString == null)
            {
                _label.text = "<null>";
                return;
            }
            
            _label.text = string.IsNullOrEmpty(_stringFormat)
                ? _localizableString.Localize(LocalizationManager.Instance.CurrentLanguage)
                : string.Format(_stringFormat, _localizableString.Localize(LocalizationManager.Instance.CurrentLanguage));
        }
    }
}