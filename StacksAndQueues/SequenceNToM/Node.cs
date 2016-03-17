namespace SequenceNToM
{
    public class Node<T>
    {
        public Node(T value, Node<T> prevNode = null)
        {
            this.Value = value;
            this.PrevNode = prevNode;
        }

        public T Value { get; private set; }

        public Node<T> PrevNode { get; private set; }
    }
}
