using MinesweeperProblem.Models;
using MinesweeperProblem.Utilities;
using System.Text.RegularExpressions;

namespace MinesweeperProblem.Services
{
    public class MineSweeperService
    {
        private MinesweeperModel minesweeper;

        public MineSweeperService()
        {
            int gridSize, numberOfMines;
            GetUserInputforMineSweeper(out gridSize, out numberOfMines);
            minesweeper = new MinesweeperModel
            {
                numberOfMines = numberOfMines,
                gridSize = gridSize,
                minefield = new char[gridSize, gridSize],
                visibleField = new char[gridSize, gridSize]
            };
        }

        public MineSweeperService(MinesweeperModel minesweeper)
        {
            this.minesweeper = minesweeper;
        }

        public void PlayMineSweeper()
        {
            minesweeper = GridDisplayHelper.InitializeGrid(minesweeper);
            PlayGame();
        }

        /// <summary>
        /// Function to get user input for the grid size and number of mines
        /// </summary>
        /// <param name="gridSize"></param>
        /// <param name="numberOfMines"></param>
        public void GetUserInputforMineSweeper(out int gridSize, out int numberOfMines)
        {
            gridSize = InteractionHelper.GetGridSize();
            numberOfMines = InteractionHelper.GetNumberOfMines(gridSize);
        }

        /// <summary>
        /// Main entry function to play the game
        /// </summary>
        private void PlayGame()
        {
            Console.WriteLine("Here is your minefield:");
            while (true)
            {
                DisplayCurrentGameState();
                string? move = InteractionHelper.GetPlayerMove();

                if (IsValidMove(move))
                {
                    ProcessPlayerMove(move);
                }
                else
                {
                    Console.WriteLine("Incorrect Input.");
                }
            }
        }

        /// <summary>
        /// This function displays the current state of the game
        /// </summary>
        private void DisplayCurrentGameState()
        {
            GridDisplayHelper.DisplayGrid(minesweeper.visibleField);
            Console.WriteLine();
        }

        /// <summary>
        /// This function checks if the player's move is valid
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        private bool IsValidMove(string move)
        {
            Regex regex = new Regex(@"^[a-zA-Z]+[0-9]+$");
            return !string.IsNullOrWhiteSpace(move) && move.Length >= 2 && move.Length <= 4 && regex.Match(move.ToUpper()).Success;
        }

        /// <summary>
        /// Function to process the player's move
        /// </summary>
        /// <param name="move"></param>
        public void ProcessPlayerMove(string move)
        {
            // Use regular expression to separate alpha and numeric parts
            Match match = Regex.Match(move, @"([a-zA-Z]+)(\d+)");
            if (match.Success)
            {
                string rowPart = match.Groups[1].Value;
                string columnPart = match.Groups[2].Value;
                var rowData = rowPart.ToUpper().ToCharArray();
                if (rowData.Length > 1 && rowData[0] != 'F')
                {
                    Console.WriteLine("Incorrect Input.");
                    return;
                }
                char action = rowData[0] == 'F' && rowData.Length > 1 ? rowData[0] : 'A';
                int row = int.Parse(columnPart) - 1;
                int col = rowData[0] == 'F' && rowData.Length > 1 ? rowData[1] - 'A' : rowData[0] - 'A';

                if (row < 0 || row >= minesweeper.gridSize || col < 0 || col >= minesweeper.gridSize)
                {
                    Console.WriteLine("Incorrect Input.");
                    return;
                }

                if (action == 'F')
                {
                    FlagCell(row, col);
                }
                else
                {
                    if (RevealCell(row, col))
                    {
                        GridDisplayHelper.DisplayGrid(minesweeper.minefield);
                        Console.WriteLine("Oh no, you detonated a mine! Game over.");
                        Console.WriteLine("Press any key to play again...");
                        Console.ReadKey();
                        MineSweeperService mineSweeper = new MineSweeperService(); // Start a new game
                        mineSweeper.PlayMineSweeper();
                        return;
                    }
                }

                if (CheckForWin())
                {
                    GridDisplayHelper.DisplayGrid(minesweeper.visibleField);
                    Console.WriteLine("Congratulations! You cleared the minefield!");
                    Console.WriteLine("Press any key to play again...");
                    Console.ReadKey();
                    MineSweeperService mineSweeper = new MineSweeperService(); // Start a new game
                    mineSweeper.PlayMineSweeper();
                    return;
                }
                else
                {
                    Console.WriteLine("This square contains {0} adjacent mines.", CountAdjacentMines(row, col));
                }
            }
            else
            {
                Console.WriteLine("Incorrect Input.");
            }
        }

        /// <summary>
        /// Feature to flag a cell
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void FlagCell(int row, int col)
        {
            if (minesweeper.visibleField[row, col] == '_' || minesweeper.visibleField[row, col] == '\0')
            {
                minesweeper.visibleField[row, col] = 'F';
            }
            else if (minesweeper.visibleField[row, col] == 'F')
            {
                minesweeper.visibleField[row, col] = '_';
            }
        }

        /// <summary>
        /// Function to reveal a cell based on the user's input
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private bool RevealCell(int row, int col)
        {
            if (minesweeper.minefield[row, col] == 'X')
            {
                return true; // Game over, hit a mine
            }

            if (minesweeper.visibleField[row, col] == 'F')
            {
                Console.WriteLine("Cannot reveal a flagged cell. Unflag it first.");
                return false; // Cell already revealed
            }

            RevealAndCountAdjacentMines(row, col);
            
            return false; // Not hit a mine
        }

        /// <summary>
        /// This function reveals a cell and counts the number of mines adjacent to it
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void RevealAndCountAdjacentMines(int row, int col)
        {
            if (minesweeper.visibleField[row, col] != '_' && minesweeper.visibleField[row, col] != 'F')
            {
                Console.WriteLine("Cell already revealed.");
                return; // Cell already revealed
            }
            int adjacentMines = CountAdjacentMines(row, col);
            minesweeper.visibleField[row, col] = adjacentMines.ToString()[0];
            if (adjacentMines == 0)
            {
                RevealNeighboringCells(row, col);
            }
        }

        /// <summary>
        /// This function reveals the neighboring cells of a cell
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void RevealNeighboringCells(int row, int col)
        {
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (IsValidCell(i, j) && !RevealCell(i, j))
                    {
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Function to count the number of mines adjacent to a cell
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private int CountAdjacentMines(int row, int col)
        {
            int count = 0;

            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (IsValidCell(i, j) && minesweeper.minefield[i, j] == 'X')
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Function to check if a cell is valid
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public bool IsValidCell(int row, int col)
        {
            return row >= 0 && row < minesweeper.gridSize && col >= 0 && col < minesweeper.gridSize;
        }

        /// <summary>
        /// Determine if the game has been won
        /// </summary>
        /// <returns></returns>
        public bool CheckForWin()
        {
            // Check if all non-mine cells are revealed
            for (int i = 0; i < minesweeper.gridSize; i++)
            {
                for (int j = 0; j < minesweeper.gridSize; j++)
                {
                    if (minesweeper.minefield[i, j] != 'X' && minesweeper.visibleField[i, j] == '_' || minesweeper.visibleField[i,j] == '\0')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
