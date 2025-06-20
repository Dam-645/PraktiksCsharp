using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class LinearEquationSolver
{
    public static void Solve(int a, int b)
    {
        if (a == 0 && b == 0)
        {
            Console.WriteLine("Уравнение имеет бесконечное множество решений");
            return;
        }

        if (a == 0)
        {
            Console.WriteLine("Уравнение не имеет решений");
            return;
        }

        if (b % a != 0)
        {
            Console.WriteLine("Уравнение не имеет целочисленных решений");
            return;
        }

        int x = b / a;
        Console.WriteLine($"Решение уравнения: x = {x}");
    }
}

class NumberChecker
{
    public static void CheckOdd(int number)
    {
        if (number % 2 == 0)
            throw new ArithmeticException("Четное число");
    }
}

class CharArrayException : Exception
{
    private readonly char[] _characters;

    public CharArrayException(int size)
    {
        _characters = new char[size];
        for (int i = 0; i < size; i++)
        {
            _characters[i] = (char)('A' + i);
        }
    }

    public string GetCharacters() => string.Join(", ", _characters);
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Задача 1: Решение уравнения Ax = B ===");
        SolveEquationDemo();

        Console.WriteLine("\n=== Задача 2: Проверка четности числа ===");
        CheckNumberDemo();

        Console.WriteLine("\n=== Задача 3: Пользовательское исключение ===");
        CustomExceptionDemo();
    }

    static void SolveEquationDemo()
    {
        try
        {
            Console.Write("Введите коэффициент A: ");
            int a = int.Parse(Console.ReadLine());

            Console.Write("Введите коэффициент B: ");
            int b = int.Parse(Console.ReadLine());

            LinearEquationSolver.Solve(a, b);
        }
        catch (FormatException)
        {
            Console.WriteLine("Ошибка: Введено не число");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Ошибка: Число слишком большое");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Неожиданная ошибка: {ex.Message}");
        }
    }

    static void CheckNumberDemo()
    {
        while (true)
        {
            try
            {
                Console.Write("Введите целое число (или 'q' для выхода): ");
                string input = Console.ReadLine();

                if (input?.ToLower() == "q")
                {
                    Console.WriteLine("Завершение программы...");
                    break;
                }

                int number = int.Parse(input);

                try
                {
                    NumberChecker.CheckOdd(number);
                    Console.WriteLine("Введено нечетное число");
                }
                catch (ArithmeticException)
                {
                    Console.WriteLine("Введено четное число");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: Введено не число");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Ошибка: Число слишком большое");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Неожиданная ошибка: {ex.Message}");
            }
        }
    }

    static void CustomExceptionDemo()
    {
        try
        {
            throw new CharArrayException(5);
        }
        catch (CharArrayException ex)
        {
            Console.WriteLine($"Сгенерированный массив символов: {ex.GetCharacters()}");
        }
    }
}
