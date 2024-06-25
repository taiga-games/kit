using System;

namespace TaigaGames.Kit.ECS
{
    public interface IEcsPool
    {
        void Resize(int capacity);
        bool Has(int entity);
        void Del(int entity);
        void AddRaw(int entity, object dataRaw);
        object GetRaw(int entity);
        void SetRaw(int entity, object dataRaw);
        int GetId();
        Type GetComponentType();
        void Copy(int srcEntity, int dstEntity);
    }
}