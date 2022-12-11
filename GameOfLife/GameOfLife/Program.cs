using System;
using System.Collections.Generic;

namespace GameOfLife
{
    internal static class Program
    {
        private static void Main()
        {
            int[][] board =
            {
                new[] { 0, 1, 0 },
                new[] { 0, 0, 1 },
                new[] { 1, 1, 1 },
                new[] { 0, 0, 0 }
            };
            var nextState = GameOfLife(board);
            Console.WriteLine("Next state of the board:");
            foreach (var row in nextState)
            {
                Console.WriteLine(string.Join(", ", row));
            }
        }

        private static IEnumerable<int[]> GameOfLife(IReadOnlyList<int[]> board)
        {
            var m = board.Count;
            var n = board[0].Length;
            var nextState = new int[m][];
            for (var i = 0; i < m; i++)
            {
                nextState[i] = new int[n];
            }
            
            for (var i = 0; i < m; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    var liveNeighbors = 0;
                    for (var row = Math.Max(0, i - 1); row <= Math.Min(m - 1, i + 1); row++)
                    {
                        for (var col = Math.Max(0, j - 1); col <= Math.Min(n - 1, j + 1); col++)
                        {
                            liveNeighbors += board[row][col];
                        }
                    }

                    liveNeighbors -= board[i][j];

                    switch (board[i][j])
                    {
                        case 1 when (liveNeighbors < 2 || liveNeighbors > 3):
                            nextState[i][j] = 0;
                            break;
                        case 0 when liveNeighbors == 3:
                            nextState[i][j] = 1;
                            break;
                        default:
                            nextState[i][j] = board[i][j];
                            break;
                    }
                }
            }

            return nextState;
        }
    }
}