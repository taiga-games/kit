using System.Collections.Generic;

namespace TaigaGames.Pools
{
    public abstract class ElementPool<T> : IElementPool<T> where T : class, new()
    {
        private Queue<T> _elements;
        private int _maxCapacity;
        
        public ElementPool(int initialCapacity, int maxCapacity)
        {
            _elements = new Queue<T>();
            _maxCapacity = maxCapacity;
            
            for (var i = 0; i < initialCapacity; i++)
                _elements.Enqueue(Create());
        }

        public T Spawn()
        {
            var element = _elements.Count == 0 ? Create() : _elements.Dequeue();
            OnSpawn(element);
            return element;
        }

        public void Despawn(T element)
        {
            _elements.Enqueue(element);
            OnDespawn(element);
        }

        protected abstract void OnSpawn(T element);
        protected abstract void OnDespawn(T element);

        private T Create()
        {
            var newCapacity = _elements.Count + 1;
            if (_maxCapacity > 0 && newCapacity > _maxCapacity)
                throw new PoolExceededFixedCapacityException($"Cannot resize pool to {newCapacity} elements because it exceeds the maximum capacity of {_maxCapacity}.");
            return new T();
        }
    }
}