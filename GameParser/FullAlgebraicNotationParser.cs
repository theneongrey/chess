using GameLogic;
using GameParser.PieceMapper;
using System.Text.RegularExpressions;

namespace GameParser
{
    public class FullAlgebraicNotationParser
    {
        private bool _isWhiteTurn;
        private IPieceMapper _pieceMapper;

        private FullAlgebraicNotationParser(IPieceMapper pieceMapper)
        {
            _isWhiteTurn = true;
            _pieceMapper = pieceMapper;
        }

        private Game Parse(string notation)
        {
            Game game = new Game();

            var steps = SplitSteps(notation);
            var moves = GetSingleMoves(steps);

            PerformeMoves(game, moves);

            return game;
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
                    if (moves[0].EndsWith("++") || moves[0][^1] == '#')
                    {
                        result.Add(moves[0]);
                    }
                    else
                    {
                        throw new InvalidDataException($"Move {step} is not valid");
                    }
                }
                else
                {
                    throw new InvalidDataException($"Move {step} is not valid");
                }
            }

            return result;
        }

        private void PerformeMove(Game game, string move)
        {
            Position? from;
            Position? to;

            // perform castling right
            if (move.Trim() == "O-O")
            {
                from = new Position(4, _isWhiteTurn ? 0 : 7);
                to = new Position(6, _isWhiteTurn ? 0 : 7);
            }
            // perform castling left
            else if (move.Trim() == "O-O-O")
            {
                from = new Position(4, _isWhiteTurn ? 0 : 7);
                to = new Position(2, _isWhiteTurn ? 0 : 7);
            }
            else
            {
                string[] fromTo = move.Split('-', StringSplitOptions.RemoveEmptyEntries);
                if (fromTo.Length != 2)
                {
                    fromTo = move.Split('x', StringSplitOptions.RemoveEmptyEntries);
                }

                if (fromTo.Length != 2)
                {
                    throw new InvalidDataException($"Move {move} is not valid");
                }

                var fromString = fromTo[0].Trim();
                var toString = fromTo[1].Trim();

                Piece piece = Pieces.Pawn;
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

                var pieceAtCell = game.SelectPiece(from.Value);
                if (pieceAtCell == null || pieceAtCell.Identifier.ToUpper() != piece.Identifier)
                {
                    throw new InvalidDataException($"Move {move} is not valid. The piece at {from} is not a {piece.Identifier}");
                }
                if (char.IsLower(pieceAtCell.Identifier[0]) && !_isWhiteTurn ||
                    char.IsUpper(pieceAtCell.Identifier[0]) && _isWhiteTurn)
                {
                    throw new InvalidDataException($"Move {move} is not valid. It's the wrong turn.");
                }
                if (!game.TryMove(to.Value))
                {
                    throw new InvalidDataException($"Move {move} is not valid. {piece.Identifier} at {from} can not move to {to}.");
                }
                if (move[^1] == '#' && !game.IsGameOver)
                {
                    throw new InvalidDataException($"Game should be over but it isn't.");
                }
            }
        }

        private void PerformeMoves(Game game, List<string> moves)
        {
            foreach (var move in moves)
            {
                PerformeMove(game, move);
                _isWhiteTurn = !_isWhiteTurn;
            }
        }

        public static Game GetGameFromNotation(string notation)
        {
            return GetGameFromNotation(notation, new EnglishPieceMapper());
        }

        public static Game GetGameFromNotation(string notation, IPieceMapper pieceMapper)
        {
            var parser = new FullAlgebraicNotationParser(pieceMapper);
            return parser.Parse(notation);
        }
    }
}