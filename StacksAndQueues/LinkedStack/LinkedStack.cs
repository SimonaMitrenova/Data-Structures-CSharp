namespace LinkedStack
{
    using System;

    public class LinkedStack<T>
    {
        private class Node<T>
        {
            public Node(T value, Node<T> nextNode = null)
            {
                this.Value = value;
                this.NextNode = nextNode;
            }

            public T Value { get; private set; }

            public Node<T> NextNode { get; private set; }
        }

        private Node<T> firstNode;

        public int Count { get; private set; }

        public void Push(T element)
        {
            var newNode = new Node<T>(element, this.firstNode);
            this.firstNode = newNode;
            this.Count++;
        }

        public T Pop()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Empty stack");
            }

            var element = this.firstNode.Value;
            this.firstNode = this.firstNode.NextNode;
            this.Count--;
            return element;
        }

        public T[] ToArray()
        {
            var array = new T[this.Count];
            var currentElement = this.firstNode;
            for (int i = 0; i < this.Count; i++)
            {
                array[i] = currentElement.Value;
                currentElement = currentElement.NextNode;
            }

            return array;
        }
    }
}
