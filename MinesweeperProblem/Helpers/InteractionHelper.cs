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
                    if(gridSize > 10)
                    {
                        Console.WriteLine("Maximum size of grid is 10.");
                        continue;
                    }
                    if(gridSize < 3)
                    {
                        Console.WriteLine("Minimum size of grid is 2.");
                        continue;
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect Input.");
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
                if (int.TryParse(Console.ReadLine(), out numberOfMines))
                {
                    if(numberOfMines == 0)
                    {
                        Console.WriteLine("There must be at least 1 mine.");
                    }
                    else if (numberOfMines > 0 && numberOfMines <= (int)(0.35 * gridSize * gridSize))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Maximum number is 35% of total sqaures.");
                    }

                }
                else
                {
                    Console.WriteLine("Incorect input.");
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
