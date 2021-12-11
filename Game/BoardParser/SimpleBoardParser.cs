using GameLogic.InternPieces;

namespace GameLogic.BoardParser
{
    internal class SimpleBoardParser : IBoardParser
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
        /// Creates a board by a simple input (See DefaultLayout variable) with a single board for black (upper case) and white (lower case)
        /// </summary>
        /// <param name="input">Simple input. - defines an empty cell. A white piece is defined by a small case letter. A black piece is defined by an upper case letter. Empty space is only allowed at the beginning or the end of a row. The board must not contain any other character.</param>
        /// <returns>A set up board</returns>
        /// <exception cref="BoardParserException">When simple input is not defined properly</exception>
        public Board CreateBoard(string input)
        {
            var result = new Board();
            var rows = input.Trim().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            if (rows.Length != 8)
            {
                throw new BoardParserException("The input must contain exactly 8 rows.");
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
                _ => throw new BoardParserException($"Character {c} at position {p} is not valid")
            };
        }

        private void AssignRows(Board board, string[] rows)
        {
            for (int y = 0; y < rows.Length; y++)
            {
                var row = rows[y].Trim();
                var rowIndex = 7-y;

                if (row.Length != 8)
                {
                    throw new BoardParserException($"Every row must contain 8 cells. ({row.Length} cells found at row {y})");
                }

                for (int colIndex = 0; colIndex < row.Length; colIndex++)
                {
                    char cell = row[colIndex];
                    var piece = GetPieceByChar(cell, new Position(colIndex, rowIndex));
                    if (piece != null)
                    {
                        board.AddPiece(piece);
                    }
                }
            }
        }
    }
}
