using GameLogic.CheckTester;
using GameLogic.BoardParser;
using GameLogic.GameHistory;
using GameLogic.InternPieces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GameLogic.Test")]
namespace GameLogic
{
    public class Game
    {
        private Board _board;
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

        public Game() : this(SimpleBoardParser.DefaultLayout)
        {
        }

        private Game(string board)
        {
            var parser = new SimpleBoardParser();
            _board = parser.CreateBoard(board);
            _colorsTurn = PieceColor.White;
            _state = GameState.GameRunning;
            _gameHistory = new GameStack();

        }

        public static Game FromString(string board)
        {
            return new Game(board);
        }

        private APiece? GetPieceIfItsPlayersTurn(Position cell)
        {
            var piece = _board.GetPieceAt(cell);
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

            return piece.GetAllowedMoves(_board);
        }

        public Piece? SelectPiece(Position from)
        {
            if (IsGameRunning)
            {
                _selectedPiece = GetPieceIfItsPlayersTurn(from);
                return _selectedPiece?.PieceType;
            }

            return null;
        }

        private APiece? GetCapturedPiece(APiece piece, Position to)
        {
            // not a pawn -> not the enpassant special case
            if (piece is not PawnPiece pawn)
            {
                return _board.GetPieceAt(to);
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
                _board.GetPieceAt(to) == null)
            {
                return _board.GetPieceAt(new Position(to.X, to.Y - pawnMovementDirection));
            }

            return _board.GetPieceAt(to);
        }

        internal bool IsCheckMate()
        {
            var pieces = _board.GetPiecesByColor(_colorsTurn == PieceColor.White ? PieceColor.Black : PieceColor.White);
            foreach (var piece in pieces)
            {
                if (piece.CanMove(_board))
                {
                    return false;
                }
            }

            return true;
        }

        private void PerformMove(AGameMove move)
        {
            _gameHistory.AddAndRunMove(_board, move);
        }

        private bool CheckCasteling(APiece piece, Position to)
        {
            if (piece.WasMoved)
            {
                return false;
            }

            if (piece is KingPiece)
            {
                if (to.X - piece.Position.X == 2)
                {
                    return _board.GetPieceAt(new Position(7, piece.Position.Y)) is RookPiece rook && rook.IsCastlingPossible(_board);
                }
                else if (piece.Position.X - to.X == 2)
                {
                    return _board.GetPieceAt(new Position(0, piece.Position.Y)) is RookPiece rook && rook.IsCastlingPossible(_board);
                }
            }
            else if (piece is RookPiece rook && to.X == 4)
            {
                return rook.IsCastlingPossible(_board);
            }

            return false;
        }

        private bool MovePiece(APiece piece, Position to)
        {
            if (piece.IsMoveAllowed(_board, to))
            {
                var isCastelingMove = CheckCasteling(piece, to);
                if (isCastelingMove)
                {
                    PerformMove(new GameMoveCasteling(_board.GetPieceAt(new Position(4, to.Y))!, to));
                }
                else
                {
                    APiece? capturedPiece = GetCapturedPiece(piece, to);
                    if (capturedPiece != null)
                    {
                        PerformMove(new GameMoveRemovePiece(capturedPiece));
                    }
                }

                PerformMove(new GameMoveMovePiece(piece, to));

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
                    IsCheckPending = CheckTest.IsKingInDanger(_board, _colorsTurn);
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

            PerformMove(new GameMovePromotePiece(_selectedPiece!, piece.Invoke()));
            SwapTurns();
            _state = GameState.GameRunning;

            return true;
        }

        public Piece?[][] GetBoard()
        {
            var board = new Piece?[8][];
            for (var y = 0; y < board.Length; y++)
            {
                board[y] = new Piece[8];
                for (var x = 0; x < board[y].Length; x++)
                {
                    board[y][x] = _board.GetPieceAt(new Position(x, y))?.PieceType;
                }
            }

            return board;
        }

        public Piece? GetPieceAtCell(Position position)
        {
            return _board.GetPieceAt(position)?.PieceType;
        }

        public override string ToString()
        {
            return _board.ToString();
        }

        public string ToFullAlgebraicNotation()
        {
            return _gameHistory.ToString()!;
        }
    }
}
