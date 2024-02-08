using System.Diagnostics.CodeAnalysis;

namespace MinesweeperProblem.UnitTest.Utilities
{
    public static class TestHelper
    {
        [ExcludeFromCodeCoverage]
        public static bool AllElementsEqual(char[,] array, char expectedValue)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] != expectedValue)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        [ExcludeFromCodeCoverage]
        public static bool CheckForMines(char[,] array, int numberOfMines)
        {
            int mineCount = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == 'X')
                    {
                        mineCount++;
                    }
                }
            }
            return mineCount == numberOfMines;
        }

        [ExcludeFromCodeCoverage]
        public static char[,] ReplaceAllElementsWithOne(char[,] array)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = '1';
                }
            }
            return array;
        }
    }
}
