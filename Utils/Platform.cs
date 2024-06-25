namespace TaigaGames.Kit
{
    public static class Platform
    {
        public static bool IsMobile()
        {
#if UNITY_ANDROID || UNITY_IOS
            return true;
#endif
            return false;
        }

        public static bool IsDesktop()
        {
            return !IsMobile();
        }
    }
}