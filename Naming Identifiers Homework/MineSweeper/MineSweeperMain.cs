namespace MineSweeper
{
    using System;
    using System.Collections.Generic;

    public class MineSweeperMain
    {   
        private const int Max = 35;
        private static string command;
        private static char[,] gameBoard = CreateGameBoard();
        private static char[,] bombs = InsertBombs();
        private static int pointsCounter = 0;
        private static bool grum = false;
        private static List<Player> players = new List<Player>(6);
        private static int red = 0;
        private static int kolona = 0;
        private static bool flag = true;       
        private static bool flag2 = false;

        private static void Main()
        {
            do
            {
                if (flag)
                {
                    Console.WriteLine(
                        "Hajde da igraem na “Mini4KI”. Probvaj si kasmeta da otkriesh poleteta bez mini4ki."
                        + " Komanda 'top' pokazva klasiraneto, 'restart' po4va nova igra, 'exit' izliza i hajde 4ao!");
                    PrintGameBoard(gameBoard);
                    flag = false;
                }

                Console.Write("Daj red i kolona : ");
                command = Console.ReadLine().Trim();
                if (command.Length >= 3)
                {
                    if (int.TryParse(command[0].ToString(), out red) && int.TryParse(command[2].ToString(), out kolona)
                        && red <= gameBoard.GetLength(0) && kolona <= gameBoard.GetLength(1))
                    {
                        command = "turn";
                    }
                }

                switch (command)
                {
                    case "top":
                        Clasification(players);
                        break;
                    case "restart":
                        gameBoard = CreateGameBoard();
                        bombs = InsertBombs();
                        PrintGameBoard(gameBoard);
                        grum = false;
                        flag = false;
                        break;
                    case "exit":
                        Console.WriteLine("4a0, 4a0, 4a0!");
                        break;
                    case "turn":
                        if (bombs[red, kolona] != '*')
                        {
                            if (bombs[red, kolona] == '-')
                            {
                                tisinahod(gameBoard, bombs, red, kolona);
                                pointsCounter++;
                            }

                            if (Max == pointsCounter)
                            {
                                flag2 = true;
                            }
                            else
                            {
                                PrintGameBoard(gameBoard);
                            }
                        }
                        else
                        {
                            grum = true;
                        }

                        break;
                    default:
                        Console.WriteLine("\nGreshka! nevalidna Komanda\n");
                        break;
                }

                if (grum)
                {
                    PrintGameBoard(bombs);
                    Console.Write("\nHrrrrrr! Umria gerojski s {0} to4ki. " + "Daj si niknejm: ", pointsCounter);
                    string niknejm = Console.ReadLine();
                    Player t = new Player(niknejm, pointsCounter);
                    if (players.Count < 5)
                    {
                        players.Add(t);
                    }
                    else
                    {
                        for (int i = 0; i < players.Count; i++)
                        {
                            if (players[i].Points < t.Points)
                            {
                                players.Insert(i, t);
                                players.RemoveAt(players.Count - 1);
                                break;
                            }
                        }
                    }

                    players.Sort((Player r1, Player r2) => r2.Name.CompareTo(r1.Name));
                    players.Sort((Player r1, Player r2) => r2.Points.CompareTo(r1.Points));
                    Clasification(players);

                    gameBoard = CreateGameBoard();
                    bombs = InsertBombs();
                    pointsCounter = 0;
                    grum = false;
                    flag = true;
                }

                if (flag2)
                {
                    Console.WriteLine("\nBRAVOOOS! Otvri 35 kletki bez kapka kryv.");
                    PrintGameBoard(bombs);
                    Console.WriteLine("Daj si imeto, batka: ");
                    string imeee = Console.ReadLine();
                    Player player = new Player(imeee, pointsCounter);
                    players.Add(player);
                    Clasification(players);
                    gameBoard = CreateGameBoard();
                    bombs = InsertBombs();
                    pointsCounter = 0;
                    flag2 = false;
                    flag = true;
                }
            }
            while (command != "exit");
            Console.WriteLine("Made in Bulgaria - Uauahahahahaha!");
            Console.WriteLine("AREEEEEEeeeeeee.");
            Console.Read();
        }

        private static void Clasification(List<Player> allTimeBestPlayers)
        {
            Console.WriteLine("\nTo4KI:");
            if (allTimeBestPlayers.Count > 0)
            {
                for (int i = 0; i < allTimeBestPlayers.Count; i++)
                {
                    Console.WriteLine("{0}. {1} --> {2} kutii", i + 1, allTimeBestPlayers[i].Name, allTimeBestPlayers[i].Points);
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("prazna klasaciq!\n");
            }
        }

        private static void tisinahod(char[,] POLE, char[,] BOMBI, int RED, int KOLONA)
        {
            char kolkoBombi = GetNumberOfBombsAroundPlayer(BOMBI, RED, KOLONA);
            BOMBI[RED, KOLONA] = kolkoBombi;
            POLE[RED, KOLONA] = kolkoBombi;
        }

        private static void PrintGameBoard(char[,] board)
        {
            int rows = board.GetLength(0);
            int cols = board.GetLength(1);
            Console.WriteLine("\n    0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("   ---------------------");
            for (int i = 0; i < rows; i++)
            {
                Console.Write("{0} | ", i);
                for (int j = 0; j < cols; j++)
                {
                    Console.Write("{0} ", board[i, j]);
                }

                Console.Write("|");
                Console.WriteLine();
            }

            Console.WriteLine("   ---------------------\n");
        }

        private static char[,] CreateGameBoard()
        {
            int boardRows = 5;
            int boardColumns = 10;
            char[,] board = new char[boardRows, boardColumns];
            for (int row = 0; row < boardRows; row++)
            {
                for (int col = 0; col < boardColumns; col++)
                {
                    board[row, col] = '?';
                }
            }

            return board;
        }

        private static char[,] InsertBombs()
        {
            int boardRows = 5;
            int boardCols = 10;
            char[,] bombsOnGameBoard = new char[boardRows, boardCols];

            for (int i = 0; i < boardRows; i++)
            {
                for (int j = 0; j < boardCols; j++)
                {
                    bombsOnGameBoard[i, j] = '-';
                }
            }

            List<int> randomNumbers = new List<int>();
            while (randomNumbers.Count < 15)
            {
                Random random = new Random();
                int randomNumber = random.Next(50);
                if (!randomNumbers.Contains(randomNumber))
                {
                    randomNumbers.Add(randomNumber);
                }
            }

            foreach (int number in randomNumbers)
            {
                int row = number / boardCols;
                int col = number % boardCols;
                if (col == 0 && number != 0)
                {
                    row--;
                    col = boardCols;
                }
                else
                {
                    col++;
                }

                bombsOnGameBoard[row, col - 1] = '*';
            }

            return bombsOnGameBoard;
        }

        //private static void smetki(char[,] pole)
        //{
        //    int kol = pole.GetLength(0);
        //    int red = pole.GetLength(1);

        //    for (int i = 0; i < kol; i++)
        //    {
        //        for (int j = 0; j < red; j++)
        //        {
        //            if (pole[i, j] != '*')
        //            {
        //                char kolkoo = GetNumberOfBombsAroundPlayer(pole, i, j);
        //                pole[i, j] = kolkoo;
        //            }
        //        }
        //    }
        //}

        private static char GetNumberOfBombsAroundPlayer(char[,] gameField, int row, int col)
        {
            int numberOfBombs = 0;
            int fieldRows = gameField.GetLength(0);
            int fieldCols = gameField.GetLength(1);

            if (row - 1 >= 0)
            {
                if (gameField[row - 1, col] == '*')
                {
                    numberOfBombs++;
                }
            }

            if (row + 1 < fieldRows)
            {
                if (gameField[row + 1, col] == '*')
                {
                    numberOfBombs++;
                }
            }

            if (col - 1 >= 0)
            {
                if (gameField[row, col - 1] == '*')
                {
                    numberOfBombs++;
                }
            }

            if (col + 1 < fieldCols)
            {
                if (gameField[row, col + 1] == '*')
                {
                    numberOfBombs++;
                }
            }

            if ((row - 1 >= 0) && (col - 1 >= 0))
            {
                if (gameField[row - 1, col - 1] == '*')
                {
                    numberOfBombs++;
                }
            }

            if ((row - 1 >= 0) && (col + 1 < fieldCols))
            {
                if (gameField[row - 1, col + 1] == '*')
                {
                    numberOfBombs++;
                }
            }

            if ((row + 1 < fieldRows) && (col - 1 >= 0))
            {
                if (gameField[row + 1, col - 1] == '*')
                {
                    numberOfBombs++;
                }
            }

            if ((row + 1 < fieldRows) && (col + 1 < fieldCols))
            {
                if (gameField[row + 1, col + 1] == '*')
                {
                    numberOfBombs++;
                }
            }

            return char.Parse(numberOfBombs.ToString());
        }
    }
}