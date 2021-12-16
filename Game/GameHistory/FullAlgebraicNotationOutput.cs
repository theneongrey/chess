using System.Text;

namespace GameLogic.GameHistory
{
    internal static class FullAlgebraicNotationOutput
    {
        private static string MoveToString(AGameMove move, bool wasPieceRemoved)
        {
            if (move is GameMoveCasteling casteling)
            {
                return casteling.IsQueenCasteling ? "O-O-O" : "O-O";
            }

            var moveConnector = wasPieceRemoved ? "x" : "-";
            if (move is not GameMoveMovePiece movePiece)
            {
                throw new InvalidDataException("GameMoveMovePiece is the only type that should be possible here.");
            }

            var piece = movePiece.PieceIdentifier;
            if (piece == Pieces.Pawn.Identifier)
            {
                piece = string.Empty;
            }

            return $"{piece}{GetCellNameFromPosition(movePiece.From)}{moveConnector}{GetCellNameFromPosition(movePiece.To)}";
        }

        private static string GetCellNameFromPosition(Position pos)
        {
            return char.ConvertFromUtf32('a' + pos.X) + (pos.Y + 1).ToString();
        }

        public static IEnumerable<string> GameStackToStringArray(GameStack gameStack)
        {
            var moves = gameStack.Moves;
            var step = 1;
            var output = new List<string>();
            var moveAsString = new StringBuilder();
            var isWhiteTurn = true;
            var wasPieceRemoved = false;

            foreach (var move in moves)
            {
                switch (move)
                {
                    case GameMoveRemovePiece _:
                        wasPieceRemoved = true;
                        continue;
                    case GameMovePromotePiece promotedPiece:
                        moveAsString.Append(promotedPiece.PromotedPieceIdentifier);
                        continue;
                    case GameMoveCheck _:
                        moveAsString.Append("+");
                        continue;
                    case GameMoveCheckMate _:
                        moveAsString.Append("#");
                        continue;
                }

                if (isWhiteTurn)
                {
                    if (moveAsString.Length > 0)
                    {
                        output.Add(moveAsString.ToString());
                        moveAsString.Clear();
                    }

                    moveAsString.Append($"{step}.{MoveToString(move, wasPieceRemoved)}");
                    step++;
                    isWhiteTurn = false;
                }
                else
                {
                    moveAsString.Append($" {MoveToString(move, wasPieceRemoved)}");
                    isWhiteTurn = true;
                }
                wasPieceRemoved = false;
            }

            if (moveAsString.Length > 0)
            {
                output.Add(moveAsString.ToString());
            }

            return output;
        }

        public static string GameStackToString(GameStack gameStack)
        {
            var moves = GameStackToStringArray(gameStack);
            return string.Join(Environment.NewLine, moves);
        }
    }
}
