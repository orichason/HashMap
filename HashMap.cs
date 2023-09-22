using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashMap
{
    //indexer example: https://sharplab.io/#v2:C4LgTgrgdgNAJiA1AHwAICYAMBYAUBgRjzwCUBTOAZWAE8AbMgQTDAEMaACAIwHsuOAvByhkA7hwAUASgDceXlwDaAIgDMygLqCOBTJjn4CATgkKVAVk2ziuPBg7kqtBszY08AbzwcfHAJZQwD5QEAC2BAa+/oHBYeiRvgFBwmGqCT5JsaEALOnRySGh5nnevqiq+RzAABZ+AM6KqLrRcGQAHhqlPl64UVEA5mTAXX09feMcdaJ+wADG1ZJJAHQACqxgdWQSAa1tUlIjExxjRxOzrJs6ID6oAOwp4XmnvueX6Nccdw/xh88cr2QOKoPl9Cmlfs8ARxsiD7oVchDTlDzLCHsU/n1EVEAL5Y3w1MA8cQicQASSguwA8hBgJSAGYkVhQQYAUTaszIAAdgH4eFAJMoVjx+sprL0JrjxeNNsMpaM8T4pjN5otAqt1ptthT2vsFcc9VEoQQPoUCNoAG6sOgQMgyT63J5/KHvLLoC1Wm12u6OyEXQHArIVISW622+0+pF+6EmsLZd2hr0Og0vKMorLmeOe8MTPWSjEEonCMQcclUmn0xnMshsjnc3n8wXCqAAcmAooj2IxI0lkpsQA=
    internal class HashMap<TKey, TValue> : IDictionary<TKey, TValue>
    {
        struct Entry
        {
            public TKey Key;
            public TValue Value;
            public int Next;
            public int HashCode;
            public Entry(TKey key, TValue value, int next, int hashCode) 
            {
                this.Key = key;
                this.Value = value;
                this.Next = next;
                this.HashCode = hashCode;
            }
        }
        public TValue this[TKey key]
        {
            get
            {
                throw new NotImplementedException();
            }
            set => throw new NotImplementedException();
        }

        public ICollection<TKey> Keys => null;

        public ICollection<TValue> Values => null;

        private IEqualityComparer<TKey> comparer;
        public int Count { get; private set; }

        public bool IsReadOnly => false;

        private int[] bucketPositions;

        private Entry[] entries;
        public HashMap(IEqualityComparer<TKey> Comparer)
        {
            bucketPositions = Array.Empty<int>();
            entries = Array.Empty<Entry>();
            this.comparer = Comparer;
        }

        public void Add(TKey key, TValue value)
        {
            Add(new(key, value));
        }

        private Entry[] ReSizeEntries(Entry[] arrayToResize)
        {
            
        }

        private int[] ReSizeBucketPositions(int[] bucketPositions)
        {
            int[] reSizedArray = new int[bucketPositions.Length * 2];

            for (int i = 0; i < reSizedArray.Length; i++)
            {
                for (int i = 0; i < length; i++)
                {
                    //To Do: loop through entries and re hash them to figure out where they go into the new array
                    entries[i].HashCode;

                }
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            if(bucketPositions.Length == 0)
            {
                int[] temp = new int[16];
                Entry[] resizedEntries = new Entry[16];

                bucketPositions = temp;
                entries = resizedEntries;
            }

            else if(Count == bucketPositions.Length)
            {
                entries = ReSize(entries);
            }


        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            throw new NotImplementedException();
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
