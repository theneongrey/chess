namespace ChessApi.Model
{
    public class CellPosition
    {
        public string Value { get; }
        public int PosX { get; }
        public int PosY { get; }

        private CellPosition(string value)
        {
            if (!IsCellPosition(value))
            {
                throw new ArgumentException("\"{value}\" is no valid cell position");
            }

            Value = value;
            var horizontalPosition = char.ToLower(value[0]);
            var verticalPosition = value[1];
            PosX = horizontalPosition - 'a';
            PosY = verticalPosition - '1';
        }

        public static CellPosition FromString(string position)
        {
            return new CellPosition(position);
        }

        public static bool TryFromString(string position, out CellPosition? value)
        {
            if (IsCellPosition(position))
            {
                value = new CellPosition(position);
                return true;
            }

            value = default;
            return false;
        }

        public static bool IsCellPosition(string position)
        {
            if (string.IsNullOrEmpty(position) || position.Length != 2)
            {
                return false;
            }

            var horizontalPosition = char.ToLower(position[0]);
            if (horizontalPosition < 'a' || horizontalPosition > 'h')
            {
                return false;
            }

            var verticalPosition = position[1];
            if (verticalPosition < '1' || verticalPosition > '8')
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
