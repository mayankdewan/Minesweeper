using System.Text.RegularExpressions;

namespace MinesweeperProblem.Utilities
{
    public static class InteractionHelper
    {
        public static int GetGridSize()
        {
            Console.WriteLine("Welcome to Minesweeper!");
            int gridSize = 0;
            while (true)
            {
                Console.Write("Enter the size of the grid (e.g. 4 for a 4x4 grid): ");
                if (int.TryParse(Console.ReadLine(), out gridSize) && gridSize > 0)
                {
                    if(gridSize > 26)
                    {
                        Console.WriteLine("The maximum grid size is 26x26. Please enter a smaller size.");
                        continue;
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a positive integer for the size of the grid.");
                }
            }
            return gridSize;
        }

        public static int GetNumberOfMines(int gridSize)
        {
            int numberOfMines = 0;
            while (true)
            {
                Console.Write("Enter the number of mines to place on the grid (maximum is 35% of the total squares): ");
                if (int.TryParse(Console.ReadLine(), out numberOfMines) && numberOfMines > 0 && numberOfMines <= (int)(0.35 * gridSize * gridSize))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a positive integer for the number of mines (not exceeding 35% of the total squares).");
                }
            }
            return numberOfMines;
        }

        public static string GetPlayerMove()
        {
            Console.Write("Enter your move (e.g. A1 or FA1 to Flag a particular cell): ");
            return Console.ReadLine();
        }
    }
}
