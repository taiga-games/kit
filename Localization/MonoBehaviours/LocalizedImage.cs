using UnityEngine;
using UnityEngine.UI;

namespace TaigaGames.Kit.Localization
{
    [RequireComponent(typeof(Image))]
    public class LocalizedImage : LocalizedObject
    {
        public LocalizableSprite Localization;
        public Image Image;

        private void Reset()
        {
            Image = GetComponent<Image>();
        }

        public override void Localize()
        {
            Image.sprite = Localization.Localize(LocalizationManager.Instance.CurrentLanguage);
        }
    }
}