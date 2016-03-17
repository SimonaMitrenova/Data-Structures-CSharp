namespace Sorting
{
    using System;

    public class Permutation<T> where T : IComparable
    {
        public Permutation(T[] sequence, int stepsToOrder = 0)
        {
            this.Sequence = sequence;
            this.StepsToOrder = stepsToOrder;
        }

        public T[] Sequence { get; set; }

        public int StepsToOrder { get; set; }

        public bool IsOrdered()
        {
            for (int i = 0; i < this.Sequence.Length - 1; i++)
            {
                if (this.Sequence[i].CompareTo(this.Sequence[i + 1]) > 0)
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            return string.Join(string.Empty, this.Sequence);
        }
    }
}
