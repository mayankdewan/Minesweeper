using MinesweeperProblem.Models;

namespace MinesweeperProblem.Utilities
{
    public static class GridDisplayHelper
    {
        public static void DisplayGrid(char[,] grid)
        {
            int gridSize = grid.GetLength(0);
            Console.WriteLine("   " + string.Join(" ", Enumerable.Range(1, gridSize).Select(i => (char)(i + 64))));
            for (int i = 0; i < gridSize; i++)
            {
                if (i < 9)
                {
                    Console.Write((i + 1) + "  ");
                }
                else
                {
                    Console.Write((i + 1) + " ");
                }
                for (int j = 0; j < gridSize; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Initialize the grid for the game
        /// </summary>
        public static MinesweeperModel InitializeGrid(MinesweeperModel minesweeper)
        {
            minesweeper.minefield = new char[minesweeper.gridSize, minesweeper.gridSize];
            minesweeper.visibleField = new char[minesweeper.gridSize, minesweeper.gridSize];

            // Initialize minefield and visibleField
            for (int i = 0; i < minesweeper.gridSize; i++)
            {
                for (int j = 0; j < minesweeper.gridSize; j++)
                {
                    minesweeper.minefield[i, j] = '_';
                    minesweeper.visibleField[i, j] = '_';
                }
            }

            // Place mines randomly
            Random random = new Random();
            int minesPlaced = 0;

            while (minesPlaced < minesweeper.numberOfMines)
            {
                int row = random.Next(minesweeper.gridSize);
                int col = random.Next(minesweeper.gridSize);

                if (minesweeper.minefield[row, col] != 'X')
                {
                    minesweeper.minefield[row, col] = 'X';
                    minesPlaced++;
                }
            }
            return minesweeper;
        }
    }
}
