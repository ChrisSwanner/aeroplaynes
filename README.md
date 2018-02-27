# Rock Paper Scissors

#### .Net MVC app that allows two users to play rock, paper, scissors and returns the winner., 2-14-18

The rules of rock, paper, scissors:

It is played by 2 people. Each person chooses "Rock", "Paper", or "Scissors" by forming a shape with their hand.
They choose at the same time, so that each person doesn't know what the other person has picked.
If one person chooses "Rock" and the other person picks "Scissors" then "Rock" wins. It's a game of combinations:
Rock v. Scissors = Rock wins
Rock v. Paper = Paper wins
Paper v. Scissors = Scissors wins
If both people choose the same thing, then it is a draw and nobody wins.

#### _By Sara Hamilton and Joel Adams_

## Description

_This is an Epicodus practice project for week 2 of the C# course. Its purpose is to display understanding .Net MVC testing in C#._

#### _Rock Paper Scissors_
* Each player inputs their name.
* Each player selects an input of either "rock", "paper", or "scissors".
* The two inputs are compared.
* The winning selection and winner's name are displayed.

### Specifications

* Take in name input for player 1 and player 2.
  * sample input : Player 1: Joel, Player 2: Sara
  * sample output : Player 1: Joel, Player 2: Sara
* Each player selects either rock, paper, or scissors.
  * sample input : dropdown list: "rock, paper:selected, scissors"
  * sample output : no output yet
* Input choices for each player are compared and evaluated.
  * sample input : Player 1: "paper", Player 2: "paper"
  * sample output : no output yet
* Winning selection is displayed.
  * sample input : Player 1: "paper", Player 2: "rock"
  * sample output : "Paper beats Rock. Joel wins."
* In case of a draw, Draw is displayed.
  * sample input : Player 1: "paper", Player 2: "paper"
  * sample output : "You both picked Paper. It's a draw."
* New Game button can be selected to reset the game.
  * sample input: Select New Game button
  * sample output: Game is reset.

## Setup/Installation Requirements

* _Clone this GitHub repository_

```
git clone https://github.com/Sara-Hamilton/Airplanes.git
```

* _Install the .NET Framework_

  .NET Core 1.1 SDK (Software Development Kit)

  .NET runtime.

  See https://www.learnhowtoprogram.com/c/getting-started-with-c/installing-c for instructions and links.

* _Run the program_
  1. In the command line, cd into the project folder.
  2. In the command line, type dotnet restore. Enter.  It make take a few minutes to complete this process.
  3. In the command line, type dotnet build. Enter. Any errror messages will be displayed in red.  Errors will need to be corrected before the app can be run. After correcting errors and saving changes, type dotnet build again.  When message says Build succeeded in green, proceed to the next step.
  4. In the command line, type dotnet run. Enter.

* _View program on web browser at port localhost:5000/_

* _Follow the prompts._

* _Select Play Again button to keep playing with same players._

* _Select Reset Game button to reset all values._

## Support and contact details

_To suggest changes, submit a pull request in the GitHub repository._

## Technologies Used

* HTML
* C#
* .Net Core 1.1

### License

*MIT License*

Copyright (c) 2018 **_Sara Hamilton and Joel Adams_**

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
