using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization; 

public class NumberMagic
{
  
    public static double GetHypotenuse(double sideA, double sideB)
    {
        double squareA = Math.Pow(sideA, 2);
        double squareB = Math.Pow(sideB, 2);
        return Math.Sqrt(squareA + squareB);
    }

   
    public static (double Floor, double Ceiling) GetFloorAndCeiling(double number)
    {
        return (Math.Floor(number), Math.Ceiling(number));
    }

    
    public static void ShowTrigonometricValues(double angleInDegrees)
    {
        double radians = angleInDegrees * Math.PI / 180;
        var sin = Math.Sin(radians);
        var cos = Math.Cos(radians);
        var tan = Math.Tan(radians);

        Console.WriteLine($"Синус: {sin:F4}, Косинус: {cos:F4}, Тангенс: {tan:F4}");
    }

    
    public static void DisplayRandomNumbers()
    {
        Random rnd = new Random();
        var randomNumbers = Enumerable.Range(0, 10)
                                      .Select(_ => rnd.Next(1, 101))
                                      .ToList();

        Console.WriteLine("10 случайных чисел от 1 до 100:");
        Console.WriteLine(string.Join(" ", randomNumbers));
    }

    
    public static (double Min, double Max) GetMinMax(double[] numbers)
    {
        if (numbers == null) throw new ArgumentNullException(nameof(numbers), "Массив не может быть null.");
        if (numbers.Length == 0) throw new ArgumentException("Массив не может быть пустым.");

        return (numbers.Min(), numbers.Max());
    }

    public static void ReadAndConvertToInt()
    {
        Console.Write("Введите целое число: ");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int number))
        {
            Console.WriteLine($"Вы ввели: {number:N0}"); 
        }
        else
        {
            Console.WriteLine("Ошибка: Не удалось распознать целое число.");
        }
    }

    
    public static string ToBinaryString(int number)
    {
        return Convert.ToString(number, 2).PadLeft(8, '0'); 
    }

    
    public static DateTime ParseDate(string dateString)
    {
        if (DateTime.TryParseExact(dateString, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime date))
        {
            return date;
        }
        else
        {
            throw new FormatException("Неверный формат даты. Ожидается yyyy-MM-dd.");
        }
    }

   
    public static double SafeConvertToDouble(object obj)
    {
        if (obj is double d)
        {
            return d;
        }
        else if (obj is int i)
        {
            return Convert.ToDouble(i);
        }
        else if (obj is string s)
        {
            if (double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException($"Невозможно преобразовать строку '{s}' в double.", nameof(obj));
            }
        }
        else
        {
            throw new ArgumentException($"Невозможно преобразовать объект типа {obj?.GetType()} в double.", nameof(obj));
        }
    }

    public static bool? TryConvertToBoolean(string input)
    {
        try
        {
            return Convert.ToBoolean(input);
        }
        catch (FormatException)
        {
            Console.WriteLine("Недопустимый формат для булевого значения.");
            return null;
        }
    }

   
    public static void RunDemos()
    {
        Console.WriteLine("1. Гипотенуза с катетами 3 и 4: " + GetHypotenuse(3, 4));

        var (floor, ceiling) = GetFloorAndCeiling(3.7);
        Console.WriteLine($"2. Округление 3.7: вниз = {floor}, вверх = {ceiling}");

        Console.WriteLine("3. Тригонометрические функции для 30 градусов:");
        ShowTrigonometricValues(30);

        Console.WriteLine("4. Генерация случайных чисел:");
        DisplayRandomNumbers();

        double[] numbers = { 1.5, 2.3, 0.7, 4.8, 3.2 };
        var (min, max) = GetMinMax(numbers);
        Console.WriteLine($"5. Минимальное: {min}, Максимальное: {max}");

        Console.WriteLine("6. Конвертация строки в число:");
        ReadAndConvertToInt();

        Console.WriteLine("7. Число 10 в двоичной системе: " + ToBinaryString(10));

        DateTime date = ParseDate("2025-05-26");
        Console.WriteLine($"8. Конвертация даты: {date:dd.MM.yyyy}");

        Console.WriteLine("9. Конвертация объекта '3.14' в double: " + SafeConvertToDouble("3.14"));

        Console.WriteLine("10. Конвертация в bool:");
        bool? boolResult = TryConvertToBoolean("True");
        Console.WriteLine("'True' -> " + boolResult);
        boolResult = TryConvertToBoolean("0");
        Console.WriteLine("'0' -> " + boolResult);
    }

    public static void Main(string[] args)
    {
        RunDemos();
    }
}
