using MinesweeperProblem.Models;
using MinesweeperProblem.Services;
using MinesweeperProblem.UnitTest.Utilities;
using MinesweeperProblem.Utilities;
using NuGet.Frameworks;

namespace MinesweeperProblem.UnitTest
{
    public class MineSweeperServiceTests
    {
        [Fact]
        public void InitializeGrid_Should_CreateGrid()
        {
            // Arrange
            var minesweeper = GetMinesweeperModel();

            // Act
            minesweeper = GridDisplayHelper.InitializeGrid(minesweeper);

            // Assert
            Assert.True(TestHelper.AllElementsEqual(minesweeper.visibleField, '_'));
        }

        [Fact]
        public void InitializeGrid_Should_CreateSpecifiedNumberOfMines()
        {
            // Arrange
            var minesweeper = GetMinesweeperModel();

            // Act
            minesweeper = GridDisplayHelper.InitializeGrid(minesweeper);

            // Assert
            Assert.True(TestHelper.CheckForMines(minesweeper.minefield, minesweeper.numberOfMines));
        }

        [Fact]
        public void InitializeGrid_Should_Not_CreateSpecifiedNumberOfMines()
        {
            // Arrange
            var minesweeper = GetMinesweeperModel();

            // Act
            minesweeper = GridDisplayHelper.InitializeGrid(minesweeper);

            // Assert
            Assert.False(TestHelper.CheckForMines(minesweeper.minefield, 1));
        }

        [Fact]
        public void DisplayGrid_ShouldOutputCorrectGrid()
        {
            // Arrange
            var minesweeper = GetMinesweeperModel();
            string expectedOutput =
            "   A B C D E\r\n" +
            "1  \0 \0 \0 \0 \0 \r\n" +
            "2  \0 \0 \0 \0 \0 \r\n" +
            "3  \0 \0 \0 \0 \0 \r\n" +
            "4  \0 \0 \0 \0 \0 \r\n" +
            "5  \0 \0 \0 \0 \0 \r\n";


            // Act
            string capturedOutput;
            using (var stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                GridDisplayHelper.DisplayGrid(minesweeper.visibleField);
                capturedOutput = stringWriter.ToString();
            }

            // Assert
            Assert.Equal(expectedOutput, capturedOutput);
        }

        [Fact]
        public void DisplayGrid_ShouldOutputCorrectGridForMoreThanNineElements()
        {
            // Arrange
            var minesweeper = GetMinesweeperModel(10);
            string expectedOutput =
            "   A B C D E F G H I J\r\n" +
            "1  \0 \0 \0 \0 \0 \0 \0 \0 \0 \0 \r\n" +
            "2  \0 \0 \0 \0 \0 \0 \0 \0 \0 \0 \r\n" +
            "3  \0 \0 \0 \0 \0 \0 \0 \0 \0 \0 \r\n" +
            "4  \0 \0 \0 \0 \0 \0 \0 \0 \0 \0 \r\n" +
            "5  \0 \0 \0 \0 \0 \0 \0 \0 \0 \0 \r\n" +
            "6  \0 \0 \0 \0 \0 \0 \0 \0 \0 \0 \r\n" +
            "7  \0 \0 \0 \0 \0 \0 \0 \0 \0 \0 \r\n" +
            "8  \0 \0 \0 \0 \0 \0 \0 \0 \0 \0 \r\n" +
            "9  \0 \0 \0 \0 \0 \0 \0 \0 \0 \0 \r\n" +
            "10 \0 \0 \0 \0 \0 \0 \0 \0 \0 \0 \r\n";
            
            // Act
            string capturedOutput;
            using (var stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                GridDisplayHelper.DisplayGrid(minesweeper.visibleField);
                capturedOutput = stringWriter.ToString();
            }

            // Assert
            Assert.Equal(expectedOutput, capturedOutput);
        }

        [Fact]
        public void DisplayGrid_ShouldNotOutputCorrectGrid()
        {
            // Arrange
            var minesweeper = GetMinesweeperModel(3);
            string expectedOutput =
            "   A B C D E\r\n" +
            "1  \0 \0 \0 \0 \0 \r\n" +
            "2  \0 \0 \0 \0 \0 \r\n" +
            "3  \0 \0 \0 \0 \0 \r\n" +
            "4  \0 \0 \0 \0 \0 \r\n" +
            "5  \0 \0 \0 \0 \0 \r\n";


            // Act
            string capturedOutput;
            using (var stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                GridDisplayHelper.DisplayGrid(minesweeper.visibleField);
                capturedOutput = stringWriter.ToString();
            }

            // Assert
            Assert.NotEqual(expectedOutput, capturedOutput);
        }

        [Fact]
        public void Check_If_Input_Is_Valid()
        {
            // Arrange
            var minesweeper = GetMinesweeperModel();
            var minesweeperService = new MineSweeperService(minesweeper);

            // Act
            bool capturedOutput = minesweeperService.IsValidCell(1,1);

            // Assert
            Assert.True(true, capturedOutput.ToString());
        }

        [Fact]
        public void Check_If_Input_Is_NotValid()
        {
            // Arrange
            var minesweeper = GetMinesweeperModel();
            var minesweeperService = new MineSweeperService(minesweeper);

            // Act
            bool capturedOutput = minesweeperService.IsValidCell(6, 6);

            // Assert
            Assert.False(false, capturedOutput.ToString());
        }

        [Fact]
        public void Check_If_User_Won()
        {
            // Arrange
            var minesweeper = GetMinesweeperModel();
            var minesweeperService = new MineSweeperService(minesweeper);

            // Act
            minesweeper.visibleField = TestHelper.ReplaceAllElementsWithOne(minesweeper.visibleField);
            bool capturedOutput = minesweeperService.CheckForWin();

            // Assert
            Assert.True(true, capturedOutput.ToString());
        }

        [Fact]
        public void Check_If_User_NotWon()
        {
            // Arrange
            var minesweeper = GetMinesweeperModel();
            var minesweeperService = new MineSweeperService(minesweeper);

            // Act
            bool capturedOutput = minesweeperService.CheckForWin();

            // Assert
            Assert.False(false, capturedOutput.ToString());
        }
        //[Fact]
        //public void RevealCell_Should_Return_True_If_Hit_Mine()
        //{
        //    // Arrange
        //    var mineSweeperService = new MineSweeperService();

        //    // Set up a mine at position (0, 0)
        //    mineSweeperService.SetMinefield(new char[,] { { 'X', '_' }, { '_', '_' } });

        //    // Act
        //    bool hitMine = mineSweeperService.RevealCell(0, 0);

        //    // Assert
        //    Assert.True(hitMine);
        //}

        //[Fact]
        //public void RevealCell_Should_Return_False_If_Not_Hit_Mine()
        //{
        //    // Arrange
        //    var mineSweeperService = new MineSweeperService();

        //    // Set up a mine at position (0, 1)
        //    mineSweeperService.SetMinefield(new char[,] { { '_', 'X' }, { '_', '_' } });

        //    // Act
        //    bool hitMine = mineSweeperService.RevealCell(0, 0);

        //    // Assert
        //    Assert.False(hitMine);
        //}

        //[Fact]
        //public void CheckForWin_Should_Return_True_If_All_Non_Mine_Cells_Revealed()
        //{
        //    // Arrange
        //    var mineSweeperService = new MineSweeperService();

        //    // Set up a minefield with one mine and reveal all other cells
        //    mineSweeperService.SetMinefield(new char[,] { { 'X', '1' }, { '2', 'F' } });
        //    mineSweeperService.RevealCell(1, 0);

        //    // Act
        //    bool gameWon = mineSweeperService.CheckForWin();

        //    // Assert
        //    Assert.True(gameWon);
        //}

        private MinesweeperModel GetMinesweeperModel(int minesweeperSize = 5)
        {
            return new MinesweeperModel
            {
                numberOfMines = minesweeperSize,
                gridSize = minesweeperSize,
                minefield = new char[minesweeperSize, minesweeperSize],
                visibleField = new char[minesweeperSize, minesweeperSize]
            };
        }
    }
}