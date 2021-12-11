using GameLogic.CheckTester;
using GameLogic.FieldParser;
using GameLogic.GameHistory;
using GameLogic.Pieces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GameLogic.Test")]
namespace GameLogic
{
    public class Game
    {
        private Board _field;
        private PieceColor _colorsTurn;
        private GameState _state;
        private APiece? _selectedPiece;
        private GameStack _gameHistory;

        public bool IsGameRunning => _state == GameState.GameRunning;
        public bool IsPieceSelectionActive => _state == GameState.PieceSelection;

        public bool IsGameOver => _state switch
            {
                GameState.BlackWon => true,
                GameState.WhiteWon => true,
                GameState.Draw => true,
                _ => false,
            };

        public bool IsCheckPending { get; private set; }

        public Game()
        {
            var parser = new SingleBoardSimpleStringLayoutParser();
            _field = parser.CreateField(SingleBoardSimpleStringLayoutParser.DefaultLayout);
            _colorsTurn = PieceColor.White;
            _state = GameState.GameRunning;
            _gameHistory = new GameStack();
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
            // not a pawn -> not the enpassant special case
            if (piece is not PawnPiece pawn)
            {
                return _field.GetPieceAt(to);
            }

            // moved forward, nothing was captured
            if (pawn.Position.X == to.X)
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
            var pieces = _field.GetPiecesByColor(_colorsTurn == PieceColor.White ? PieceColor.Black : PieceColor.White);
            foreach (var piece in pieces)
            {
                if (piece.CanMove(_field))
                {
                    return false;
                }
            }

            return true;
        }

        private void PerformMove(AGameMove move)
        {
            _gameHistory.AddAndRunMove(_field, move);
        }

        private bool HandleCasteling(APiece piece, Position to)
        {
            if (piece.WasMoved)
            {
                return false;
            }

            if (piece is KingPiece)
            {
                if (to.X - piece.Position.X == 2)
                {
                    var rook = _field.GetPieceAt(new Position(7, piece.Position.Y));
                    PerformMove(new MovePiece(rook!, new Position(4, piece.Position.Y)));
                    return true;
                }
                else if (piece.Position.X - to.X == 2)
                {
                    var rook = _field.GetPieceAt(new Position(0, piece.Position.Y));
                    PerformMove(new MovePiece(rook!, new Position(4, piece.Position.Y)));
                    return true;
                }
            }
            else if (piece is RookPiece && to.X == 4)
            {
                if (_field.GetPieceAt(new Position(4, piece.Position.Y)) is KingPiece king && 
                    !king.WasMoved)
                {
                    var direction = piece.Position.X == 0 ? -1 : 1;
                    PerformMove(new MovePiece(king, new Position(4 + (2 * direction), piece.Position.Y)));
                }   
            }

            return false;
        }

        private bool MovePiece(APiece piece, Position to)
        {
            if (piece.IsMoveAllowed(_field, to))
            {
                var wasACastelingMove = HandleCasteling(piece, to);

                if (!wasACastelingMove)
                {
                    APiece? capturedPiece = GetCapturedPiece(piece, to);
                    if (capturedPiece != null)
                    {
                        PerformMove(new RemovePiece(capturedPiece));
                    }
                }

                PerformMove(new MovePiece(piece, to));

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
                    IsCheckPending = CheckTest.IsKingInDanger(_field, _colorsTurn);
                }
            }

            return true;
        }

        public bool PerformPromotion(Piece @type)
        {
            var pieceAssignment = new Dictionary<Piece, Func<APiece>>()
            {
                { TradablePieces.Queen, () => new QueenPiece(_selectedPiece!.Position, _colorsTurn) },
                { TradablePieces.Knight, () => new KnightPiece(_selectedPiece!.Position, _colorsTurn) },
                { TradablePieces.Rook, () => new RookPiece(_selectedPiece!.Position, _colorsTurn) },
                { TradablePieces.Bishop, () => new BishopPiece(_selectedPiece!.Position, _colorsTurn) },
            };

            pieceAssignment.TryGetValue(@type, out var piece);

            if (piece == null)
            {
                return false;
            }

            PerformMove(new PromotePiece(_selectedPiece!, piece.Invoke()));
            SwapTurns();
            _state = GameState.GameRunning;

            return true;
        }
    }
}
