using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Rectangle
{
    private double x1, y1;
    private double x2, y2;

    // Конструктор по умолчанию
    public Rectangle()
    {
        x1 = 0;
        y1 = 1;
        x2 = 1;
        y2 = 0;
    }

    // Конструктор, принимающий координаты
    public Rectangle(double x1, double y1, double x2, double y2)
    {
        // Убеждаемся, что x1,y1 - верхний левый угол, а x2,y2 - нижний правый
        this.x1 = Math.Min(x1, x2);
        this.y1 = Math.Max(y1, y2);
        this.x2 = Math.Max(x1, x2);
        this.y2 = Math.Min(y1, y2);
    }

    // Вычисление периметра
    public double CalculatePerimeter()
    {
        double width = x2 - x1;
        double height = y1 - y2;
        return 2 * (width + height);
    }

    // Вычисление площади
    public double CalculateArea()
    {
        double width = x2 - x1;
        double height = y1 - y2;
        return width * height;
    }

    // Метод для вывода информации о прямоугольнике
    public void PrintInfo(string label)
    {
        Console.WriteLine($"{label}:");
        Console.WriteLine($"  Координаты верхнего левого угла: ({x1}, {y1})");
        Console.WriteLine($"  Координаты нижнего правого угла: ({x2}, {y2})");
        Console.WriteLine($"  Периметр = {CalculatePerimeter()}");
        Console.WriteLine($"  Площадь = {CalculateArea()}");
    }
}

class Program
{
    static void Main()
    {
        Rectangle userRectangle = null; // Инициализируем null, чтобы было возможно обратиться к нему позже.
        Rectangle defaultRectangle = new Rectangle(); // Создаем прямоугольник по умолчанию

        // Получаем координаты пользовательского прямоугольника
        while (userRectangle == null) // Повторяем, пока не получим корректные координаты.
        {
            try
            {
                Console.WriteLine("Введите координаты левого верхнего угла (x1 y1 через пробел):");
                string[] coords1 = Console.ReadLine().Split();
                if (coords1.Length != 2) throw new ArgumentException("Необходимо ввести два числа (x1 и y1).");

                Console.WriteLine("Введите координаты правого нижнего угла (x2 y2 через пробел):");
                string[] coords2 = Console.ReadLine().Split();
                if (coords2.Length != 2) throw new ArgumentException("Необходимо ввести два числа (x2 и y2).");

                double x1 = double.Parse(coords1[0]);
                double y1 = double.Parse(coords1[1]);
                double x2 = double.Parse(coords2[0]);
                double y2 = double.Parse(coords2[1]);

                userRectangle = new Rectangle(x1, y1, x2, y2); // Пытаемся создать прямоугольник. Если координаты некорректные, будет выброшено исключение.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка ввода: {ex.Message}"); // Обрабатываем все ошибки, возникшие при парсинге и создании прямоугольника.
            }
        }

        // Выводим информацию о прямоугольниках
        Console.WriteLine("\nРезультаты:");
        userRectangle.PrintInfo("Пользовательский прямоугольник");
        defaultRectangle.PrintInfo("Прямоугольник по умолчанию");

        Console.ReadKey(); // Ожидаем нажатия клавиши перед закрытием консоли.
    }
}
