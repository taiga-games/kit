namespace TaigaGames.Kit.ECS
{
    public interface IEcsAutoReset<T> where T : struct
    {
        void AutoReset(ref T c);
    }
}