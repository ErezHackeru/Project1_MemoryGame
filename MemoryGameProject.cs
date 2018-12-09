using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    class MemoryGameProject
    {
        static void Main(string[] args)
        {
            int[,] matrix = PartA();

            PartB(matrix);

            Console.ReadLine();
        }

        private static void PartB(int[,] matrix)
        {
            int ChosenRow = 0, ChosenColumn = 0;
            bool Gamestop = false;
            int TrialCounter = 0;
            int FirstNumToCheck = -1, SecondNumToCheck = -2;
            int Gamer1Points = 0, Gamer2Points = 0;
            string player = "Player 1";

            while (!Gamestop)
            {
                if (TrialCounter % 2 == 0)
                {
                    FirstNumToCheck = 0;
                    SecondNumToCheck = 0;
                }
                Console.WriteLine($"This is {player} turn: ");
                
                do
                {
                    Console.WriteLine("Please write a row number (zero based):");
                    ChosenRow = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Please write a column number (zero based):");
                    ChosenColumn = Convert.ToInt32(Console.ReadLine());

                } while ((ChosenRow < 0) && (ChosenColumn < 0) && (ChosenRow >= matrix.GetLength(0)) && (ChosenColumn >= matrix.GetLength(1)));

                Console.WriteLine($"The card here is: {matrix[ChosenRow, ChosenColumn]}");

                if (TrialCounter % 2 == 1)
                {
                    FirstNumToCheck = matrix[ChosenRow, ChosenColumn];
                }
                else if (TrialCounter % 2 == 0)
                {
                    SecondNumToCheck = matrix[ChosenRow, ChosenColumn];
                }
                
                if (player == "Player 1")
                    Gamer1Points = PartC(matrix, FirstNumToCheck, SecondNumToCheck, Gamer1Points, ChosenRow, ChosenColumn);
                else if (player == "Player 2")
                    Gamer2Points = PartC(matrix, FirstNumToCheck, SecondNumToCheck, Gamer2Points, ChosenRow, ChosenColumn);

                TrialCounter++;
                if ((FirstNumToCheck != SecondNumToCheck) && (TrialCounter % 2 == 0))
                {
                    switchPlayer(ref player);
                }

                //Checking if to stop the game:
                if ((Gamer1Points + Gamer2Points) == (matrix.GetLength(0) * matrix.GetLength(1)) / 2)
                {
                    Console.WriteLine($"The number of trials was: {TrialCounter / 2}");
                    Gamestop = true;
                }

            }
        }

        private static void switchPlayer(ref string player)
        {
            if (player == "Player 1")
                player = "Player 2";
            else if (player == "Player 2")
                player = "Player 1";
        }

        private static int PartC(int[,] matrix, int FirstNumToCheck, int SecondNumToCheck, int GamerPoints, int ChosenRow, int ChosenColumn)
        {
            //if the gamer has a point:
            if (FirstNumToCheck == SecondNumToCheck)
            {
                GamerPoints++;
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j] == FirstNumToCheck)
                            matrix[i, j] = 0;
                    }
                }

                Console.WriteLine("The new matrix now is:");
                PrintMatrixAsNumbers(matrix);
                
            }
            PartD_PrintMatrixAsNumbers(matrix, ChosenRow, ChosenColumn);
            return GamerPoints;
        }

        private static void PrintMatrixAsNumbers(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j].ToString().Length == 1)
                        Console.Write($"{ matrix[i, j]}  ");
                    else
                        Console.Write($"{ matrix[i, j]} ");
                }
                Console.WriteLine();
            }
        }

        private static void PartD_PrintMatrixAsNumbers(int[,] matrix, int ChosenRow, int ChosenColumn)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 0)
                        Console.Write($"{ matrix[i, j]} , ");
                    else if ((i == ChosenRow) && (j == ChosenColumn))
                        Console.Write($"{ matrix[i, j]} , ");
                    else
                        Console.Write($"X , ");
                }
                Console.WriteLine();
            }
        }

        private static int[,] PartA()
        {
            int matrixSize = -1;
            while ((matrixSize > 8) || (matrixSize < 1) || (matrixSize % 2 != 0))
            {
                Console.WriteLine("Please write a size for the matrix");
                matrixSize = Convert.ToInt32(Console.ReadLine());
            }
            int[,] matrix = new int[matrixSize, matrixSize];

            int zugot = (matrixSize * matrixSize) / 2;
            Random randNum = new Random();
            int row = 0, column = 0;
            bool matrixComplete = false;

            while (!matrixComplete)
            {
                for (int i = 1; i <= zugot; i++)
                {
                    // do twice the while loop:
                    for (int j = 0; j < 2; j++)
                    {
                        while (matrix[row, column] != 0)
                        {
                            row = randNum.Next(0, matrixSize);
                            column = randNum.Next(0, matrixSize);

                        }
                        matrix[row, column] = i;
                    }
                }
                matrixComplete = true;
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j] == 0)
                            matrixComplete = false;
                    }

                }
            }
            //print the matrix:
            Console.WriteLine("The matrix is:");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j].ToString().Length == 1)
                        Console.Write($"{ matrix[i, j]}  ");
                    else
                        Console.Write($"{ matrix[i, j]} ");
                }
                Console.WriteLine();
            }

            return matrix;
        }
    }
}
