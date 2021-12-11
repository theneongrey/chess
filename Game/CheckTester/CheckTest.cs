using GameLogic.BasicMovements;
using GameLogic.InternPieces;

namespace GameLogic.CheckTester
{
    internal static class CheckTest
    {
        private static IBasicMovement _starMovement = new BasicMovementCollection(new DiagonalMovement(), new HorizontalVerticalMovement());
        private static IBasicMovement _jumpMovement = new JumpMovement();

        public static bool WillKingBeInDanger(Board board, APiece piece, Position moveTo)
        {
            var pieceCurrentPos = piece.Position;
            var pieceAtTargetcell = board.GetPieceAt(moveTo);
            board.SetPieceAt(piece, moveTo);

            var result = IsKingInDanger(board, piece.Color);

            board.SetPieceAt(piece, pieceCurrentPos);
            if (pieceAtTargetcell != null)
            {
                board.SetPieceAt(pieceAtTargetcell, moveTo);
            }

            return result;
        }

        public static bool IsKingInDanger(Board board, PieceColor currentTurn)
        {
            var currentTurnKing = board.GetPiecesByTypeAndColor<KingPiece>(currentTurn).FirstOrDefault();
            if (currentTurnKing == null)
            {
                return false;
            }

            return IsKingInDanger(board, currentTurnKing);
        }

        private static bool IsKingInDanger(Board board, KingPiece king)
        {
            if (CanKingBeAttackedByAKnight(board, king))
            {
                return true;
            }

            if (CanKingBeAttackedByAnotherPiece(board, king))
            {
                return true;
            }

            return false;
        }

        private static bool CanKingBeAttackedByAnotherPiece(Board board, KingPiece king)
        {
            var positions = _starMovement.GetAllowedPositions(king.Position);
            foreach (var positionCollection in positions)
            {
                foreach(var position in positionCollection)
                {
                    var piece = board.GetPieceAt(position);
                    if (piece != null && piece.Color != king.Color && piece.IsTargetPositionAllowed(board, king.Position))
                    {
                        return true;
                    } 
                }
            }

            return false;
        }

        private static bool CanKingBeAttackedByAKnight(Board board, KingPiece king)
        {
            var possiblePositions = _jumpMovement.GetAllowedPositions(king.Position);

            foreach (var possiblePositionCollection in possiblePositions)
            {
                var possiblePosition = possiblePositionCollection.First();
                if (board.GetPieceAt(possiblePosition) is KnightPiece knight && knight.Color != king.Color)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
