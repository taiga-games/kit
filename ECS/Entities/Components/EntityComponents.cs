using System;

namespace TaigaGames.Kit.ECS
{
    /// <summary>
    /// Component that represents an entity id.
    /// </summary>
    [Serializable]
    public struct EntityId
    {
        /// <summary>
        /// The value of the entity id.
        /// </summary>
        public uint Value;
        
        /// <summary>
        /// Implicit conversion from EntityId to uint.
        /// </summary>
        /// <param name="entityId">Component to convert.</param>
        /// <returns>ID from component</returns>
        public static implicit operator uint(EntityId entityId) => entityId.Value;
    }

    /// <summary>
    /// Component that represents an entity name.
    /// </summary>
    [Serializable]
    public struct EntityName
    {
        /// <summary>
        /// The value of the entity name.
        /// </summary>
        public string Value;
        
        /// <summary>
        /// Implicit conversion from EntityId to string.
        /// </summary>
        /// <param name="entityName">Component to convert.</param>
        /// <returns>Name from component</returns>
        public static implicit operator string(EntityName entityName) => entityName.Value;
    }

    /// <summary>
    /// Component that represents an serializable entity.
    /// </summary>
    public struct SerializableEntity
    {
    }
}