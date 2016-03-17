namespace RideTheHorse
{
    public class Cell
    {
        public Cell(int row, int col, int step)
        {
            this.Row = row;
            this.Col = col;
            this.Step = step;
        }

        public int Row { get; private set; }

        public int Col { get; private set; }

        public int Step { get; private set; }
    }
}
