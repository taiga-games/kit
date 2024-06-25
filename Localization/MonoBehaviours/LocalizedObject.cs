using System.Collections;
using UnityEngine;

namespace TaigaGames.Kit.Localization
{
    public abstract class LocalizedObject : MonoBehaviour
    {
        public IEnumerator Start()
        {
            yield return new WaitUntil(() => LocalizationManager.Instance);
            LocalizationManager.Instance.AddLocalizedObject(this);
            Localize();
        }

        public void OnDestroy()
        {
            if (LocalizationManager.Instance) 
                LocalizationManager.Instance.RemoveLocalizedObject(this);
        }

        public abstract void Localize();
    }
}