using GameLogic.BasicMovements;
using GameLogic.InternPieces;

namespace GameLogic.CheckTester
{
    internal static class CheckTest
    {
        private static IBasicMovement _starMovement = new BasicMovementCollection(new DiagonalMovement(), new HorizontalVerticalMovement());
        private static IBasicMovement _jumpMovement = new JumpMovement();

        public static bool WillKingBeInDanger(Board field, APiece piece, Position moveTo)
        {
            var pieceCurrentPos = piece.Position;
            var pieceAtTargetcell = field.GetPieceAt(moveTo);
            field.SetPieceAt(piece, moveTo);

            var result = IsKingInDanger(field, piece.Color);

            field.SetPieceAt(piece, pieceCurrentPos);
            if (pieceAtTargetcell != null)
            {
                field.SetPieceAt(pieceAtTargetcell, moveTo);
            }

            return result;
        }

        public static bool IsKingInDanger(Board field, PieceColor currentTurn)
        {
            var currentTurnKing = field.GetPiecesByTypeAndColor<KingPiece>(currentTurn).FirstOrDefault();
            if (currentTurnKing == null)
            {
                return false;
            }

            return IsKingInDanger(field, currentTurnKing);
        }

        private static bool IsKingInDanger(Board field, KingPiece king)
        {
            if (CanKingBeAttackedByAKnight(field, king))
            {
                return true;
            }

            if (CanKingBeAttackedByAnotherPiece(field, king))
            {
                return true;
            }

            return false;
        }

        private static bool CanKingBeAttackedByAnotherPiece(Board field, KingPiece king)
        {
            var positions = _starMovement.GetAllowedPositions(king.Position);
            foreach (var positionCollection in positions)
            {
                foreach(var position in positionCollection)
                {
                    var piece = field.GetPieceAt(position);
                    if (piece != null && piece.Color != king.Color && piece.IsTargetPositionAllowed(field, king.Position))
                    {
                        return true;
                    } 
                }
            }

            return false;
        }

        private static bool CanKingBeAttackedByAKnight(Board field, KingPiece king)
        {
            var possiblePositions = _jumpMovement.GetAllowedPositions(king.Position);

            foreach (var possiblePositionCollection in possiblePositions)
            {
                var possiblePosition = possiblePositionCollection.First();
                if (field.GetPieceAt(possiblePosition) is KnightPiece knight && knight.Color != king.Color)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
