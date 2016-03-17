using System;

public class CircularQueue<T>
{
    private const int InitialCapacity = 16;
    private T[] array;
    private int startIndex = 0;
    private int endIndex = 0;

    public int Count { get; private set; }

    public CircularQueue() : this(InitialCapacity)
    {
    }

    public CircularQueue(int capacity)
    {
        this.array = new T[capacity];
    }

    public void Enqueue(T element)
    {
        if (this.Count >= this.array.Length)
        {
            this.Resize();
        }

        this.array[this.endIndex] = element;
        this.endIndex = (this.endIndex + 1) % this.array.Length;
        this.Count++;
    }

    private void Resize()
    {
        var newArray = new T[this.array.Length * 2];
        this.CopyAllElementsTo(newArray);
        this.array = newArray;
        this.startIndex = 0;
        this.endIndex = this.Count;
    }

    private void CopyAllElementsTo(T[] resultArray)
    {
        int sourceIndex = this.startIndex;
        int destinationIndex = 0;
        for (int i = 0; i < this.Count; i++)
        {
            resultArray[destinationIndex] = this.array[sourceIndex];
            sourceIndex = (sourceIndex + 1) % this.array.Length;
            destinationIndex++;
        }
    }

    public T Dequeue()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("The queue is empty.");
        }

        var result = this.array[this.startIndex];
        this.array[this.startIndex] = default(T);
        this.startIndex = (this.startIndex + 1) % this.array.Length;
        this.Count--;
        return result;
    }

    public T[] ToArray()
    {
        var copyArray = new T[this.Count];
        this.CopyAllElementsTo(copyArray);

        return copyArray;
    }
}


class Example
{
    static void Main()
    {
        var queue = new CircularQueue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Enqueue(5);
        queue.Enqueue(6);

        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        var first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-7);
        queue.Enqueue(-8);
        queue.Enqueue(-9);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-10);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");
    }
}
