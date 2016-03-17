namespace ArrayBasedStack
{
    using System;
    using System.Linq;

    public class ArrayStack<T>
    {
        private const int InitialCapasity = 16;
        private T[] elements;

        public ArrayStack() : this(InitialCapasity)
        {
        }

        public ArrayStack(int capasity)
        {
            this.elements = new T[capasity];
        }

        public int Count { get; private set; }

        public int Capasity
        {
            get
            {
                return this.elements.Length;
            }
        }

        public void Push(T element)
        {
            if (this.Count == this.elements.Length)
            {
                this.Grow();
            }

            this.elements[this.Count] = element;
            this.Count++;
        }

        public T Pop()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Empty stack.");
            }

            var element = this.elements[this.Count - 1];
            this.elements[this.Count - 1] = default(T);
            this.Count--;
            return element;
        }

        public T[] ToArray()
        {
            var copyElements = new T[this.Count];
            Array.Copy(this.elements, copyElements, this.Count);
            return copyElements.Reverse().ToArray();
        }

        private void Grow()
        {
            var newElements = new T[this.elements.Length*2];
            Array.Copy(this.elements, newElements, this.Count);
            this.elements = newElements;
        }
    }
}
