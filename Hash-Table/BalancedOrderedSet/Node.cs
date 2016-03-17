namespace BalancedOrderedSet
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Node<T> : IEnumerable<T> where T : IComparable<T>
    {
        private Node<T> leftChild;
        private Node<T> rightChild;
          
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }

        public Node<T> LeftChild
        {
            get
            {
                return this.leftChild;
            }
            set
            {
                if (value != null)
                {
                    value.Perant = this;
                }

                this.leftChild = value;
            }
        }

        public Node<T> RightChild
        {
            get
            {
                return this.rightChild;
            }
            set
            {
                if (value != null)
                {
                    value.Perant = this;
                }

                this.rightChild = value;
            }
        }

        public Node<T> Perant { get; set; }

        public int BalanceFactor { get; set; }

        public bool IsLeftChild
        {
            get
            {
                return this.Perant.LeftChild == this;
            }
        }

        public bool IsRightChild
        {
            get
            {
                return this.Perant.RightChild == this;
            }
        }

        public int ChildrenCount
        {
            get
            {
                var childrenCount = 0;
                if (this.LeftChild != null)
                {
                    childrenCount++;
                }
                if (this.RightChild != null)
                {
                    childrenCount++;
                }

                return childrenCount;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (this.LeftChild != null)
            {
                foreach (var item in this.LeftChild)
                {
                    yield return item;
                }
            }

            yield return this.Value;

            if (this.RightChild != null)
            {
                foreach (var item in this.RightChild)
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
