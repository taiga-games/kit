namespace TaigaGames.Pools
{
    /// <summary>
    /// Interface representing a pool for managing reusable elements.
    /// </summary>
    /// <typeparam name="T">The type of elements in the pool, which must be a class with a parameterless constructor.</typeparam>
    public interface IElementPool<T> where T : class, new()
    {
        /// <summary>
        /// Spawn an element from the pool.
        /// </summary>
        /// <returns>The spawned element</returns>
        T Spawn();

        /// <summary>
        /// Despawns the specified element, returning it to the pool.
        /// </summary>
        /// <param name="element">The element to despawn.</param>
        void Despawn(T element);
    }
}