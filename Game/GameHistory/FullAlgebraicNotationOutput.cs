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

            var piece = movePiece.PieceIdentifier.ToUpper();
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

        public static string GameStackToString(GameStack gameStack)
        {
            var moves = gameStack.Moves;
            var step = 1;
            var output = new StringBuilder();
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
                        output.Append(promotedPiece.PromotedPieceIdentifier);
                        continue;
                    case GameMoveCheck _:
                        output.Append("+");
                        continue;
                    case GameMoveCheckMate _:
                        output.Append("#");
                        continue;
                }

                if (isWhiteTurn)
                {
                    output.Append($"{Environment.NewLine}{step}.{MoveToString(move, wasPieceRemoved)}");
                    step++;
                    isWhiteTurn = false;
                }
                else
                {
                    output.Append($" {MoveToString(move, wasPieceRemoved)}");
                    isWhiteTurn = true;
                }
                wasPieceRemoved = false;
            }

            return output.ToString().Trim();
        }
    }
}
