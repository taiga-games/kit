using UnityEngine;

namespace TaigaGames.Kit.Localization
{
    [CreateAssetMenu(menuName = "Localization/AudioClip", fileName = "Localization AudioClip", order = 1002)]
    public class LocalizableAudioClip : LocalizationObject<AudioClip>
    {
        public override AudioClip Localize(SystemLanguage language)
        {
            var result = base.Localize(language);
            return result ? result : English;
        }
    }
}