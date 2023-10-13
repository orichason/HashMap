using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace HashMap
{
    //indexer example: https://sharplab.io/#v2:C4LgTgrgdgNAJiA1AHwAICYAMBYAUBgRjzwCUBTOAZWAE8AbMgQTDAEMaACAIwHsuOAvByhkA7hwAUASgDceXlwDaAIgDMygLqCOBTJjn4CATgkKVAVk2ziuPBg7kqtBszY08AbzwcfHAJZQwD5QEAC2BAa+/oHBYeiRvgFBwmGqCT5JsaEALOnRySGh5nnevqiq+RzAABZ+AM6KqLrRcGQAHhqlPl64UVEA5mTAXX09feMcdaJ+wADG1ZJJAHQACqxgdWQSAa1tUlIjExxjRxOzrJs6ID6oAOwp4XmnvueX6Nccdw/xh88cr2QOKoPl9Cmlfs8ARxsiD7oVchDTlDzLCHsU/n1EVEAL5Y3w1MA8cQicQASSguwA8hBgJSAGYkVhQQYAUTaszIAAdgH4eFAJMoVjx+sprL0JrjxeNNsMpaM8T4pjN5otAqt1ptthT2vsFcc9VEoQQPoUCNoAG6sOgQMgyT63J5/KHvLLoC1Wm12u6OyEXQHArIVISW622+0+pF+6EmsLZd2hr0Og0vKMorLmeOe8MTPWSjEEonCMQcclUmn0xnMshsjnc3n8wXCqAAcmAooj2IxI0lkpsQA=
    public class HashMap<TKey, TValue> : IDictionary<TKey, TValue>
    {
        public struct Entry
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
                int hashCode = comparer.GetHashCode(key);

                int bucketPosition = bucketPositions[hashCode % bucketPositions.Length];

                for (int i = bucketPosition; i != -1; i = entries[i].Next)
                {
                    if (entries[bucketPosition].Key.Equals(key)) return entries[bucketPosition].Value;
                }

                throw new Exception("Key doesn't exist");
            }
            set
            {
                int hashCode = comparer.GetHashCode(key);

                int bucketPosition = bucketPositions[hashCode % bucketPositions.Length];

                for (int i = bucketPosition; i != -1; i = entries[i].Next)
                {
                    if (entries[bucketPosition].Key.Equals(key))
                    {
                        entries[bucketPosition].Value = value;
                    }
                }
            }
        }

        public ICollection<TKey> Keys => null;

        public ICollection<TValue> Values => null;

        private IEqualityComparer<TKey> comparer;
        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public int[] bucketPositions { get; private set; }

        public Entry[] entries { get; private set; }

        public HashMap(IEqualityComparer<TKey> comparer)
        {
            bucketPositions = Array.Empty<int>();
            entries = Array.Empty<Entry>();
            this.comparer = comparer;
        }

        public void Add(TKey key, TValue value)
        {
            Add(new(key, value));
        }

        private Entry[] ReSizeEntries(Entry[] entries)
        {
            Entry[] reSizedArray = new Entry[entries.Length * 2];

            for (int i = 0; i < entries.Length; i++)
            {
                reSizedArray[i] = entries[i];
            }

            return reSizedArray;
        }
        private int[] ReHash(int[] bucketPositions)
        {
            int[] reSizedArray = new int[bucketPositions.Length * 2];

            //set values of array to -1 to ensure no entry is already pointed to
            for (int i = 0; i < reSizedArray.Length; i++)
            {
                reSizedArray[i] = -1;
            }

            for (int i = 0; i < entries.Length; i++)
            {
                //loop through entries and re hash them to figure out where they go into the new buckets

                int bucketIndex = entries[i].HashCode % reSizedArray.Length;

                if (reSizedArray[bucketIndex] != -1)
                {
                    entries[i].Next = reSizedArray[bucketIndex];
                }

                reSizedArray[bucketIndex] = i;
            }

            return reSizedArray;
        }

        bool BucketContains(TKey key, int bucketPosition)
        {
            //loop through bucket to see if key exists. 

            for (int i = bucketPosition; i != -1; i = entries[i].Next)
            {
                if (entries[i].Key.Equals(key)) return true;
            }

            return false;
        }
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            int hashCode = comparer.GetHashCode(item.Key);

            Entry entry = new Entry(item.Key, item.Value, -1, hashCode);

            //checking if key already exists
            if (Count != 0 && BucketContains(entry.Key, bucketPositions[hashCode % bucketPositions.Length])) throw new Exception("Key already exists");

            if (bucketPositions.Length == 0)
            {
                bucketPositions = new int[16];
                entries = new Entry[16];
                for (int i = 0; i < bucketPositions.Length; i++)
                {
                    bucketPositions[i] = -1;
                }
            }

            else if (Count == bucketPositions.Length)
            {
                bucketPositions = ReHash(bucketPositions);
                entries = ReSizeEntries(entries);
            }

            int bucketIndex = entry.HashCode % bucketPositions.Length;

            entries[Count] = entry;

            if (bucketPositions[bucketIndex] == -1)
            {
                bucketPositions[bucketIndex] = Count;
            }

            else
            {
                entries[Count].Next = bucketPositions[bucketIndex];
                bucketPositions[bucketIndex] = Count;
            }

            Count++;
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
            if (Count == 0) return false;

            int bucketIndex = comparer.GetHashCode(key) % bucketPositions.Length;

            Entry current = entries[bucketPositions[bucketIndex]];
            if (current.Key.Equals(key)) return true;

            while (current.Next != -1)
            {
                if (entries[current.Next].Key.Equals(key)) return true;

                current = entries[current.Next];
            }

            return false;
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
            int hashCode = comparer.GetHashCode(key);

            int bucketPosition = bucketPositions[hashCode % bucketPositions.Length];

            for (int i = bucketPosition; i != -1; i = entries[i].Next)
            {
                if (entries[bucketPosition].Key.Equals(key))
                {
                    value = entries[bucketPosition].Value;
                    return true;
                }
            }

            value = default;

            return false;

        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
