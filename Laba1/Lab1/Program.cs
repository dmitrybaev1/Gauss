using System;
using System.IO;

namespace Lab1
{
    static class Program
    {
        static Gauss gauss;
        static Line[] lines;
        static void Main(string[] args)
        {
            int a = 0;
            while (a!=2)
            {
                Console.WriteLine("------Метод Гаусса------");
                Console.WriteLine("1-начать\n2-выйти");
                if (int.TryParse(Console.ReadLine(), out a))
                {
                    if (a == 1)
                    {
                        int n = 0;
                        while (n != 1 && n != 2 && n != 3 && n != 4)
                        {
                            Console.Write("Введите '1', чтобы ввести данные с клавиатуры,'2'- ввод из файла,'3'- случайные коэффициенты,'4'- тест :");
                            if (int.TryParse(Console.ReadLine(), out n))
                            {
                                switch (n)
                                {
                                    case 1:
                                        InputConsole();
                                        break;
                                    case 2:
                                        InputFile();
                                        break;
                                    case 3:
                                        InputRandom();
                                        break;
                                    case 4:
                                        Test();
                                        break;
                                    default:
                                        IncorrectOutput();
                                        break;
                                }
                            }
                            else
                                IncorrectOutput();
                        }
                    }
                    else if (a != 2)
                    {
                        IncorrectOutput();
                    }
                }
                else
                    IncorrectOutput();
            }
        }
        static void Test()
        {
            lines = new Line[5];
            decimal[] arr1 = { 1, -1, 3, 1, 5, 1 };
            lines[0] = new Line(arr1);
            decimal[] arr2 = { 4, -1, 5, 4, 4, 1 };
            lines[1] = new Line(arr2);
            decimal[] arr3 = { 2, -2, 4, 1, 6, -1 };
            lines[2] = new Line(arr3);
            decimal[] arr4 = { 1, -4, 5, -1, 3, 1 };
            lines[3] = new Line(arr4);
            decimal[] arr5 = { 3, -2, 6, 3, 5, -1 };
            lines[4] = new Line(arr5);
            Init();
        }
        static void InputFile()
        {
            bool isCorrect = true;
            string path = @"C:\Users\dmitr\Documents\vychmat\Lab1\Lab1.txt";
            string[] s = File.ReadAllLines(path);
            lines = new Line[s.Length];
                for (int i = 0; i < s.Length; i++)
                {
                    string[] arr = s[i].Split(' ');
                    decimal[] d = new decimal[arr.Length];
                    if (arr.Length == s.Length + 1&&(s.Length>=2&&s.Length<=20))
                    {
                        for (int j = 0; j < arr.Length; j++)
                        {
                            if (!decimal.TryParse(arr[j], out d[j]))
                            {
                                isCorrect = false;
                                break;
                            }
                        }
                    }
                    else
                        isCorrect = false;
                if (isCorrect)
                    lines[i] = new Line(d);
                else
                    break;
                }
            if (isCorrect)
                Init();
            else
                IncorrectOutput();
        }
        static void InputRandom()
        {
            Random random = new Random();
            int d = 0;
            while (d <= 1 || d > 20)
            {
                Console.Write("Введите количество неизвестных:");
                string dimension = Console.ReadLine();
                if (int.TryParse(dimension, out d) && d >= 2 && d <= 20)
                    lines = new Line[d];
                else
                    Console.WriteLine("Количество неизвестных должно быть больше 1 и не более 20!");
            }
            for (int i = 0; i < d; i++)
            {
                decimal[] arr = new decimal[lines.Length + 1];
                for (int j = 0; j < (d + 1); j++)
                {
                    int k = random.Next(0,16);
                    if (k > 0) {
                        int b = random.Next(1, 3);
                        if (b == 1)
                            arr[j] = k;
                        else
                            arr[j] = -k;
                    }                   
                }
                lines[i] = new Line(arr);
            }
            Init();
        }
        static void InputConsole()
        {
            int d = 0;
            while (d <= 1||d>20)
            {
                Console.Write("Введите количество неизвестных:");
                string dimension = Console.ReadLine();
                    if (int.TryParse(dimension, out d) && (d >= 2&&d<=20))
                        lines = new Line[d];
                    else
                        Console.WriteLine("Количество неизвестных должно быть больше 1 и не более 20!");               
            }
            Console.WriteLine("Введите коэффициенты:");
            for (int i = 0; i < d; i++)
            {
                decimal[] arr = new decimal[lines.Length+1];
                for (int j = 0; j < (d + 1); j++)
                {
                    if (j != d)
                    {
                        Console.Write((j + 1) + " коэффициент " + (i + 1) + " строки:");
                    }
                    else
                    {
                        Console.Write("Свободный коэффициент " + (i + 1) + " строки:");
                    }
                    while (!decimal.TryParse(Console.ReadLine(), out arr[j]))
                    {
                        IncorrectOutput();
                    }
                }
                lines[i] = new Line(arr);
            }
            Init();
        }
        static void Init()
        {
            gauss = new Gauss(lines);
            Do();
        }
        static void Do()
        {
            OutputInitialMatrix();
            try
            {
                lines = gauss.GetTriangularMatrix();
                TriangularMatrixOutput();
                decimal d = gauss.GetDeterminant();
                OutputDeterminant(d);
                decimal[] roots = gauss.GetRoots();
                OutputRoots(roots);
                decimal[] inconsistencies = gauss.GetInconsistencies();
                OutputInconsistencies(inconsistencies);
            }
            catch(ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
                
        }
        static void IncorrectOutput()
        {
            Console.WriteLine("Некорректный ввод. Повторите попытку");
        }
        static void OutputInconsistencies(decimal[] inc)
        {
            Console.WriteLine("______________");
            Console.WriteLine("Невязки:");
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine("{0:G1}",inc[i]);
            }
            Console.WriteLine("______________");
        }
        static void OutputRoots(decimal[] roots)
        {
            Console.WriteLine("______________");
            Console.WriteLine("Корни:");
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine("x"+(i+1)+"={0:0.###}",roots[i]);
            }
            Console.WriteLine("______________");
        }
        static void TriangularMatrixOutput()
        {
            Console.WriteLine("______________");
            Console.WriteLine("Треугольная матрица:");
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine(lines[i]);
            }
            Console.WriteLine("______________");
        }
        static void OutputDeterminant(decimal determinant)
        {
            Console.WriteLine("Определитель:{0:0.###}" ,determinant);
        }
        static void OutputInitialMatrix()
        {
            Console.WriteLine("______________");
            Console.WriteLine("Матрица:");
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine(lines[i]);
            }
            Console.WriteLine("______________");
        }
    }
}
