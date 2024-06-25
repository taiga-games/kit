using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace TaigaGames.Kit
{
    public static class ArrayEx
    {
        public static T[] Shuffle<T> (this T[] array)
        {
            for (int t = 0; t < array.Length; t++ )
            {
                var r = Random.Range(t, array.Length);
                (array[t], array[r]) = (array[r], array[t]);
            }

            return array;
        }
        
        public static T[] Shuffle<T> (this T[] array, int seed)
        {
            var rng = new System.Random(seed);
            var length = array.Length;
            while (length > 1)
            {
                length--;
                var k = rng.Next(length + 1);
                (array[k], array[length]) = (array[length], array[k]);
            }

            return array;
        }
        
        public static T[] Shuffle<T> (this T[] array, System.Random random)
        {
            var length = array.Length;
            while (length > 1)
            {
                length--;
                var k = random.Next(length + 1);
                (array[k], array[length]) = (array[length], array[k]);
            }
            return array;
        }
        
        public static IList<T> Shuffle<T> (this IList<T> array)
        {
            for (int t = 0; t < array.Count; t++ )
            {
                var r = Random.Range(t, array.Count);
                (array[t], array[r]) = (array[r], array[t]);
            }

            return array;
        }
        
        public static IList<T> Shuffle<T> (this IList<T> array, int seed)
        {
            var rng = new System.Random(seed);
            var length = array.Count;
            while (length > 1)
            {
                length--;
                var k = rng.Next(length + 1);
                (array[k], array[length]) = (array[length], array[k]);
            }

            return array;
        }
        
        public static IList<T> Shuffle<T> (this IList<T> array, System.Random random)
        {
            var length = array.Count;
            while (length > 1)
            {
                length--;
                var k = random.Next(length + 1);
                (array[k], array[length]) = (array[length], array[k]);
            }
            return array;
        }

        public static T GetRandomElement<T>(this T[] array)
        {
            return array[Random.Range(0, array.Length)];
        }

        public static T GetRandomElement<T>(this IList<T> array)
        {
            return array[Random.Range(0, array.Count)];
        }

        public static T GetRandomElement<T>(this T[] array, int seed)
        {
            var random = new System.Random(seed);
            return array[random.Next(0, array.Length)];
        }

        public static T GetRandomElement<T>(this IReadOnlyList<T> list, int seed)
        {
            var random = new System.Random(seed);
            return list[random.Next(0, list.Count)];
        }

        public static T GetRandomElement<T>(this T[] array, System.Random random)
        {
            return array[random.Next(0, array.Length)];
        }

        public static T GetRandomElement<T>(this IList<T> list, System.Random random)
        {
            return list[random.Next(0, list.Count)];
        }

        public static T GetRandomElement<T>(this IEnumerable<T> set)
        {
            return set.ToList().GetRandomElement();
        }
        
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}
