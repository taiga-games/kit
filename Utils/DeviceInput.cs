using UnityEngine;
namespace TaigaGames.Kit
{
    public static class DeviceInput
    {
        public static bool BackButtonPressed()
        {
            if (Application.isEditor)
            {
                return Input.GetKeyDown(KeyCode.Escape);
            }
            return Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape);
        }
    }
}
