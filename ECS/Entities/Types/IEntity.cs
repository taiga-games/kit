namespace TaigaGames.Kit.ECS
{
    /// <summary>
    /// Interface representing an entity with a unique in-game identifier and an ECS entity identifier.
    /// </summary>
    public interface IEntity
    {
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
        /// Adds a new component of type <typeparamref name="T"/> to the entity.
        /// </summary>
        /// <typeparam name="T">The type of component to add.</typeparam>
        /// <returns>A reference to the added component.</returns>
        ref T Add<T>() where T : struct;

        /// <summary>
        /// Gets the component of type <typeparamref name="T"/> from the entity.
        /// </summary>
        /// <typeparam name="T">The type of component to get.</typeparam>
        /// <returns>A reference to the component.</returns>
        ref T Get<T>() where T : struct;

        /// <summary>
        /// Gets the component of type <typeparamref name="T"/> from the entity, or adds it if it doesn't exist.
        /// </summary>
        /// <typeparam name="T">The type of component to get or add.</typeparam>
        /// <returns>A reference to the component.</returns>
        ref T GetOrAdd<T>() where T : struct;

        /// <summary>
        /// Determines whether the entity has a component of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of component to check for.</typeparam>
        /// <returns><c>true</c> if the entity has the component; otherwise, <c>false</c>.</returns>
        bool Has<T>() where T : struct;

        /// <summary>
        /// Deletes the component of type <typeparamref name="T"/> from the entity.
        /// </summary>
        /// <typeparam name="T">The type of component to delete.</typeparam>
        void Del<T>() where T : struct;

        /// <summary>
        /// Destroys the entity, removing it and all of its components.
        /// </summary>
        void Destroy();
    }
}
