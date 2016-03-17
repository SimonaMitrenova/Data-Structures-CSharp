namespace AvlTreeLab
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AvlTree<T> where T : IComparable
    {
        private Node<T> root;

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
                inserted = this.InsertInternal(this.root, item);
            }

            if (inserted)
            {
                this.Count++;
            }
        }

        private bool InsertInternal(Node<T> node, T item)
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

            this.RetraceNodeCount(currentNode);
            return true;
        }

        private void RetraceNodeCount(Node<T> node)
        {
            var currentNode = node;
            while (currentNode != null)
            {
                var leftCount = currentNode.LeftChild?.NodeCount ?? 0;
                var rightCount = currentNode.RightChild?.NodeCount ?? 0;
                currentNode.NodeCount = leftCount + 1 + rightCount;

                currentNode = currentNode.Parent;
            }
        }

        private void RetraceInsert(Node<T> node)
        {
            var parent = node.Parent;
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
                parent = node.Parent;
            }
        }

        private void RotateRight(Node<T> node)
        {
            var parent = node.Parent;
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
                this.root.Parent = null;
            }

            node.LeftChild = child.RightChild;
            child.RightChild = node;

            node.BalanceFactor += -1 + Math.Max(child.BalanceFactor, 0);
            child.BalanceFactor += -1 - Math.Min(node.BalanceFactor, 0);

            node.NodeCount = (node.LeftChild?.NodeCount?? 0) + 1 + (node.RightChild?.NodeCount ?? 0);
            child.NodeCount = (child.LeftChild?.NodeCount ?? 0) + 1 + child.RightChild.NodeCount;
        }

        private void RotateLeft(Node<T> node)
        {
            var parent = node.Parent;
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
                this.root.Parent = null;
            }

            node.RightChild = child.LeftChild;
            child.LeftChild = node;

            node.BalanceFactor += 1 - Math.Min(child.BalanceFactor, 0);
            child.BalanceFactor += 1 + Math.Max(node.BalanceFactor, 0);

            node.NodeCount = (node.LeftChild?.NodeCount ?? 0) + 1 + (node.RightChild?.NodeCount ?? 0);
            child.NodeCount = child.LeftChild.NodeCount + 1 + (child.RightChild?.NodeCount ?? 0);
        }

        public bool Contains(T item)
        {
            var node = this.root;
            while (node != null)
            {
                if (node.Value.Equals(item))
                {
                    return true;
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

            return false;
        }

        public void ForeachDfs(Action<int, T> action)
        {
            if (this.Count == 0)
            {
                return;
            }

            this.InOrderDfs(this.root, 1, action);
        }

        public IEnumerable<T> Range(T from, T to)
        {
            if (from.CompareTo(to) > 0)
            {
                throw new ArgumentException("First item must be smaller than second.");
            }
            if (this.root == null)
            {
                return Enumerable.Empty<T>();
            }
            var itemsInRange = new List<T>();
            this.InOrderDfs(this.root, itemsInRange, from, to);
            return itemsInRange;
        }

        private void InOrderDfs(Node<T> node, ICollection<T> itemsInRange, T from, T to)
        {
            var currentNode = node;
            var isGreaterThanFrom = node.Value.CompareTo(from) >= 0;
            var isSmallerThanTo = node.Value.CompareTo(to) <= 0;
            if (currentNode.LeftChild != null && isGreaterThanFrom)
            {
                this.InOrderDfs(currentNode.LeftChild, itemsInRange, from, to);
            }

            if (isSmallerThanTo && isGreaterThanFrom)
            {
                itemsInRange.Add(currentNode.Value);
            }

            if (currentNode.RightChild != null && isSmallerThanTo)
            {
                this.InOrderDfs(currentNode.RightChild, itemsInRange, from, to);
            }
        }

        private void InOrderDfs(Node<T> node, int depth, Action<int, T> action)
        {
            if (node.LeftChild != null)
            {
                this.InOrderDfs(node.LeftChild, depth + 1, action);
            }

            action(depth, node.Value);

            if (node.RightChild != null)
            {
                this.InOrderDfs(node.RightChild, depth + 1, action);
            }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                {
                    throw new IndexOutOfRangeException("Invalid index.");
                }

                var node = this.root;
                int count = index;

                while (node != null)
                {
                    int sizeOfLeftSubTree = node.LeftChild?.NodeCount ?? 0;
                    if (count == sizeOfLeftSubTree)
                    {
                        return node.Value;
                    }
                    else if (sizeOfLeftSubTree < count)
                    {
                        node = node.RightChild;
                        count -= (sizeOfLeftSubTree + 1);
                    }
                    else
                    {
                        node = node.LeftChild;
                    }
                }

                throw new IndexOutOfRangeException("Invalid index.");
            }
        }
    }
}
