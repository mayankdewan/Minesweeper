using System.Diagnostics.CodeAnalysis;

namespace MinesweeperProblem.Models
{
    [ExcludeFromCodeCoverage]
    public class MinesweeperModel
    {
        public int gridSize { get; set; }
        public int numberOfMines { get; set; }
        public char[,] minefield { get; set; }
        public char[,] visibleField { get; set; }
        public bool showMessage { get; set; }
    }
}
