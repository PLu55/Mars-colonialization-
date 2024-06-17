using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PLu.Utilities
{    
    [Serializable]
    public class SerializableDictionaryItem<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;

        public SerializableDictionaryItem(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }

    [Serializable]
    
    public class SerializableDictionary<TKey, TValue>
    {
        [SerializeField]
        
        private List<SerializableDictionaryItem<TKey, TValue>> _items;

        // Prevent direct construction
        private SerializableDictionary() { }

        public SerializableDictionary(Dictionary<TKey, TValue> dictionary)
        {
            _items = new List<SerializableDictionaryItem<TKey, TValue>>();
        }

        public Dictionary<TKey, TValue> ToDictionary()
        {
            Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
            foreach (var item in _items)
            {
                dictionary[item.Key] = item.Value;
            }
            return dictionary;
        }
    }
}