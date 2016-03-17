namespace LongestPathInATree
{
    using System.Collections.Generic;

    public class Tree
    {
        public Tree(int value, params Tree[] children)
        {
            this.Value = value;
            this.Children = new List<Tree>();
            foreach (var child in children)
            {
                this.Children.Add(child);
                child.Parent = this;
            }
        }

        public int Value { get; private set; }

        public Tree Parent { get; set; }

        public IList<Tree> Children { get; set; }

        public int SumToTheRoot { get; set; }
    }
}
