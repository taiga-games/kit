using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace TaigaGames.Kit
{
    public static class WeightedRandomEx
    {
        public static T GetWeightedRandomElement<T>(this IEnumerable<T> collection, Func<T, float> getWeight)
        {
            return GetWeightedRandomElement(collection, getWeight, Random.value);
        }
        
        public static T GetWeightedRandomElement<T>(this IEnumerable<T> collection, Func<T, float> getWeight, float randomValue)
        {
            if (randomValue is > 1f or < 0f)
                throw new ArgumentOutOfRangeException(nameof(randomValue), "Value must be between 0 and 1");
            
            var totalWeight = collection.Sum(getWeight);

            var randomNumber = totalWeight * randomValue;
            foreach (var item in collection)
            {
                var weight = getWeight(item);
                if (randomNumber <= weight)
                    return item;
                randomNumber -= weight;
            }

            throw new InvalidOperationException("WeightedRandom: This should never happen");
        }
    }
}