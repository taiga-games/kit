using System;

namespace TaigaGames.Kit.ECS
{
    /// <summary>
    /// Interface representing an entity with a unique in-game identifier and an ECS entity identifier.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Event triggered when a component is added to the entity.
        /// </summary>
        event EntityComponentAdded OnComponentAdded;
        
        /// <summary>
        /// Event triggered when a component is changed in the entity.
        /// </summary>
        event EntityComponentChanged OnComponentChanged;
        
        /// <summary>
        /// Event triggered when a component is removed from the entity.
        /// </summary>
        event EntityComponentRemoved OnComponentRemoved;
        
        /// <summary>
        /// Gets the unique in-game identifier of the entity.
        /// </summary>
        /// <returns>The unique in-game identifier.</returns>
        uint GetId();

        /// <summary>
        /// Gets the ECS entity identifier associated with the entity.
        /// </summary>
        /// <returns>The ECS entity identifier.</returns>
        int GetEcsEntityId();

        /// <summary>
        /// Set a component of type <typeparamref name="T"/> to the entity.
        /// </summary>
        /// <typeparam name="T">The type of component to add.</typeparam>
        void Set<T>(T component) where T : struct;

        /// <summary>
        /// Set a component of type <paramref name="componentType"/> to the entity.
        /// </summary>
        /// <param name="componentType">The type of component to set.</param>
        /// <param name="component">The component to set.</param>
        void Set(Type componentType, object component);

        /// <summary>
        /// Gets the component of type <typeparamref name="T"/> from the entity.
        /// </summary>
        /// <typeparam name="T">The type of component to get.</typeparam>
        /// <returns>The component.</returns>
        T Get<T>() where T : struct;
        
        /// <summary>
        /// Gets the component of type <typeparamref name="T"/> from the entity as a reference.
        /// </summary>
        /// <typeparam name="T">The type of component to get.</typeparam>
        /// <returns>The component.</returns>
        ref T GetRef<T>() where T : struct; 
        
        /// <summary>
        /// Gets the component of type <paramref name="componentType"/> from the entity.
        /// </summary>
        /// <param name="componentType">The type of component to get.</param>
        /// <returns>The component.</returns>
        object Get(Type componentType);

        /// <summary>
        /// Determines whether the entity has a component of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of component to check for.</typeparam>
        /// <returns><c>true</c> if the entity has the component; otherwise, <c>false</c>.</returns>
        bool Has<T>() where T : struct;
        
        /// <summary>
        /// Determines whether the entity has a component of type <paramref name="componentType"/>.
        /// </summary>
        /// <param name="componentType">The type of component to check for.</param>
        /// <returns><c>true</c> if the entity has the component; otherwise, <c>false</c>.</returns>
        bool Has(Type componentType);

        /// <summary>
        /// Deletes the component of type <typeparamref name="T"/> from the entity.
        /// </summary>
        /// <typeparam name="T">The type of component to delete.</typeparam>
        void Del<T>() where T : struct;
        
        /// <summary>
        /// Deletes the component of type <paramref name="componentType"/> from the entity.
        /// </summary>
        /// <param name="componentType">The type of component to delete.</param>
        void Del(Type componentType);

        /// <summary>
        /// Destroys the entity, removing it and all of its components.
        /// </summary>
        void Destroy();

        /// <summary>
        /// Clones the entity, copying all of its components to the target entity.
        /// </summary>
        /// <param name="toEntity">The target entity to clone to.</param>
        void Clone(IEntity toEntity);

        /// <summary>
        /// Tries to get the name of the entity.
        /// </summary>
        /// <param name="name">The name of the entity.</param>
        /// <returns><c>true</c> if the entity has a name; otherwise, <c>false</c>.</returns>
        bool TryGetName(out string name);
        
        /// <summary>
        /// Gets the name of the entity.
        /// </summary>
        /// <returns>The name of the entity.</returns>
        string GetName();
        
        /// <summary>
        /// Sets the name of the entity.
        /// </summary>
        /// <param name="name">The name to set.</param>
        void SetName(string name);
    }
}
