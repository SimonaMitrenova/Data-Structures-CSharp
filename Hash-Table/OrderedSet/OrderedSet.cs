namespace OrderedSet
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class OrderedSet<T> : IEnumerable<T> where T : IComparable<T>
    {
        private Node<T> root;

        public OrderedSet()
        {
            this.root = null;
            this.Count = 0;
        }

        public int Count { get; private set; }

        public void Add(T element)
        {
            bool inserted = true;
            if (this.root == null)
            {
                this.root = new Node<T>(element);
            }
            else
            {
                inserted = this.InserdInternal(this.root, element);
            }

            if (inserted)
            {
                this.Count++;
            }
        }

        private bool InserdInternal(Node<T> node, T element)
        {
            var currentNode = node;
            var newNode = new Node<T>(element);
            while (true)
            {
                if (currentNode.Value.CompareTo(element) < 0)
                {
                    if (currentNode.RightChild == null)
                    {
                        currentNode.RightChild = newNode;
                        break;
                    }

                    currentNode = currentNode.RightChild;
                }
                else if (currentNode.Value.CompareTo(element) > 0)
                {
                    if (currentNode.LeftChild == null)
                    {
                        currentNode.LeftChild = newNode;
                        break;
                    }

                    currentNode = currentNode.LeftChild;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public bool Contains(T element)
        {
            var node = this.FindNode(element);
            if (node != null)
            {
                return true;
            }

            return false;
        }

        private Node<T> FindNode(T element)
        {
            var node = this.root;
            while (node != null)
            {
                if (node.Value.Equals(element))
                {
                    return node;
                }
                else if (node.Value.CompareTo(element) < 0)
                {
                    node = node.RightChild;
                }
                else
                {
                    node = node.LeftChild;
                }
            }

            return null;
        }

        public bool Remove(T element)
        {
            // Its place should be taken by the bigger child node.
            var nodeToRemove = this.FindNode(element);
            if (nodeToRemove == null)
            {
                return false;
            }

            this.Delete(nodeToRemove);
            this.Count--;
            return true;
        }

        private void Delete(Node<T> nodeToDelete)
        {
            // node to delete has two children
            if (nodeToDelete.LeftChild != null && nodeToDelete.RightChild != null)
            {
                Node<T> replacement = nodeToDelete.RightChild;
                while (replacement.LeftChild != null)
                {
                    replacement = replacement.LeftChild;
                }
                nodeToDelete.Value = replacement.Value;
                nodeToDelete = replacement;
            }

            // node to delete has one child
            Node<T> child = nodeToDelete.LeftChild ?? nodeToDelete.RightChild;
            if (child != null)
            {
                child.Parent = nodeToDelete.Parent;

                if (nodeToDelete.Parent.Parent == null)
                {
                    this.root = child;
                }
                else
                {
                    if (nodeToDelete.Parent.LeftChild == nodeToDelete)
                    {
                        nodeToDelete.Parent.LeftChild = child;
                    }
                    else
                    {
                        nodeToDelete.Parent.RightChild = child;
                    }
                }
            }
            // node to delete has no children
            else
            {
                if (nodeToDelete.Parent == null)
                {
                    this.root = null;
                }
                else
                {
                    if (nodeToDelete.Parent.LeftChild == nodeToDelete)
                    {
                        nodeToDelete.Parent.LeftChild = null;
                    }
                    else
                    {
                        nodeToDelete.Parent.RightChild = null;
                    }
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (this.root == null)
            {
                return Enumerable.Empty<T>().GetEnumerator();
            }

            return this.root.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
