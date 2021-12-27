using GameLogic;
using GameParser.PieceMapper;
using System.Text.RegularExpressions;

namespace GameParser
{
    public class FullAlgebraicNotationParser
    {
        private bool _isWhiteTurn;
        private IPieceMapper _pieceMapper;
        private Game _game;

        private FullAlgebraicNotationParser(IPieceMapper pieceMapper, Game game, bool isWhiteTurn = true)
        {
            _isWhiteTurn = isWhiteTurn;
            _pieceMapper = pieceMapper;
            _game = game;
        }

        private Game Parse(string notation)
        {
            var steps = SplitSteps(notation);
            var moves = GetSingleMoves(steps);

            PerformeMoves(moves);

            return _game;
        }

        private Position? GetCoordinatesByName(string name)
        {
            if (name.Length < 2)
            {
                return null;
            }

            var x = name[0] - 'a';
            var y = name[1] - '1';

            try
            {
                return new Position(x, y);
            }
            catch
            {
                return null;
            }

        }

        private string[] SplitSteps(string stepSequence)
        {
            string pattern = @"[0-9]+\.";
            string[] result = Regex.Split(stepSequence, pattern);

            return result;
        }

        private List<string> GetSingleMoves(string[] steps)
        {
            List<string> result = new ();

            foreach (string step in steps)
            {
                if (string.IsNullOrWhiteSpace(step))
                {
                    continue;
                }

                var moves = step.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (moves.Length == 2)
                {
                    result.Add(moves[0]);
                    result.Add(moves[1]);
                }
                else if (moves.Length == 1)
                {
                    result.Add(moves[0]);
                }
                else
                {
                    throw new InvalidDataException($"Move {step} is not valid");
                }
            }

            return result;
        }

        private void PerformeMove(string move)
        {
            Position? from;
            Position? to;
            Piece piece = Pieces.Pawn;


            // perform castling right
            if (move.Trim() == "O-O")
            {
                from = new Position(4, _isWhiteTurn ? 0 : 7);
                to = new Position(6, _isWhiteTurn ? 0 : 7);
                piece = Pieces.King;
            }
            // perform castling left
            else if (move.Trim() == "O-O-O")
            {
                from = new Position(4, _isWhiteTurn ? 0 : 7);
                to = new Position(2, _isWhiteTurn ? 0 : 7);
                piece = Pieces.King;
            }
            else
            {
                // check if move or capture
                string[] fromTo = move.Split('-', StringSplitOptions.RemoveEmptyEntries);
                if (fromTo.Length != 2)
                {
                    fromTo = move.Split('x', StringSplitOptions.RemoveEmptyEntries);
                }

                // check if from and to exists
                if (fromTo.Length != 2)
                {
                    throw new InvalidDataException($"Move {move} is not valid");
                }

                var fromString = fromTo[0].Trim();
                var toString = fromTo[1].Trim();

                if (char.IsUpper(fromString[0]))
                {
                    piece = _pieceMapper.GetPieceByName(fromString[0]);
                    fromString = fromString[1..];
                }

                from = GetCoordinatesByName(fromString);
                to = GetCoordinatesByName(toString);

                if (!from.HasValue)
                {
                    throw new InvalidDataException($"Move {move} is not valid. Could not parse source cell position.");
                }
                if (!to.HasValue)
                {
                    throw new InvalidDataException($"Move {move} is not valid. Could not parse target cell position.");
                }
            }

            // check if given piece is actual piece
            var pieceAtCell = _game.SelectPiece(from.Value);
            if (pieceAtCell == null || pieceAtCell.Identifier != piece.Identifier)
            {
                throw new InvalidDataException($"Move {move} is not valid. The piece at {from} is not a {piece.Identifier}");
            }

            // check if piece corresponds to the current turn
            if (pieceAtCell.IsWhite && !_isWhiteTurn ||
                !pieceAtCell.IsWhite && _isWhiteTurn)
            {
                throw new InvalidDataException($"Move {move} is not valid. It's the wrong turn.");
            }

            // try to perform move
            if (!_game.TryMove(to.Value))
            {
                throw new InvalidDataException($"Move {move} is not valid. {piece.Identifier} at {from} can not move to {to}.");
            }

            // special annotations
            var lastChar = move[^1];

            // promotion
            if (char.IsUpper(lastChar) && lastChar != 'O') // O is for casteling only
            {
                if (piece.Identifier != _pieceMapper.PawnName || (to.Value.Y != 0 && to.Value.Y != 7) || 
                    !_pieceMapper.AllowedPromotionPieces.Contains(lastChar))
                {
                    throw new InvalidDataException($"Move {move} is not valid. The given promotion is not valid");
                }

                _game.PerformPromotion(_pieceMapper.GetPieceByName(lastChar));
            }


            if (lastChar == '#' && !_game.IsGameOver)
            {
                throw new InvalidDataException($"Game should be over but it isn't.");
            }
        }

        private void PerformeMoves(List<string> moves)
        {
            foreach (var move in moves)
            {
                PerformeMove(move);
                _isWhiteTurn = !_isWhiteTurn;
            }
        }

        public static Game GetGameFromNotation(string notation)
        {
            return GetGameFromNotation(notation, new EnglishPieceMapper());
        }

        public static Game GetGameFromNotation(string notation, IPieceMapper pieceMapper)
        {
            var parser = new FullAlgebraicNotationParser(pieceMapper, new Game());
            return parser.Parse(notation);
        }

        public static Game ContinueGameFromNotation(string board, string notation, bool isWhiteTurn)
        {
            return ContinueGameFromNotation(board, notation, isWhiteTurn, new EnglishPieceMapper());
        }

        public static Game ContinueGameFromNotation(string board, string notation, bool isWhiteTurn, IPieceMapper pieceMapper)
        {
            var parser = new FullAlgebraicNotationParser(pieceMapper, Game.FromString(board), isWhiteTurn);
            return parser.Parse(notation);
        }
    }
}