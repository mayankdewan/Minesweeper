# Overview
This Repo contains the implementation of classic Minesweeper game utilizing the Depth-First Search (DFS) algorithm. The game is played on a grid of cells, some containing mines. The objective is to reveal all cells without hitting a mine. Users can input commands in the format {character}{numeric}, such as A1 or FA1 to flag/un-flag a specific cell(This option is there for the user just in case user is in doubt that there is a mine on that particular cell). 

## Grid Format
The grid is represented as follows: <br />
 &nbsp;  A B C D <br />
1  _ _ _ _ <br />
2  _ _ _ _ <br />
3  _ _ _ _ <br />
4  _ _ _ _ <br />

(This is an example for a 4x4 grid)

# Getting Started

## Dependies

Ensure you have .Net Core 6.0 Package and Visual Studio installed. You can download the .Net Core package (https://dotnet.microsoft.com/en-us/download/dotnet/6.0).

### Installation

* Clone the project to your local system.
* Open the solution file in Visual Studio.
* Build the project.

## Execution
This is a console application. Once the code is built, press F5 or start the application in Visual Studio to launch the Minesweeper game.

## Gameplay
* Enter your moves in the format {character}{numeric} (e.g., A1 or FA1 for flagging).
* Selecting a cell with a mine results in a loss.
* Successfully revealing all cells without hitting a mine leads to victory.
