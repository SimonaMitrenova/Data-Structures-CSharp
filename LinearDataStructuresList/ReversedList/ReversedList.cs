namespace ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IEnumerable<T>
    {
        private const int defaultCapasity = 16;
        private T[] array;

        public ReversedList() : this(defaultCapasity)
        {
        }

        public ReversedList(int capasity)
        {
            this.array = new T[capasity];
            this.Count = 0;
        }

        public int Count { get; private set; }

        public int Capasity
        {
            get
            {
                return this.array.Length;
            }
        }

        public void Add(T element)
        {
            if (this.Count == this.Capasity)
            {
                var newArray = new T[this.Capasity * 2];
                Array.Copy(this.array, newArray, this.Count);
                this.array = newArray;
            }

            this.array[this.Count] = element;
            this.Count++;
        }

        public void Remove(int index)
        {
            if (index < 0 || index > this.Count)
            {
                throw new IndexOutOfRangeException("Invalid index.");
            }

            int innerIndex = this.Count - index - 1;
            var newArray = new T[this.array.Length];
            Array.Copy(this.array, newArray, innerIndex);
            Array.Copy(this.array, innerIndex + 1, newArray, innerIndex, index + 1);
            this.array = newArray;
            this.Count--;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index > this.Count)
                {
                    throw new IndexOutOfRangeException("Invalid index.");
                }

                return this.array[this.Count - index - 1];
            }

            set
            {
                if (index < 0 || index > this.Count)
                {
                    throw new IndexOutOfRangeException("Invalid index.");
                }

                this.array[this.Count - index - 1] = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                yield return this.array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
