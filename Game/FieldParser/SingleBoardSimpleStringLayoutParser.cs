using GameLogic.Pieces;

namespace GameLogic.FieldParser
{
    internal class SingleBoardSimpleStringLayoutParser : IFieldParser
    {
        public static string DefaultLayout = @"RNBQKBNR
PPPPPPPP
--------
--------
--------
--------
pppppppp
rnbqkbnr";

        /// <summary>
        /// Creates a field by a simple input (See DefaultLayout variable) with a single board for black (upper case) and white (lower case)
        /// </summary>
        /// <param name="input">Simple input. - defines an empty cell. A white piece is defined by a small case letter. A black piece is defined by an upper case letter. Empty space is only allowed at the beginning or the end of a row. The field must not contain any other character.</param>
        /// <returns>A set up field</returns>
        /// <exception cref="FieldParserException">When simple input is not defined properly</exception>
        public Field CreateField(string input)
        {
            var result = new Field();
            var rows = input.Trim().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            if (rows.Length != 8)
            {
                throw new FieldParserException("The input must contain exactly 8 rows.");
            }
            
            AssignRows(result, rows);

            return result;
        }

        private APiece? GetPieceByChar(char c, Position p)
        {
            var white = PieceColor.White;
            var black = PieceColor.Black;

            return c switch
            {
                '-' => null,
                'p' => new PawnPiece(p, white),
                'r' => new RookPiece(p, white),
                'n' => new KnightPiece(p, white),
                'b' => new BishopPiece(p, white),
                'q' => new QueenPiece(p, white),
                'k' => new KingPiece(p, white),
                'P' => new PawnPiece(p, black),
                'R' => new RookPiece(p, black),
                'N' => new KnightPiece(p, black),
                'B' => new BishopPiece(p, black),
                'Q' => new QueenPiece(p, black),
                'K' => new KingPiece(p, black),
                _ => throw new FieldParserException($"Character {c} at position {p} is not valid")
            };
        }

        private void AssignRows(Field field, string[] rows)
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
                    var piece = GetPieceByChar(cell, new Position(colIndex, rowIndex));
                    if (piece != null)
                    {
                        field.AddPiece(piece);
                    }
                }
            }
        }
    }
}
