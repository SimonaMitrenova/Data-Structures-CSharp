namespace BalancedOrderedSet
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class BalancedOrderedSet<T> : IEnumerable<T> where T : IComparable<T>
    {
        private Node<T> root;

        public BalancedOrderedSet()
        {
            this.root = null;
            this.Count = 0;
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            var inserted = true;
            if (this.root == null)
            {
                this.root = new Node<T>(item);
            }
            else
            {
                inserted = this.InsertInernal(this.root, item);
            }

            if (inserted)
            {
                this.Count++;
            }
        }

        private bool InsertInernal(Node<T> node, T item)
        {
            var currentNode = node;
            var newNode = new Node<T>(item);
            var shouldRetrace = false;
            while (true)
            {
                if (currentNode.Value.CompareTo(item) < 0)
                {
                    if (currentNode.RightChild == null)
                    {
                        currentNode.RightChild = newNode;
                        currentNode.BalanceFactor--;
                        shouldRetrace = currentNode.BalanceFactor != 0;
                        break;
                    }

                    currentNode = currentNode.RightChild;
                }
                else if (currentNode.Value.CompareTo(item) > 0)
                {
                    if (currentNode.LeftChild == null)
                    {
                        currentNode.LeftChild = newNode;
                        currentNode.BalanceFactor++;
                        shouldRetrace = currentNode.BalanceFactor != 0;
                        break;
                    }

                    currentNode = currentNode.LeftChild;
                }
                else
                {
                    return false;
                }
            }

            if (shouldRetrace)
            {
                this.RetraceInsert(currentNode);
            }

            return true;
        }

        private void RetraceInsert(Node<T> node)
        {
            var parent = node.Perant;
            while (parent != null)
            {
                if (node.IsLeftChild)
                {
                    if (parent.BalanceFactor == 1)
                    {
                        parent.BalanceFactor++;
                        if (node.BalanceFactor == -1)
                        {
                            this.RotateLeft(node);
                        }

                        this.RotateRight(parent);
                        break;
                    }
                    else if (parent.BalanceFactor == -1)
                    {
                        parent.BalanceFactor = 0;
                        break;
                    }
                    else
                    {
                        parent.BalanceFactor = 1;
                    }
                }
                else
                {
                    if (parent.BalanceFactor == -1)
                    {
                        parent.BalanceFactor--;
                        if (node.BalanceFactor == 1)
                        {
                            this.RotateRight(node);
                        }

                        this.RotateLeft(parent);
                        break;
                    }
                    else if (parent.BalanceFactor == 1)
                    {
                        parent.BalanceFactor = 0;
                        break;
                    }
                    else
                    {
                        parent.BalanceFactor = -1;
                    }
                }

                node = parent;
                parent = node.Perant;
            }
        }

        private void RotateRight(Node<T> node)
        {
            var parent = node.Perant;
            var child = node.LeftChild;
            if (parent != null)
            {
                if (node.IsLeftChild)
                {
                    parent.LeftChild = child;
                }
                else
                {
                    parent.RightChild = child;
                }
            }
            else
            {
                this.root = child;
                this.root.Perant = null;
            }

            node.LeftChild = child.RightChild;
            child.RightChild = node;

            node.BalanceFactor -= 1 + Math.Max(child.BalanceFactor, 0);
            child.BalanceFactor -= 1 - Math.Min(node.BalanceFactor, 0);
        }

        private void RotateLeft(Node<T> node)
        {
            var parent = node.Perant;
            var child = node.RightChild;
            if (parent != null)
            {
                if (node.IsLeftChild)
                {
                    parent.LeftChild = child;
                }
                else
                {
                    parent.RightChild = child;
                }
            }
            else
            {
                this.root = child;
                this.root.Perant = null;
            }

            node.RightChild = child.LeftChild;
            child.LeftChild = node;

            node.BalanceFactor += 1 - Math.Min(child.BalanceFactor, 0);
            child.BalanceFactor += 1 + Math.Max(node.BalanceFactor, 0);
        }

        public bool Contains(T item)
        {
            var node = this.FindNode(item);
            if (node != null)
            {
                return true;
            }

            return false;
        }

        private Node<T> FindNode(T item)
        {
            var node = this.root;
            while (node != null)
            {
                if (node.Value.Equals(item))
                {
                    return node;
                }
                else if (node.Value.CompareTo(item) < 0)
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

        public bool Remove(T item)
        {
            var node = this.FindNode(item);
            if (node == null)
            {
                return false;
            }

            this.Delete(node);
            this.Count--;
            return true;
        }

        private void Delete(Node<T> node)
        {
            var leftChild = node.LeftChild;
            var rightChild = node.RightChild;
            if (leftChild == null)
            {
                if (rightChild == null)
                {
                    if (node == this.root)
                    {
                        this.root = null;
                    }
                    else
                    {
                        var parent = node.Perant;
                        if (node.IsLeftChild)
                        {
                            parent.LeftChild = null;
                            this.DeleteBalanceFactor(parent, -1);
                        }
                        else
                        {
                            parent.RightChild = null;
                            this.DeleteBalanceFactor(parent, 1);
                        }
                    }
                }
                else
                {
                    this.Replace(node, rightChild);
                    this.DeleteBalanceFactor(node, 0);
                }
            }
            else if (rightChild == null)
            {
                this.Replace(node, leftChild);
                this.DeleteBalanceFactor(node, 0);
            }
            else
            {
                var successor = rightChild;
                if (successor.LeftChild == null)
                {
                    var parent = node.Perant;
                    successor.Perant = parent;
                    successor.LeftChild = leftChild;
                    successor.BalanceFactor = node.BalanceFactor;
                    if (node == this.root)
                    {
                        this.root = successor;
                    }
                    else
                    {
                        if (node.IsLeftChild)
                        {
                            parent.LeftChild = successor;
                        }
                        else
                        {
                            parent.RightChild = successor;
                        }
                    }
                    this.DeleteBalanceFactor(successor, 1);
                }
                else
                {
                    while (successor.LeftChild != null)
                    {
                        successor = successor.LeftChild;
                    }

                    var parent = node.Perant;
                    var successorParent = successor.Perant;
                    var successorRight = successor.RightChild;
                    if (successor.IsLeftChild)
                    {
                        successorParent.LeftChild = successorRight;
                    }
                    else
                    {
                        successorParent.RightChild = successorRight;
                    }

                    successor.Perant = parent;
                    successor.LeftChild = leftChild;
                    successor.BalanceFactor = node.BalanceFactor;
                    successor.RightChild = rightChild;

                    if (node == this.root)
                    {
                        this.root = successor;
                    }
                    else
                    {
                        if (node.IsLeftChild)
                        {
                            parent.LeftChild = successor;
                        }
                        else
                        {
                            parent.RightChild = successor;
                        }
                    }

                    this.DeleteBalanceFactor(successorParent, -1);
                }
            }
        }

        private void Replace(Node<T> node, Node<T> child)
        {
            var left = child.LeftChild;
            var right = child.RightChild;

            node.BalanceFactor = child.BalanceFactor;
            node.Value = child.Value;
            node.LeftChild = left;
            node.RightChild = right;
        }

        private void DeleteBalanceFactor(Node<T> node, int balance)
        {
            throw new NotImplementedException();
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
