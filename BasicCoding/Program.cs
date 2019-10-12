using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicCoding
{
    class Program
    {
        static void Main(string[] args)
        {

            //вызов задачи 1):
            int[] ar = { 7, 1, 2, 3, 4, 5, 6, 7, 68, 69, 70, 15, 17 };
            int max = getMax(ar, 0);
            Console.WriteLine($"Assignment result #1 = {max}");

            //вызов задачи 2):
            int inserted = InsertNumber(8, 15, 0, 0);
            Console.WriteLine($"Assignment result #2 = {inserted}");

            //вызов задачи 3):
            int index = findElement(ar);
            Console.WriteLine($"Assignment result #3 = {index}");

            //вызов задачи 4):
            string word = getLatinLetters("qwqerrrqrtrgtrrrвааааппппп12@RY","wwwкеееIOopppOOOtyybbbn)003T");
            Console.WriteLine($"Assignment result #4 = {word}");

            //вызов задачи 5):
            long time = 0;
            int biggerNumber = FindNextBiggerNumber(3456432, out time);
            Console.WriteLine($"Assignment result #5 = {biggerNumber}");
            Console.WriteLine($"It took {time} miliseconds to process Assignment #5");
            //вызов задачи 6):

            int [] filteredNum = FilterDigit(ar, 7);
            Console.Write($"Assignment result #6: ");
            foreach (var item in filteredNum)
            {
                Console.Write($"{item} ");
            }


        }

        //Даны два целых знаковых четырех байтовых числа и две позиции битов i и j (i<j). 
        //Реализовать алгоритм InsertNumber вставки битов с j-ого по i-ый бит одного числа 
        //в другое так, чтобы биты второго числа занимали позиции с бита j по бит i

        public static int InsertNumber(int first, int second, int i, int j)
        {
            if (i > j)
            {
                throw new ArgumentException("i cannot be greater than j");
            }

            if (i < 0 || i > 31)
            {
                throw new ArgumentOutOfRangeException("i must be in range from 0 to 31.");
            }

            if (j < 0 || j > 31)
            {
                throw new ArgumentOutOfRangeException("j must be in range from 0 to 31.");
            }

            int result = (int)(uint.MaxValue >> (31 - j + i) << i);
            return first & ~result | (second << i) & result;
        }

        //Реализовать рекурсивный алгоритм поиска максимального элемента в неотсортированном целочисленом массиве.

        static int getMax(int[] arr, int index)
        {
            if (index + 1 == arr.Length)
            {
                return arr[index];
            }
            else
            {
                return Math.Max(arr[index], getMax(arr, index + 1));
            }
        }

        //Реализовать алгоритм поиска в вещественном массиве индекса элемента, для которого 
        //сумма элементов слева и сумма элементов справа равны.Если такого элемента не существует вернуть null (или -1).

        static int findElement(int[] arr)
        {
            int right_sum = 0, left_sum = 0;

            for (int i = 1; i < arr.Length; i++)
                right_sum += arr[i];

            for (int i = 0, j = 1; j < arr.Length; i++, j++)
            {
                right_sum -= arr[j];
                left_sum += arr[i];

                if (left_sum == right_sum)
                    return i + 1;
            }

            return -1;
        }

        //Реализовать алгоритм конкатенации двух строк, содержащих только символы 
        //латинского алфавита, исключая повторяющиеся символы.

        static string getLatinLetters(string s1, string s2)
        {
            string combined = s1 + s2;

            string newString = string.Empty;
            List<char> array = new List<char>();
            foreach (char c in combined)
            {
                if (((int)c>=65 && (int)c<91) || ((int)c >= 97 && (int)c < 123))
                {
                    if (array.Contains(c))
                        continue;

                    newString += c.ToString();
                    array.Add(c);
                }
            }
            return newString;

        }


        //Реализовать метод FindNextBiggerNumber, который принимает положительное целое число и 
        //возвращает ближайшее наибольшее целое, состоящее из цифр исходного числа,
        //и null (или -1), если такого числа не существует.

        private static int FindNextBiggerNumber(int number, out long delay)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            int[] buffer = new int[number.ToString().Length];

            for (int i = buffer.Length - 1; i > -1; i--)
            {
                buffer[i] = number % 10;
                number /= 10;
            }
            int index = IndexSearching(buffer);

            if (index == -1)
            {
                stopwatch.Stop();
                delay = stopwatch.ElapsedMilliseconds;
                return -1;
            }
            int temp = 0;
            if (index < buffer.Length - 1)
            {
                temp = buffer[index];
                buffer[index] = buffer[index + 1];
                buffer[index + 1] = temp;
                Array.Sort(buffer, index + 1, buffer.Length - index - 1);
            }

            int result = 0;
            for (int i = 0; i < buffer.Length; i++)
            {
                result += (int)(buffer[i] * Math.Pow(10, buffer.Length - 1 - i));
            }

            stopwatch.Stop();
            delay = stopwatch.ElapsedMilliseconds;

            return result;
        }

        private static int IndexSearching(int[] temp)
        {
            for (int i = temp.Length - 1; i > 0; i--)
            {
                if (temp[i] > temp[i - 1])
                {
                    return (i - 1);
                }
            }
            return -1;
        }


        //Реализовать метод FilterDigit который принимает массив целых чисел и 
        //фильтрует его таким образом, чтобы на выходе остались только числа, содержащие заданную цифру

        public static int[] FilterDigit(int[] array, int key)
        {

            if (array.Length == 0)
            {
                throw new ArgumentException("Array cannot be empty.", nameof(array));
            }

            if (key < 0 || key > 9)
            {
                throw new ArgumentOutOfRangeException($"key is not the decimal digit");
            }

            List<int> list = new List<int>();

            foreach (var item in array)
            {
                if (HasKey(item, key))
                {
                    list.Add(item);
                }
            }

            return list.ToArray();
        }

        public static bool HasKey(int number, int digit)
        {
            while (number != 0)
            {
                if (Math.Abs(number % 10) == digit)
                {
                    return true;
                }

                number /= 10;
            }

            return false;
        }

    }
}

