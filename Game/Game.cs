using GameLogic.FieldParser;
using GameLogic.Pieces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GameLogic.Test")]
namespace GameLogic
{
    public class Game
    {
        private Field _field;
        private PieceColor _colorsTurn;
        private GameState _state;
        private APiece? _selectedPiece;

        public bool IsGameRunning => _state == GameState.GameRunning;

        public bool IsPieceSelectionActive => _state == GameState.PieceSelection;

        public bool IsGameOver => _state switch
            {
                GameState.BlackWon => true,
                GameState.WhiteWon => true,
                GameState.Draw => true,
                _ => false,
            };

        public Game()
        {
            var parser = new SingleBoardSimpleStringLayoutParser();
            _field = parser.CreateField(SingleBoardSimpleStringLayoutParser.DefaultLayout);
            _colorsTurn = PieceColor.White;
            _state = GameState.GameRunning;
        }

        private APiece? GetPieceIfItsPlayersTurn(Position cell)
        {
            var piece = _field.GetPieceAt(cell);
            if (piece == null || piece.Color != _colorsTurn)
            {
                return null;
            }

            return piece;
        }

        private void CheckForGameOver()
        {
            if (IsCheckMate())
            {
                if (_colorsTurn == PieceColor.White)
                {
                    _state = GameState.WhiteWon;
                }
                else
                {
                    _state = GameState.BlackWon;
                }
            }
        }

        private void CheckForPieceSelection()
        {
            if (_selectedPiece is PawnPiece pawn && 
                (pawn.Position.Y == 0 ||
                pawn.Position.Y == 7))
            {
                _state = GameState.PieceSelection;
            }
        }

        private void SwapTurns()
        {
            _selectedPiece = null;
            _colorsTurn = _colorsTurn == PieceColor.White ? PieceColor.Black : PieceColor.White;
        }

        public IEnumerable<Position> GetMovesForCell(Position cell)
        {
            if (!IsGameRunning)
            {
                return Enumerable.Empty<Position>();
            }

            var piece = GetPieceIfItsPlayersTurn(cell);
            if (piece == null)
            {
                return Enumerable.Empty<Position>();
            }

            return piece.GetAllowedMoves(_field);
        }

        public void SelectPiece(Position from)
        {
            if (IsGameRunning)
            {
                _selectedPiece = GetPieceIfItsPlayersTurn(from);
            }
        }

        private APiece? GetCapturedPiece(APiece piece, Position to)
        {
            // not a pawn, not the enpassant special case
            if (piece is not PawnPiece pawn)
            {
                return _field.GetPieceAt(to);
            }

            // moved forward, nothing was captured
            if (pawn.LastPosition.X - pawn.Position.X == 0)
            {
                return null;
            }

            // endposition makes sense for an enpassant and targetpositon is empty
            var pawnMovementDirection = pawn.Color == PieceColor.White ? 1 : -1;
            if (pawnMovementDirection == 1 && pawn.Position.Y == 5 ||
                pawnMovementDirection == -1 && pawn.Position.Y == 2 &&
                _field.GetPieceAt(to) == null)
            {
                return _field.GetPieceAt(new Position(to.X, to.Y - pawnMovementDirection));
            }

            return _field.GetPieceAt(to);
        }

        internal bool IsCheckMate()
        {
            return false;
        }

        private void ReplacePiece(APiece selectedPiece, APiece piece)
        {
            _field.RemovePiece(selectedPiece);
            _field.AddPiece(piece);
        }

        private bool MovePiece(APiece piece, Position to)
        {
            // to do: handle castling
            if (piece.IsMoveAllowed(_field, to))
            {
                _field.MovePiece(piece, to);

                APiece? capturedPiece = GetCapturedPiece(piece, to);
                if (capturedPiece != null)
                {
                    _field.RemovePiece(capturedPiece);
                }

                return true;
            }

            return false;
        }

        public bool TryMove(Position to)
        {
            if (!IsGameRunning ||
                _selectedPiece == null ||
                !MovePiece(_selectedPiece, to))
            {
                return false;
            }

            CheckForGameOver();

            if (IsGameRunning)
            {
                CheckForPieceSelection();

                if (!IsPieceSelectionActive)
                {
                    SwapTurns();
                }
            }

            return true;
        }

        public bool PerformPieceSelection(PieceSelectionType @type)
        {
            APiece? piece = type switch
            {
                PieceSelectionType.Queen => new QueenPiece(_selectedPiece!.Position, _colorsTurn),
                PieceSelectionType.Rook => new RookPiece(_selectedPiece!.Position, _colorsTurn),
                PieceSelectionType.Knight => new KnightPiece(_selectedPiece!.Position, _colorsTurn),
                PieceSelectionType.Bishop => new BishopPiece(_selectedPiece!.Position, _colorsTurn),
                _ => null
            };

            if (piece == null)
            {
                return false;
            }

            ReplacePiece(_selectedPiece!, piece);
            SwapTurns();
            _state = GameState.GameRunning;

            return true;
        }
    }
}
