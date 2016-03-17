namespace LinkedQueue
{
    using System;

    public class LinkedQueue<T>
    {
        private class QueueNode<T>
        {
            public QueueNode(T element)
            {
                this.Value = element;
            }

            public T Value { get; private set; }

            public QueueNode<T> PrevNode { get; set; }

            public QueueNode<T> NextNode { get; set; }
        }

        private QueueNode<T> head;

        private QueueNode<T> tail;  

        public int Count { get; private set; }

        public void Enqueue(T element)
        {
            if (this.Count == 0)
            {
                this.head = this.tail = new QueueNode<T>(element);
            }
            else
            {
                var newTail = new QueueNode<T>(element);
                this.tail.NextNode = newTail;
                newTail.PrevNode = this.tail;
                this.tail = newTail;
            }

            this.Count++;
        }

        public T Dequeue()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Empty queue.");
            }
            var element = this.head.Value;
            this.head = this.head.NextNode;
            if (this.head != null)
            {
                this.head.PrevNode = null;
            }
            else
            {
                this.tail = null;
            }
            
            this.Count--;
            return element;
        }

        public T[] ToArray()
        {
            var array = new T[this.Count];
            var currentElement = this.head;
            for (int i = 0; i < this.Count; i++)
            {
                array[i] = currentElement.Value;
                currentElement = currentElement.NextNode;
            }

            return array;
        }
    }
}
