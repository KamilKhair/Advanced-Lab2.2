using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace GenericApp
{
    public class MultiDictionary<K, V> : IMultiDictionary<K, V>, IEnumerable<KeyValuePair<K, IEnumerable<V>>> where K : struct where V : struct
    {
        public MultiDictionary()
        {
            Dictionary = new Dictionary<K, LinkedList<V>>();
        }

        private Dictionary<K, LinkedList<V>> Dictionary { get; }

        public void Add(K key, V value)
        {
            if (!typeof(K).IsDefined(typeof(KeyAttribute)))
            {
                throw new CustomAttributeFormatException("CustomAttributeFormatException: The Key Type doesn't has a KeyAttribute");
            }

            if (ContainsKey(key))
            {
                Dictionary[key].AddLast(value);
            }
            else
            {
                Dictionary.Add(key, new LinkedList<V>());
                Dictionary[key].AddFirst(value);
            }
        }

        public void CreateNewValue(K key)
        {
            
            if (ContainsKey(key))
            {
                Dictionary[key].AddLast((V)Activator.CreateInstance(typeof(V)));
            }
            else
            {
                Dictionary.Add(key, new LinkedList<V>());
                Dictionary[key].AddFirst((V)Activator.CreateInstance(typeof(V)));
            }
        }

        public bool Remove(K key)
        {
            if (!Dictionary.ContainsKey(key))
            {
                return false;
            }
            return Dictionary.Remove(key);
        }

        public bool Remove(K key, V value)
        {
            if (!Contains(key, value))
            {
                return false;
            }
            return Dictionary[key].Remove(value);
        }

        public void Clear()
        {
            Dictionary.Clear();
        }

        public bool ContainsKey(K key)
        {
            return Dictionary.ContainsKey(key);
        }

        public bool Contains(K key, V value)
        {
            if (!Dictionary.ContainsKey(key))
            {
                return false;
            }
            return Dictionary[key].Contains(value);
        }

        public ICollection<K> Keys => Dictionary.Keys;

        public ICollection<V> Values
        {
            get
            {
                var list = new List<V>();
                foreach (var key in Dictionary)
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

        public int Count => Dictionary.Count;

        public IEnumerator<KeyValuePair<K, IEnumerable<V>>> GetEnumerator()
        {
            var list = new List<KeyValuePair<K, IEnumerable<V>>>();

            foreach (var item in Dictionary)
            {
                list.Add(new KeyValuePair<K, IEnumerable<V>>(item.Key, item.Value));
            }

            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}