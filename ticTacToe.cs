using System;
using System.Collections.Generic;

//Rebuild required to run on your system
namespace TicTacToe
{
    class Program{
        static char[] board = new char[9];
        static Random random = new Random();
        static void Main(string[] args){
            bool playAgain = true;
            while (playAgain){
                InitializeBoard();
                bool gameOver = false;
                bool playerTurn = true; // Player starts first, maybe I'll add a choice to let the player choose
                while (!gameOver){
                    DisplayBoard();
                    if (playerTurn){
                        PlayerMove();}
                    else{
                        AIMove();}

                    char winner = CheckWinner();
                    if (winner == 'X'){
                        DisplayBoard();
                        Console.WriteLine("You Win!!");
                        gameOver = true;}
                    else if (winner == 'O'){
                        DisplayBoard();
                        Console.WriteLine("You Lose..");
                        gameOver = true;
                    }
                    else if (IsBoardFull()){
                        DisplayBoard();
                        Console.WriteLine("The game is a draw.");
                        gameOver = true;
                    }

                    playerTurn = !playerTurn;
                }

                playAgain = AskPlayAgain();
            }
        }

        static void InitializeBoard(){
            for (int i = 0; i < 9; i++){
                board[i] = (char)('1' + i);
            }
        }

        static void DisplayBoard(){
            Console.Clear();
            Console.WriteLine("Tic Tac Toe");
            Console.WriteLine();
            Console.WriteLine($" {board[0]} | {board[1]} | {board[2]} ");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {board[3]} | {board[4]} | {board[5]} ");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {board[6]} | {board[7]} | {board[8]} ");
            Console.WriteLine();
        }

        static void PlayerMove(){
            bool validMove = false;
            while (!validMove){
                Console.Write("Enter your move (1-9): ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int position) && position >= 1 && position <= 9){
                    int index = position - 1;
                    if (board[index] != 'X' && board[index] != 'O'){
                        board[index] = 'X';
                        validMove = true;
                    }
                    else{
                        Console.WriteLine("Try again, this space is populated.");
                    }
                }
                else{
                    Console.WriteLine("Invalid input. Enter a number from 1 to 9.");
                }
            }
        }

        static void AIMove(){
            // Fairly general Ai here, it works well enough
            int winMove = FindWinningMove('O');
            if (winMove != -1){
                board[winMove] = 'O';
                return;
            }

            // Check if player is about to win and block
            int blockMove = FindWinningMove('X');
            if (blockMove != -1){
                board[blockMove] = 'O';
                return;
            }

            // Otherwise, random move
            List<int> emptySpots = new List<int>();
            for (int i = 0; i < 9; i++){
                if (board[i] != 'X' && board[i] != 'O'){
                    emptySpots.Add(i);
                }
            }
            if (emptySpots.Count > 0){
                int index = random.Next(emptySpots.Count);
                board[emptySpots[index]] = 'O';
            }
        }

        static int FindWinningMove(char symbol){
            // Check rows
            for (int row = 0; row < 3; row++){
                int start = row * 3;
                if (board[start] == symbol && board[start + 1] == symbol && board[start + 2] != 'X' && board[start + 2] != 'O') return start + 2;
                if (board[start] == symbol && board[start + 2] == symbol && board[start + 1] != 'X' && board[start + 1] != 'O') return start + 1;
                if (board[start + 1] == symbol && board[start + 2] == symbol && board[start] != 'X' && board[start] != 'O') return start;
            }

            // Check columns
            for (int col = 0; col < 3; col++){
                int start = col;
                if (board[start] == symbol && board[start + 3] == symbol && board[start + 6] != 'X' && board[start + 6] != 'O') return start + 6;
                if (board[start] == symbol && board[start + 6] == symbol && board[start + 3] != 'X' && board[start + 3] != 'O') return start + 3;
                if (board[start + 3] == symbol && board[start + 6] == symbol && board[start] != 'X' && board[start] != 'O') return start;
            }

            // Check diagonals
            if (board[0] == symbol && board[4] == symbol && board[8] != 'X' && board[8] != 'O') return 8;
            if (board[0] == symbol && board[8] == symbol && board[4] != 'X' && board[4] != 'O') return 4;
            if (board[4] == symbol && board[8] == symbol && board[0] != 'X' && board[0] != 'O') return 0;

            if (board[2] == symbol && board[4] == symbol && board[6] != 'X' && board[6] != 'O') return 6;
            if (board[2] == symbol && board[6] == symbol && board[4] != 'X' && board[4] != 'O') return 4;
            if (board[4] == symbol && board[6] == symbol && board[2] != 'X' && board[2] != 'O') return 2;

            return -1;
        } //Copy paste FTW!

        static char CheckWinner(){
            // Rows
            for (int i = 0; i < 9; i += 3){
                if (board[i] == board[i + 1] && board[i + 1] == board[i + 2]) return board[i];
            }

            // Columns
            for (int i = 0; i < 3; i++){
                if (board[i] == board[i + 3] && board[i + 3] == board[i + 6]) return board[i];
            }

            // Diagonals
            if (board[0] == board[4] && board[4] == board[8]) return board[0];
            if (board[2] == board[4] && board[4] == board[6]) return board[2];

            return ' ';
        }

        static bool IsBoardFull(){
            for (int i = 0; i < 9; i++){
                if (board[i] != 'X' && board[i] != 'O') return false;
            }
            return true;
        }

        static bool AskPlayAgain(){ //Easy function to include replayability{
            Console.WriteLine("Play Again (Y) or Quit (N)?");
            string response = Console.ReadLine().ToUpper();
            return response == "Y";
        }
    }
}