using GameLogic.BasicMovements;
using GameLogic.Pieces;

namespace GameLogic.CheckTester
{
    internal static class CheckTest
    {
        private static IBasicMovement _starMovement = new BasicMovementCollection(new DiagonalMovement(), new HorizontalVerticalMovement());
        private static IBasicMovement _jumpMovement = new JumpMovement();

        public static bool IsKingInDanger(APiece[] cells, PieceColor currentTurn)
        {
            var currentTurnKing = cells.FirstOrDefault(c => c is KingPiece && c.Color == currentTurn);
            if (currentTurnKing == null)
            {
                return false;
            }

            return IsKingInDanger(cells, (KingPiece)currentTurnKing);
        }

        private static bool IsKingInDanger(APiece[] cells, KingPiece king)
        {
            if (CanKingBeAttackedByAKnight(cells, king))
            {
                return true;
            }

            if (CanKingBeAttackedByAnotherPiece(cells, king))
            {
                return true;
            }

            return false;
        }

        private static bool CanKingBeAttackedByAnotherPiece(APiece[] cells, KingPiece king)
        {
            var positions = _starMovement.GetAllowedPositions(king.Position);
            foreach (var position in positions)
            {

            }

            return true;
        }

        private static bool CanKingBeAttackedByAKnight(APiece[] cells, KingPiece king)
        {
            var possiblePositions = _jumpMovement.GetAllowedPositions(king.Position);

            foreach (var possiblePositionCollection in possiblePositions)
            {
                var possiblePosition = possiblePositionCollection.First();
                if (cells[possiblePosition.AsCellIndex] is KnightPiece knight && knight.Color != king.Color)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
