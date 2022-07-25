using System;
using System.Collections.Generic;

namespace Cadoscopia.Core
{
    public class MultiValueDictionary<TKey, TValue>
    {
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException" accessor="get">
        /// If the key is not found.
        /// </exception>
        public ICollection<TValue>? this[TKey key]
        {
            get
            {
                if (key == null) throw new ArgumentNullException(nameof(key));
                if (dictionary.ContainsKey(key))
                    return dictionary[key];
                throw new KeyNotFoundException($"Key {key} not found.");
            }
        }

        readonly Dictionary<TKey, HashSet<TValue>> dictionary;

        public MultiValueDictionary()
        {
            dictionary = new Dictionary<TKey, HashSet<TValue>>();
        }

        public void Add(TKey key, TValue value)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            if (!dictionary.ContainsKey(key)) 
                dictionary.Add(key, new HashSet<TValue>());
            dictionary[key]!.Add(value);
        }

        public void AddRange(TKey key, IEnumerable<TValue> values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));

            foreach (TValue value in values) Add(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            return dictionary.ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            return dictionary.Remove(key);
        }

        public bool RemoveItem(TKey key, TValue item)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            if (dictionary.ContainsKey(key))
                return dictionary[key]!.Remove(item);
            return false;
        }

        public IEnumerable<TKey> Keys => dictionary.Keys;

        public void Add(TKey key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            
            if (!dictionary.ContainsKey(key)) 
                dictionary.Add(key, new HashSet<TValue>());
        }
    }
}