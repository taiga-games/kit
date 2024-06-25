namespace TaigaGames.Kit.ECS
{
    public interface IConvertableToEntity
    {
        void ConvertToEntity(EcsWorld world, int entity);
    }
}