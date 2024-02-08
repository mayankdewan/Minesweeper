using MinesweeperProblem.Models;
using MinesweeperProblem.Services;
using MinesweeperProblem.UnitTest.Utilities;
using MinesweeperProblem.Utilities;
using NuGet.Frameworks;
using System.IO;

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

        [Fact]
        public void ProcessUserMove_Return_Cell_Revealed()
        {
            // Arrange
            var minesweeper = GetMinesweeperModel();
            var minesweeperService = new MineSweeperService(minesweeper);
            string expectedOutput = "Cell already revealed.\r\n";
            // Act
            string capturedOutput;
            using (var stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                minesweeperService.ProcessPlayerMove("A1");
                capturedOutput = stringWriter.ToString();
            }

            // Assert
            Assert.Equal(expectedOutput, capturedOutput.ToString());
        }

        [Fact]
        public void ProcessUserMove_Return_Invalid_Move()
        {
            // Arrange
            var minesweeper = GetMinesweeperModel();
            var minesweeperService = new MineSweeperService(minesweeper);
            string expectedOutput = "Invalid input. Please enter a valid move.\r\n";
            // Act
            string capturedOutput;
            using (var stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                minesweeperService.ProcessPlayerMove("BA1");
                capturedOutput = stringWriter.ToString();
            }

            // Assert
            Assert.Equal(expectedOutput, capturedOutput.ToString());
        }

        [Fact]
        public void ProcessUserMove_Return_And_Display_FlaggedCell()
        {
            // Arrange
            var minesweeper = GetMinesweeperModel();
            var minesweeperService = new MineSweeperService(minesweeper);
            string expectedOutput =
            "   A B C D E\r\n" +
            "1  F \0 \0 \0 \0 \r\n" +
            "2  \0 \0 \0 \0 \0 \r\n" +
            "3  \0 \0 \0 \0 \0 \r\n" +
            "4  \0 \0 \0 \0 \0 \r\n" +
            "5  \0 \0 \0 \0 \0 \r\n";
            // Act
            string capturedOutput;
            using (var stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                minesweeperService.ProcessPlayerMove("FA1");
                GridDisplayHelper.DisplayGrid(minesweeper.visibleField);
                capturedOutput = stringWriter.ToString();
            }

            // Assert
            Assert.Equal(expectedOutput, capturedOutput.ToString());
        }

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