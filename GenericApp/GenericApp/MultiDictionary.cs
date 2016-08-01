using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GenericApp
{
    public class MultiDictionary<K, V> : IMultiDictionary<K, V>, IEnumerable<KeyValuePair<K, IEnumerable<V>>> where K : struct where V : new()
    {
        private readonly Dictionary<K, LinkedList<V>> _dictionary = new Dictionary<K, LinkedList<V>>();

        public void Add(K key, V value)
        {
            var customAttributes = typeof(K).GetCustomAttributesData();
            if (customAttributes.All(catt => catt.AttributeType.Name != "KeyAttribute"))
            {
                throw new CustomAttributeFormatException("CustomAttributeFormatException: The Key Type doesn't has a KeyAttribute");
            }
            
            if (ContainsKey(key))
            {
                _dictionary[key].AddLast(value);
                return;
            }
            _dictionary.Add(key, new LinkedList<V>());
            _dictionary[key].AddFirst(value);
        }

        public void CreateNewValue(K key)
        {
            
            if (ContainsKey(key))
            {
                _dictionary[key].AddLast(new V());
                return;
            }
            _dictionary.Add(key, new LinkedList<V>());
            _dictionary[key].AddFirst(new V());
        }

        public bool Remove(K key)
        {
            return _dictionary.ContainsKey(key) && _dictionary.Remove(key);
        }

        public bool Remove(K key, V value)
        {
            return Contains(key, value) && _dictionary[key].Remove(value);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool ContainsKey(K key)
        {
            return _dictionary.ContainsKey(key);
        }

        public bool Contains(K key, V value)
        {
            return _dictionary.ContainsKey(key) && _dictionary[key].Contains(value);
        }

        public ICollection<K> Keys => _dictionary.Keys;

        public ICollection<V> Values
        {
            get
            {
                var list = new List<V>();
                foreach (var key in _dictionary)
                {
                    foreach (var value in key.Value)
                    {
                        if (!list.Contains(value))
                        {
                            list.Add(value);
                        }
                    }
                }
                return list;
            }
        }

        public int Count => Values.Count;

        public IEnumerator<KeyValuePair<K, IEnumerable<V>>> GetEnumerator()
        {
            return _dictionary.Select(item => new KeyValuePair<K, IEnumerable<V>>(item.Key, item.Value)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}