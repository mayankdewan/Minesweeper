namespace MinesweeperProblem.Models
{
    public class MinesweeperModel
    {
        public int gridSize { get; set; }
        public int numberOfMines { get; set; }
        public char[,] minefield { get; set; }
        public char[,] visibleField { get; set; }
    }
}
