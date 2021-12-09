namespace GameLogic
{
    public readonly record struct Position
    {
        public int X { get; }
        public int Y { get; }
        public int AsCellIndex { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
            AsCellIndex = Y * 8 + x;
        }


        public override string ToString()
        {
            return $"X={X}, Y={Y}";
        }
    }
}
