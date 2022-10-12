using System;
using System.IO;
using System.Threading;
using System.Timers;



namespace Laboratory
{
    public class DataFromFile
    {
        // Создаём класс для хранения информации из файла
        public int firstTime;
        public int secondTime;
        public string position;
        public string color;
        public string word;
    }

    public class Programm
    {
        static int width = 100;
        static int height = 40;
        static System.Timers.Timer aTimer;
        static int length = File.ReadAllLines("C:\\Users\\User\\Desktop\\Лабы ulearn\\Laboratory-8 ex-1\\Laboratory-8 ex-1\\Laboratory-8 ex-1.txt").Length;
        static DataFromFile[] subtitles = new DataFromFile[length];
        static int time = 0;
        private static void SetTimer()
        {
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            foreach (var subtitle in subtitles)
            {
                if (subtitle.firstTime == time) WriteWord(subtitle);
                if (subtitle.secondTime == time) DeleteWord(subtitle);
            }
            time++;
        }
        public static void DrawScreen()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 || i == height - 1) Console.Write("*");
                    else if (j == 0 || j == width - 1) Console.Write("|");
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
        public static void ChangeColor(string color)
        {
            if (color != null) color = color.Replace(" ", "");
            if (color == "Red") Console.ForegroundColor = ConsoleColor.Red;
            if (color == "Green") Console.ForegroundColor = ConsoleColor.Green;
            if (color == "White") Console.ForegroundColor = ConsoleColor.White;
            if (color == "Blue") Console.ForegroundColor = ConsoleColor.Blue;
        }
        public static void WriteWord(DataFromFile subtitle)
        {
            ChangePosition(subtitle.position, subtitle.word.Length);
            ChangeColor(subtitle.color);
            Console.WriteLine(subtitle.word);
        }
        public static void ChangePosition(string position, int lengthOfWord)
        {
            switch (position)
            {
                case "Top":
                    Console.SetCursorPosition((width - 2 - lengthOfWord) / 2, 1);
                    break;
                case "Bottom":
                    Console.SetCursorPosition((width - 2 - lengthOfWord) / 2, height - 2);
                    break;
                case "Right":
                    Console.SetCursorPosition(width - 1 - lengthOfWord, height / 2 - 1);
                    break;
                case "Left":
                    Console.SetCursorPosition(1, height / 2 - 1);
                    break;
                default:
                    Console.SetCursorPosition((width - lengthOfWord) / 2, height / 2);
                    break;
            }
        }
        public static void DeleteWord(DataFromFile subtitle)
        {
            ChangePosition(subtitle.position, subtitle.word.Length);
            for (int i = 0; i < subtitle.word.Length; i++)
            {
                Console.Write(" ");
            }
        }
        public static void DataToClass(string[] appartedData, int count)
        {
            //Метод расскладывает информацию в class DataFromFile
            subtitles[count] = new DataFromFile();
            subtitles[count].firstTime = int.Parse(appartedData[0].Replace(":", "")) % 100;
            if (appartedData.Length == 2)
            {
                subtitles[count].secondTime = int.Parse(appartedData[1].Substring(0, 5).Replace(":", ""));
                subtitles[count].word = appartedData[1].Substring(6);
            }
            else
            {
                subtitles[count].secondTime = int.Parse(appartedData[1].Replace(":", "")) % 100;
                subtitles[count].position = appartedData[2];
                subtitles[count].color = appartedData[3];
                subtitles[count].word = appartedData[4];
            }
        }
        public static void GetInformationFromFile()
        {
            // Метод считывает информацию из файла и вызывает метод, который расскладывает информацию в class           
            char[] separators = { '-', '[', ',', ']' };
            int count = 0;
            foreach (var str in File.ReadLines("C:\\Users\\User\\Desktop\\Лабы ulearn\\Laboratory-8 ex-1\\Laboratory-8 ex-1\\Laboratory-8 ex-1.txt"))
            {
                if (str.Contains('[')) str.Replace(" ", "");
                string[] appartedData = str.Split(separators);
                DataToClass(appartedData, count);
                count++;
            }
        }
        static void Main()
        {
            GetInformationFromFile();
            DrawScreen();
            SetTimer();
            Console.ReadKey();
        }
    }
}