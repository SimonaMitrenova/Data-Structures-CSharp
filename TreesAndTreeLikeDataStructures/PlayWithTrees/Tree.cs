namespace PlayWithTrees
{
    using System.Collections.Generic;

    public class Tree<T>
    {
        //private long? subTreeSum;

        public Tree(T value, params Tree<T>[] children)
        {
            this.Value = value;
            this.Children = new List<Tree<T>>();
            foreach (var child in children)
            {
                this.Children.Add(child);
                child.Parent = this;
            }
        }

        public T Value { get; private set; }

        public Tree<T> Parent { get; set; }

        public IList<Tree<T>> Children { get; private set; }

        //public long SubTreeSum
        //{
        //    get
        //    {
        //        if (this.subTreeSum == null)
        //        {
        //            this.CalculateSubTreeSum();
        //        }

        //        return this.subTreeSum.Value;
        //    }
        //}

        //private void CalculateSubTreeSum()
        //{
        //    this.subTreeSum = 0L;
        //    this.subTreeSum += this.Value;
        //}
    }
}
