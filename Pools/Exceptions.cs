using System;

namespace TaigaGames.Pools
{
    /// <summary>
    /// Exception thrown when trying to resize a pool to a capacity that exceeds the maximum capacity.
    /// </summary>
    public class PoolExceededFixedCapacityException : Exception
    {
        public PoolExceededFixedCapacityException(string message) : base(message)
        {
        }
    }
}