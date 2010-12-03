using SharpBag.Math;
using SharpBag.Strings;
using System;
using System.Linq;

namespace SharpBag.FK
{
    /// <summary>
    /// Method sem gætu verið notuð fyrir Minesweeper leikinn
    /// </summary>
    public class Minesweeper
    {
        /// <summary>
        /// The board.
        /// </summary>
        public bool[,] Board { get; set; }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="board">The board.</param>
        public Minesweeper(bool[,] board)
        {
            this.Board = board;
        }

        /// <summary>
        /// Parses the specified string into a minesweeper board.
        /// </summary>
        /// <param name="board">The string.</param>
        /// <param name="bomb">What bombs look like.</param>
        /// <returns>The board.</returns>
        public static Minesweeper Parse(string board, char bomb = '*')
        {
            string[] lines = board.Lines();
            bool[,] nBoard = new bool[lines.Length, lines.Max(s => s.Length)];

            for (int x = 0; x < lines.Length; x++)
            {
                for (int y = 0; y < lines[x].Length; y++)
                {
                    nBoard[x, y] = lines[x][y] == bomb;
                }
            }

            return new Minesweeper(nBoard);
        }

        /// <summary>
        /// Returns the board.
        /// </summary>
        /// <param name="showCount">Whether to show the counts.</param>
        /// <param name="bomb">What bombs look like.</param>
        /// <param name="empty">What an empty space looks like.</param>
        /// <returns>The board.</returns>
        public char[,] GetBoard(bool showCount = false, char bomb = '*', char empty = '.')
        {
            char[,] board = new char[this.Board.GetLength(0), this.Board.GetLength(1)];

            for (int x = 0; x < this.Board.GetLength(0); x++)
            {
                for (int y = 0; y < this.Board.GetLength(1); y++)
                {
                    board[x, y] = this.Board[x, y] ? bomb : showCount ? this.BombsOn(x, y).ToString()[0] : empty;
                }
            }

            return board;
        }

        /// <summary>
        /// Returns how many bombs are around the specified coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>How many bombs are around the specified coordinates.</returns>
        public int BombsOn(int x, int y)
        {
            int n = 0;

            for (int xM = -1; xM <= 1; xM++)
            {
                for (int yM = -1; yM <= 1; yM++)
                {
                    int nX = x + xM,
                        nY = y + yM;

                    if (xM == 0 && yM == 0) continue;
                    if (!this.OnBoard(nX, nY)) continue;

                    if (this.Board[x + xM, y + yM]) n++;
                }
            }

            return n;
        }

        private bool OnBoard(int x, int y)
        {
            return x.IsBetweenOrEqualTo(0, this.Board.GetLength(0) - 1) && y.IsBetweenOrEqualTo(0, this.Board.GetLength(1) - 1);
        }
    }
}
