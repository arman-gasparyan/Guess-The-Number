using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessNumber
{
    class  Game 
    {
        private int[] _savedNumber = new int[4];
        private int[] _guessNumber = new int[4];
        public int Score { get; set; } = 0;
        public bool Turn { get; set; }


        public int[] GuessNumber
        {
            get
            {
                return _guessNumber;
            }

            set
            {
                if (value.Length == 4 && !Game.NumberRecurrentChecker(value))
                {
                    _guessNumber = value;
                }

            }
        }

        public int[] SavedNumber
        {
            get
            {
                return _savedNumber;
            }

            set
            {
                if (value.Length == 4 && !Game.NumberRecurrentChecker(value))
                {
                    _savedNumber = value;
                }

            }
        }

        public static bool NumberRecurrentChecker(int[] items)
        {
            for (var i = 0; i < items.Length; i++)
            {
                for (var j = 0; j < items.Length; j++)
                {
                    if (items[i] == items[j] && i != j)
                    {
                        return true;
                    }
                    if (items[0] == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static void IndexChacker(int[] savedArr, int[] guessArr, Indexes indexInfo)
        {
            indexInfo.Position = 0;
            indexInfo.Digits = 0;

            for (var i = 0; i < savedArr.Length; i++)
            {
                for (var j = 0; j < guessArr.Length; j++)
                {
                    if (savedArr[i] == guessArr[j] && i == j)
                    {
                        indexInfo.Position++;
                    }
                    if (savedArr[i] == guessArr[j] && i != j)
                    {
                        indexInfo.Digits++;
                    }
                }
            }
        }

        // 
        public static List<int> GetNumbers(Indexes indexInfo, List<int> numbers, int[] guessNumber)
        {
            List<int> getNumbers = new List<int>();
            Indexes _indexInfo = new Indexes();
            foreach (var n in numbers)
            {
                IndexChacker(IntToArray(n), guessNumber, _indexInfo);
                if (indexInfo.Digits == _indexInfo.Digits && indexInfo.Position == _indexInfo.Position)
                {
                    getNumbers.Add(n);
                }
            }
            return getNumbers;
        }

        //
        public static void ScoreChacker(Indexes indexInfo, Game g)

        {
            if (indexInfo.Position == 1)
            {
                g.Score += 100;
            }
            if (indexInfo.Position == 2)
            {
                g.Score += 200;
            }
            if (indexInfo.Position == 3)
            {
                g.Score += 300;
            }
            if (indexInfo.Position == 4)
            {
                g.Score += 400;
            }
        }


        public static int[] IntToArray(int a)
        {
            if (a < 1000 || a >= 9999)
            {
                throw new Exception("InvalidNumber");
            }
            var arr = new int[4];
            for (var i = arr.Length - 1; i >= 0; i--)
            {
                arr[i] = a % 10;
                a /= 10;
            }
            return arr;
        }

        public static int[] GetRandomArray(List<int> numbers)
        {
            Random rnd = new Random();
            int[] arr = new int[4];
            if (numbers.Count != 0)
            {
                arr = IntToArray(numbers[rnd.Next(0, numbers.Count)]);
            }

            return arr;
        }


        public static int ArrayToInt(int[] arr)
        {
            string num = "";
            for (int i = 0; i < arr.Length; i++)
            {
                num += Convert.ToString(arr[i]);
            }
            return int.Parse(num);
        }

     
    }
}
