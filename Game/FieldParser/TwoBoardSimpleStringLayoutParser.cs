using GameLogic.Pieces;

namespace GameLogic.FieldParser
{
    internal class TwoBoardSimpleStringLayoutParser : IFieldParser
    {
        public static string DefaultLayout = @"--------
                                               --------
                                               --------
                                               --------
                                               --------
                                               --------
                                               pppppppp
                                               rnbqkbnr;
                                               rnbqkbnr
                                               pppppppp";

        /// <summary>
        /// Creates a field by a simple input (See DefaultLayout variable) with tow separate boards for black and white.
        /// </summary>
        /// <param name="input">Simple input. ; separates sides. - defines an empty cell. a piece is defined by a small case letter. Empty space is only allowed at the beginning or the end of a row. The field must not contain any other character.</param>
        /// <returns>A set up field</returns>
        /// <exception cref="FieldParserException">When simple input is not defined properly</exception>
        public Board CreateField(string input)
        {
            var sides = input.Split(';');
            if (sides.Length != 2)
            {
                throw new FieldParserException("The input must have two sides separated by ';'");
            }

            var result = new Board();
            var whiteRows = sides[0].Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var blackRows = sides[1].Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            AssignRows(result, whiteRows, PieceColor.White);
            AssignRows(result, blackRows, PieceColor.Black);

            return result;
        }

        private APiece? GetPieceByChar(char c, Position p, PieceColor color)
        {
            return c switch
            {
                '-' => null,
                'p' => new PawnPiece(p, color),
                'r' => new RookPiece(p, color),
                'n' => new KnightPiece(p, color),
                'b' => new BishopPiece(p, color),
                'q' => new QueenPiece(p, color),
                'k' => new KingPiece(p, color),
                _ => throw new FieldParserException($"Character {c} at position {p} for color {color} is not valid")
            };
        }

        private void AssignRows(Board field, string[] rows, PieceColor color)
        {
            for (int y = 0; y < rows.Length; y++)
            {
                var row = rows[y].Trim();
                var rowIndex = 7-y;

                if (row.Length != 8)
                {
                    throw new FieldParserException($"Every row must contain 8 cells. ({row.Length} cells found at row {y})");
                }

                for (int colIndex = 0; colIndex < row.Length; colIndex++)
                {
                    char cell = row[colIndex];
                    var position = new Position(colIndex, rowIndex);
                    var piece = GetPieceByChar(cell, position, color);
                    
                    if (piece != null)
                    {
                        if (field.GetPieceAt(position) != null)
                        {
                            throw new FieldParserException($"The cell at position {colIndex},{rowIndex} is set twice.");
                        }
                        field.AddPiece(piece);
                    }
                }
            }
        }
    }
}
