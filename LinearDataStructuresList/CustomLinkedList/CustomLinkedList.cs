namespace CustomLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CustomLinkedList<T> : IEnumerable<T>
    {
        private class ListNode<T>
        {
            public ListNode(T value)
            {
                this.Value = value;
            }

            public T Value { get; private set; }

            public ListNode<T> NextNode { get; set; }
        }

        private ListNode<T> head;

        private ListNode<T> tail;

        public int Count { get; private set; }

        public void Add(T element)
        {
            if (this.Count == 0)
            {
                this.head = this.tail = new ListNode<T>(element);
            }
            else
            {
                var newTail = new ListNode<T>(element);
                this.tail.NextNode = newTail;
                this.tail = newTail;
            }

            this.Count++;
        }

        public T Remove(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException("Invalid index.");
            }

            if (this.Count == 0)
            {
                throw new InvalidOperationException("Empty list.");
            }

            if (this.Count == 1)
            {
                var element = this.head.Value;
                this.head = this.tail = null;
                return element;
            }

            ListNode<T> currentNode = this.head;
            ListNode<T> prevNode = null;
            for (int i = 0; i < index; i++)
            {
                prevNode = currentNode;
                currentNode = currentNode.NextNode;
            }

            if (prevNode == null)
            {
                var element = currentNode.Value;
                this.head = currentNode.NextNode;
                return element;
            }

            prevNode.NextNode = currentNode.NextNode;
            ListNode<T> lastElement = null;
            if (this.head != null)
            {
                lastElement = this.head;
                while (lastElement.NextNode != null)
                {
                    lastElement = lastElement.NextNode;
                }
            }

            this.tail = lastElement;
            this.Count--;
            return currentNode.Value;
        }

        public int FirstIndexOf(T item) 
        {
            int count = -1;
            var currentElement = this.head;
            while (currentElement != null)
            {
                count++;
                if (item.Equals(currentElement.Value))
                {
                    return count;
                }
                currentElement = currentElement.NextNode;
            }

            return count;
        }

        public int LastIndexOf(T item)
        {
            int count = -1;
            int index = -1;
            var currentElement = this.head;
            while (currentElement != null)
            {
                count++;
                if (item.Equals(currentElement.Value))
                {
                    index = count;
                }

                currentElement = currentElement.NextNode;
            }

            return index;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentElement = this.head;
            while (currentElement != null)
            {
                yield return currentElement.Value;
                currentElement = currentElement.NextNode;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
