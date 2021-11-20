namespace GameLogic
{
    public readonly record struct Position(int X, int Y)
    {
        public override string ToString()
        {
            return $"X={X}, Y={Y}";
        }
    }
}
