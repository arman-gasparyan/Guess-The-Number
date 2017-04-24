using System;
using System.Collections.Generic;
using System.IO;

namespace GuessNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press Enter to Start a Game");
            while (Console.ReadKey().Key == ConsoleKey.Enter)
            {

                Console.Clear();

                string _folder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                var path = Path.Combine(_folder, "GuessNumberHistory.txt");

                if (!File.Exists(path))
                {
                    File.CreateText(path);
                }

                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.Write("Enter Your Name" + "\t" + " - ");

                string _playerName = Console.ReadLine();

                Game _player = new Game();

                Game _computer = new Game();

                bool isException = false;

                List<int> numbers = new List<int>();

                for (int i = 1023; i < 9877; i++)
                {
                    if (!Game.NumberRecurrentChecker(Game.IntToArray(i)))
                    {
                        numbers.Add(i);
                    }
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Save Your Number  - ");
                do
                {
                    isException = false;
                    try
                    {
                        _player.SavedNumber = Game.IntToArray(int.Parse(Console.ReadLine()));
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid Number");
                        Console.Write("Save Your Number  - ");
                        isException = true;
                    }
                   
                }
                while (isException);
                Console.WriteLine(" Computer Saved Number " + "XXXX");
                Console.ForegroundColor = ConsoleColor.Red;
                _computer.SavedNumber = Game.GetRandomArray(numbers);
                Console.WriteLine("Number" + "\t" + "Digits" + "\t" + "Position");
                Indexes _playerIndexInfo = new Indexes();
                Indexes _computerIndexInfo = new Indexes();
                _player.Turn = true;
                bool isGameOver = false;


                while (!isGameOver)
                {
                    if (_computer.Turn)
                    {
                        _computer.GuessNumber = Game.GetRandomArray(numbers);
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("Computer" + " " + Game.ArrayToInt(_computer.GuessNumber));
                        Game.IndexChacker(_player.SavedNumber, _computer.GuessNumber, _playerIndexInfo);
                        numbers = Game.GetNumbers(_playerIndexInfo, numbers, _computer.GuessNumber);
                        Game.ScoreChacker(_playerIndexInfo, _computer);
                        Console.WriteLine(Game.ArrayToInt(_computer.GuessNumber) + "\t" + _playerIndexInfo.Digits + "\t" + _playerIndexInfo.Position + "\t" + "Score" + "\t" + _computer.Score);
                        File.WriteAllText(path, "Computer Score" + "\t" + _computer.Score);
                        _player.Turn = true;
                        _computer.Turn = false;
                    }

                    else if (_player.Turn)
                    {
                        do
                        {
                            isException = false;
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(_playerName + "\t");
                            try
                            {
                                _player.GuessNumber = Game.IntToArray(int.Parse(Console.ReadLine()));
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Invalid Number");
                                isException = true;
                            }
                            if (!isException)
                            {
                                Game.IndexChacker(_computer.SavedNumber, _player.GuessNumber, _computerIndexInfo);
                                Game.ScoreChacker(_computerIndexInfo, _player);
                                Console.WriteLine(Game.ArrayToInt(_player.GuessNumber) + "\t" + _computerIndexInfo.Digits + "\t" + _computerIndexInfo.Position + "\t" + "Score" + "\t" + _player.Score);
                                File.WriteAllText(path, _playerName + "\t" + "Score" + "\t" + _player.Score);
                                _player.Turn = false;
                                _computer.Turn = true;
                            }

                        } while (isException);

                    }

                    if (Game.ArrayToInt(_player.GuessNumber) == Game.ArrayToInt(_computer.SavedNumber))
                    {
                        isGameOver = true;
                        Console.WriteLine(_playerName + "\t" + "You Are Win");
                        Console.WriteLine(_playerName + "\t" + "Computer Is Win");
                        Console.WriteLine(File.ReadAllText(path));
                    }

                    if (Game.ArrayToInt(_computer.GuessNumber) == Game.ArrayToInt(_player.SavedNumber))
                    {
                        isGameOver = true;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Computer" + "\t" + "You Are Win");
                        Console.WriteLine(_playerName + "\t" + "Computer Is Win");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine(File.ReadAllText(path));
                    }

                }

                Console.WriteLine("Computer Number is - " + Game.ArrayToInt(_computer.SavedNumber));
                Console.WriteLine("Press Enter To Start a New Game");




            }

        }



    }
}
