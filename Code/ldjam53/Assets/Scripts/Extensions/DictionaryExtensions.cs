using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Extensions
{
    public static class DictionaryExtensions
    {
        public static TKey GetRandomKey<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Func<TKey, Boolean> condition = default)
        {
            var value = default(TKey);

            if (dictionary?.Count > 0)
            {
                var baseList = dictionary.Keys.ToList();

                if (condition != default)
                {
                    baseList = baseList.Where(condition).ToList();
                }

                if (baseList.Count > 0)
                {
                    int index = UnityEngine.Random.Range(0, baseList.Count);

                    value = baseList[index];
                }
            }

            return value;
        }
    }
}
