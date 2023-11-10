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
    //IEnumerator example: https://sharplab.io/#v2:CYLg1APgAgTAjAWAFBQAwAIpwCwG5lqZwB0AwgPYA2lApgMYAuAluQHYDO+SBAzJjOgBKNYAGUGAT1oBBAE6yAhhPQB6FSCI9kAb2Tp9mPk1YN0AOQCuAWwBGNWXC4HD6Y6cu37MJwahGT5tZ2sjw++n6uAR7BeHq+/u5B9gCsYehx4XzCYpIy8koAFG7orNZwADSRpqVWMJXFNTz1ATXYzdXWyQCUGei6SM7O0fZw6AC8JWVpQ0myAhM13r0zniHjk1ahywbDstjrrdM7s8kHnWkAvsi9EQCiNfYKDOSy6ADiNAz31o/PsgVdcYAPhKNAA7uhvlZfi8CgwABZMdhdLh6NQGACSUJhrywPGIHy+D0UfwBwPen2xJNhKOu3AG8XQ7AYsgsjEhxKeL1U6nQWM5f16/UGBmKlHIdCeLFYR302XEUhockUyisEmVSll6QZIruAth8tySvyqvVJp6OsGwpFgwRSOIao1ygmjpNWuc4slzDY61QWquluc20yVXQpAs8hoAW0AHNPrh0AAHWRMABuTxoTPjAe26PQ5BsACt6KZ+T9qbIyBHZFHTGMQeHIyZUYHGVB9gARJGJ8jsGhk7QBm3a4MuGzkKjoACy5FTNDMNBoAA8GADR9bh/p2GCmAw6PCCmAwJ6pWwLZuDBuLwZJX30HAQKPh42awEXWaVcRdo4nzaoAB2dAWQsGh3U3W9MxgR9WwvF9a3WV1P12JYYM3ACgNZUDr0GX9hwg9AeGg7DnDgt90EQpQv1mLZUOHdDgKw4j9Fwm18OwIimLDat4PfJ0qNWWJaL/QCGLAvCFDvZIOKY0i63Ij9KN2VIWOcejMITTiWKHa90IAMwUSg+39Ok6L4dshBoPtV3PG0r2HE9vVYX1jMtAMcyQNQvXQBQDnBdAvQKAAiccbECyo4FQWkPJULybHWBQuDUGwvwUaF1kCwtyHhVhArpNQmF0/ynlcdhvP4Sod2oMr9wUVg43QABNW5RGQfLCq8pEyuZVl2UqyhqvhWr6rpCJYDDbUrxuGABCFUdDUVJ10BC3ywQKZJKjaAjKnWgjUAATkirVljzJaCwJSl9X+KLh1HAAZJEGAAHjcEEQvmUEIXu5lnpMIEBx2zamnQHaeAuI7UPTV5MszCYQvOolyy5K6tTBRFaHQAp9MMmgbJFOybUh/zuLI6GqybBgxKIPaCjoYmGGum1tJFUddJeGgFH3DHCd3GgrEiU6bEBLBqZ5qwGeZ1C1AKAoMVIBRZC6EKunhuXkdHXMVH0OHCVVgFwecNygA=
    //indexer example: https://sharplab.io/#v2:C4LgTgrgdgNAJiA1AHwAICYAMBYAUBgRjzwCUBTOAZWAE8AbMgQTDAEMaACAIwHsuOAvByhkA7hwAUASgDceXlwDaAIgDMygLqCOBTJjn4CATgkKVAVk2ziuPBg7kqtBszY08AbzwcfHAJZQwD5QEAC2BAa+/oHBYeiRvgFBwmGqCT5JsaEALOnRySGh5nnevqiq+RzAABZ+AM6KqLrRcGQAHhqlPl64UVEA5mTAXX09feMcdaJ+wADG1ZJJAHQACqxgdWQSAa1tUlIjExxjRxOzrJs6ID6oAOwp4XmnvueX6Nccdw/xh88cr2QOKoPl9Cmlfs8ARxsiD7oVchDTlDzLCHsU/n1EVEAL5Y3w1MA8cQicQASSguwA8hBgJSAGYkVhQQYAUTaszIAAdgH4eFAJMoVjx+sprL0JrjxeNNsMpaM8T4pjN5otAqt1ptthT2vsFcc9VEoQQPoUCNoAG6sOgQMgyT63J5/KHvLLoC1Wm12u6OyEXQHArIVISW622+0+pF+6EmsLZd2hr0Og0vKMorLmeOe8MTPWSjEEonCMQcclUmn0xnMshsjnc3n8wXCqAAcmAooj2IxI0lkpsQA=
    public class HashMap<TKey, TValue> : IDictionary<TKey, TValue>
    {
        public struct Enumerator : IEnumerator
        {
            HashMap<TKey, TValue> hashMap;
            public int location { get; private set; }
            public Entry CurrentEntry { get; private set; }
            object IEnumerator.Current => CurrentEntry;

            public Enumerator(HashMap<TKey, TValue> hashMap)
            {
                this.hashMap = hashMap;
                location = 0;
                CurrentEntry = hashMap.entries[0];
            }
            public bool MoveNext()
            {
                CurrentEntry = hashMap.entries[location];

                if (location + 1 == hashMap.entries.Length)
                {
                    return false;
                }

                location++;
                return true;
            }

            public void Reset()
            {
                location = 0;
                CurrentEntry = hashMap.entries[0];
            }
        }
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

                int bucketIndex = hashCode % bucketPositions.Length;

                for (int i = bucketPositions[bucketIndex]; i != -1; i = entries[i].Next)
                {
                    if (entries[i].Key.Equals(key)) return entries[i].Value;
                }

                throw new Exception("Key doesn't exist");
            }
            set
            {
                if (bucketPositions.Length == 0)
                {
                    InitBuckets();
                }

                int hashCode = comparer.GetHashCode(key);

                int bucketIndex = hashCode % bucketPositions.Length;

                if (bucketPositions[bucketIndex] == -1)
                {
                    bucketPositions[bucketIndex] = Count;
                    entries[Count] = new Entry(key, value, -1, hashCode);
                    Count++;
                }

                else
                {
                    for (int i = bucketPositions[bucketIndex]; i != -1; i = entries[i].Next)
                    {
                        if (entries[i].Key.Equals(key))
                        {
                            entries[i].Value = value;
                        }
                    }
                }
            }
        }

        public ICollection<TKey> Keys => null;

        public ICollection<TValue> Values => null;

        private IEqualityComparer<TKey> comparer;
        public int Count { get; private set; }

        private int entryIndex;
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

        private void ReSizeEntries()
        {
            Entry[] reSizedArray = new Entry[entries.Length * 2];

            for (int i = 0; i < entries.Length; i++)
            {
                reSizedArray[i] = entries[i];
            }

            entries = reSizedArray;
        }
        private void ReHash()
        {
            //CHANGE TO VOID !!!!
            int[] reSizedBuckets = new int[bucketPositions.Length * 2];

            //set values of array to -1 to ensure no entry is already pointed to
            for (int i = 0; i < reSizedBuckets.Length; i++)
            {
                reSizedBuckets[i] = -1;
            }

            for (int i = 0; i < entries.Length; i++)
            {
                //loop through entries and re hash them to figure out where they go into the new buckets

                int newBucketIndex = entries[i].HashCode % reSizedBuckets.Length;

                if (reSizedBuckets[newBucketIndex] != -1)
                {
                    entries[i].Next = reSizedBuckets[newBucketIndex];
                }

                reSizedBuckets[newBucketIndex] = i;
            }

            bucketPositions = reSizedBuckets;
        }

        public bool BucketContains(TKey key, int bucketIndex)
        {
            //loop through bucket to see if key exists. 

            for (int i = bucketPositions[bucketIndex]; i != -1; i = entries[i].Next)
            {
                if (entries[i].Key.Equals(key)) return true;
            }

            return false;
        }

        public void InitBuckets()
        {
            bucketPositions = new int[16];
            entries = new Entry[16];
            for (int i = 0; i < bucketPositions.Length; i++)
            {
                bucketPositions[i] = -1;
            }
        }
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            if (bucketPositions.Length == 0)
            {
                InitBuckets();
            }

            int hashCode = comparer.GetHashCode(item.Key);

            int bucketIndex = hashCode % bucketPositions.Length;

            Entry entry = new Entry(item.Key, item.Value, -1, hashCode);

            //checking if key already exists
            if (Count != 0 && BucketContains(entry.Key, bucketIndex)) throw new ArgumentException("Key already exists");


            else if (Count == bucketPositions.Length)
            {
                ReHash();
                ReSizeEntries();
            }

            entries[entryIndex] = entry;

            if (bucketPositions[bucketIndex] == -1)
            {
                bucketPositions[bucketIndex] = entryIndex;
            }

            else
            {
                entries[entryIndex].Next = bucketPositions[bucketIndex];
                bucketPositions[bucketIndex] = entryIndex;
            }
            entryIndex++;
            Count++;
        }

        public void Clear()
        {
            for (int i = 0; i < bucketPositions.Length; i++)
            {
                bucketPositions[i] = -1;
                entries[i].Next = -1;
                entries[i].Value = default;
                entries[i].Key = default;
                entries[i].HashCode = default;
            }

            Count = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (Count == 0) return false;

            int bucketIndex = comparer.GetHashCode(item.Key) % bucketPositions.Length;

            Entry current = entries[bucketPositions[bucketIndex]];
            if (current.Key.Equals(item.Key)) return true;

            while (current.Next != -1)
            {
                if (entries[current.Next].Key.Equals(item.Key) && entries[current.Next].Value.Equals(item.Value)) return true;

                current = entries[current.Next];
            }

            return false;
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
            return new Enumerator(this);
        }

        public bool Remove(TKey key)
        {
            int hashCode = comparer.GetHashCode(key);

            int bucketIndex = hashCode % bucketPositions.Length;

            if (Count == 0 || !BucketContains(key, bucketIndex)) throw new Exception("Key not in map");

            Count--;

            //TO DO: AFTER EVERY REMOVE, MOVE ALL ENTRIES AFTER IT LEFT BY ONE AND RESIZE WHEN  NEEDED

            for (int i = bucketPositions[bucketIndex]; i != -1; i = entries[i].Next)
            {
                //check if it is the only entry in the bucket

                if (entries[i].Key.Equals(key))
                {
                    ShiftEntriesLeft(bucketIndex);

                    bucketPositions[bucketIndex] = entries[i].Next;

                    return true;
                }
                Entry nextEntry = entries[entries[i].Next];

                if (nextEntry.Key.Equals(key))
                {
                    entries[i].Next = nextEntry.Next;

                    ShiftEntriesLeft(bucketIndex);
                }
            }
            return false;
        }

        private void ShiftEntriesLeft(int startingIndex)
        {
            for (int i = bucketPositions[startingIndex]; i <= Count; i++)
            {
                entries[i] = entries[i + 1];
            }

            if (Count < entries.Length / 4)
            {
                CompressArrays();
            }
        }

        private void CompressArrays()
        {
            Entry[] reSizedEntries = new Entry[entries.Length / 2];
            int[] reSizedBucketPositions = new int[bucketPositions.Length / 2];

            for (int i = 0; i < reSizedEntries.Length; i++)
            {
                reSizedEntries[i] = entries[i];
            }

            entries = reSizedEntries;

            for (int i = 0; i < reSizedBucketPositions.Length; i++)
            {
                reSizedBucketPositions[i] = bucketPositions[i];
            }

            bucketPositions = reSizedBucketPositions;
        }
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            Remove(item.Key);

            return false;
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

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => (IEnumerator<KeyValuePair<TKey, TValue>>)GetEnumerator();
    }
}
