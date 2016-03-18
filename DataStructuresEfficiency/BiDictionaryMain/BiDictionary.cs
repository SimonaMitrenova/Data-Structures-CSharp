namespace BiDictionary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BiDictionary<K1, K2, V>
    {
        private Dictionary<K1, List<V>> valuesByFirstKey;
        private Dictionary<K2, List<V>> valuesBySecondKey;
        private Dictionary<Tuple<K1, K2>, List<V>> valuesByBothKeys;

        public BiDictionary()
        {
            this.valuesByFirstKey = new Dictionary<K1, List<V>>();
            this.valuesBySecondKey = new Dictionary<K2, List<V>>();
            this.valuesByBothKeys = new Dictionary<Tuple<K1, K2>, List<V>>();
        }

        public void Add(K1 firstKey, K2 secondKey, V value)
        {
            if (!this.valuesByFirstKey.ContainsKey(firstKey))
            {
                this.valuesByFirstKey[firstKey] = new List<V>();
            }
            this.valuesByFirstKey[firstKey].Add(value);

            if (!this.valuesBySecondKey.ContainsKey(secondKey))
            {
                this.valuesBySecondKey[secondKey] = new List<V>();
            }
            this.valuesBySecondKey[secondKey].Add(value);

            var pair = new Tuple<K1, K2>(firstKey, secondKey);
            if (!this.valuesByBothKeys.ContainsKey(pair))
            {
                this.valuesByBothKeys[pair] = new List<V>();
            }
            this.valuesByBothKeys[pair].Add(value);
        }

        public IEnumerable<V> Find(K1 firstKey, K2 secondKey)
        {
            var pair = new Tuple<K1, K2>(firstKey, secondKey);
            if (!this.valuesByBothKeys.ContainsKey(pair))
            {
                return Enumerable.Empty<V>();
            }

            return this.valuesByBothKeys[pair];
        }

        public IEnumerable<V> FindByFirstKey(K1 firstKey)
        {
            if (!this.valuesByFirstKey.ContainsKey(firstKey))
            {
                return Enumerable.Empty<V>();
            }

            return this.valuesByFirstKey[firstKey];
        }

        public IEnumerable<V> FindBySecondKey(K2 secondKey)
        {
            if (!this.valuesBySecondKey.ContainsKey(secondKey))
            {
                return Enumerable.Empty<V>();
            }

            return this.valuesBySecondKey[secondKey];
        }

        public bool Remove(K1 firstKey, K2 secondKey)
        {
            var pair = new Tuple<K1, K2>(firstKey, secondKey);
            if (!this.valuesByBothKeys.ContainsKey(pair))
            {
                return false;
            }

            List<V> valuesToRemove = this.valuesByBothKeys[pair];

            this.valuesByBothKeys.Remove(pair);
            foreach (var valueToRemove in valuesToRemove)
            {
                this.valuesByFirstKey[firstKey].Remove(valueToRemove);
                this.valuesBySecondKey[secondKey].Remove(valueToRemove);
            }

            return true;
        }
    }
}
