using System;
using System.Collections.Generic;

namespace TaigaGames.Kit.ECS
{
    /// <summary>
    /// Interface for entity registry that handles the registration, retrieval, and unregistration of entities.
    /// </summary>
    public interface IEntityRegistry
    {
        /// <summary>
        /// Retrieves the next available unique identifier for entities.
        /// </summary>
        /// <returns>The next available unique identifier as a <see cref="uint"/>.</returns>
        uint GetNextId();

        /// <summary>
        /// Registers a new entity and assigns it a unique identifier.
        /// </summary>
        /// <returns>The newly registered entity.</returns>
        IEntity RegisterNew();

        /// <summary>
        /// Registers a new entity with a specified ECS entity identifier.
        /// </summary>
        /// <param name="ecsEntityId">The ECS entity identifier to associate with the new entity.</param>
        /// <returns>The newly registered entity.</returns>
        IEntity RegisterNew(int ecsEntityId);

        /// <summary>
        /// Registers an entity with a specified unique identifier and ECS entity identifier.
        /// </summary>
        /// <param name="id">The unique identifier for the entity.</param>
        /// <param name="ecsEntityId">The ECS entity identifier to associate with the entity.</param>
        /// <returns>The registered entity.</returns>
        IEntity Register(uint id, int ecsEntityId);

        /// <summary>
        /// Unregisters an entity with the specified unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to unregister.</param>
        void Unregister(uint id);

        /// <summary>
        /// Retrieves an entity with the specified unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to retrieve.</param>
        /// <returns>The entity associated with the specified identifier.</returns>
        IEntity GetEntity(uint id);

        /// <summary>
        /// Attempts to retrieve an entity with the specified unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to retrieve.</param>
        /// <param name="entity">When this method returns, contains the entity associated with the specified identifier, if the entity is found; otherwise, <c>null</c>.</param>
        /// <returns><c>true</c> if the entity is found; otherwise, <c>false</c>.</returns>
        bool TryGetEntity(uint id, out IEntity entity);
        
        /// <summary>
        /// Serializes the entity registry to a dictionary.
        /// </summary>
        /// <returns>A dictionary containing the serialized entity registry.</returns>
        Dictionary<uint, Dictionary<Type, object>> Serialize();
        
        /// <summary>
        /// Deserializes the entity registry from a dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary containing the serialized entity registry.</param>
        void Deserialize(Dictionary<uint, Dictionary<Type, object>> dictionary);
    }
}