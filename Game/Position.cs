namespace GameLogic
{
    public readonly record struct Position
    {
        public int X { get; }
        public int Y { get; }
        internal int AsCellIndex { get; }

        public Position(int x, int y)
        {
            if (x < 0 || x > 7)
            {
                throw new ArgumentException($"Illegal cell position for x at {x}");
            }
            if (y < 0 || y > 7)
            {
                throw new ArgumentException($"Illegal cell position for y at {y}");
            }

            X = x;
            Y = y;
            AsCellIndex = Y * 8 + x;
        }

        public override string ToString()
        {
            return $"X={X}, Y={Y}";
        }

        public string AsCellName()
        {
            return char.ConvertFromUtf32('A' + X) + (Y + 1);
        }
    }
}
