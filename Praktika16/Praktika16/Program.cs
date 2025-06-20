using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PatternMatchingExamples
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. Простой паттерн по типу:");
            Console.WriteLine(GetObjectTypeInfo("строка"));
            Console.WriteLine(GetObjectTypeInfo(42));
            Console.WriteLine(GetObjectTypeInfo(true));
            Console.WriteLine(GetObjectTypeInfo(3.14));

            Console.WriteLine("\n2. Проверка на null:");
            Console.WriteLine(CheckNull(null));
            Console.WriteLine(CheckNull("не null"));

            Console.WriteLine("\n3. Паттерн свойств:");
            Console.WriteLine(GetPersonCategory(new Person { Age = 10 }));
            Console.WriteLine(GetPersonCategory(new Person { Age = 30 }));
            Console.WriteLine(GetPersonCategory(new Person { Age = 65 }));
            Console.WriteLine(GetPersonCategory(null));

            Console.WriteLine("\n4. Паттерн кортежей (ИСПРАВЛЕНО):");
            Console.WriteLine(GetQuadrantInfo((3, 4)));
            Console.WriteLine(GetQuadrantInfo((-2, -5)));
            Console.WriteLine(GetQuadrantInfo((-1, 2)));
            Console.WriteLine(GetQuadrantInfo((0, 0))); 

            Console.WriteLine("\n5. Позиционный паттерн:");
            Console.WriteLine(GetPointPosition(new Point(0, 0)));
            Console.WriteLine(GetPointPosition(new Point(5, 0)));
            Console.WriteLine(GetPointPosition(new Point(0, 3)));
            Console.WriteLine(GetPointPosition(new Point(2, 2)));
            Console.WriteLine(GetPointPosition(new Point(-3, 4)));
            Console.WriteLine(GetPointPosition(new Point(-1, -1)));
            Console.WriteLine(GetPointPosition(new Point(3, -2)));

            Console.WriteLine("\n6. Паттерн с when:");
            Console.WriteLine(CheckNumber(4));
            Console.WriteLine(CheckNumber(7));
            Console.WriteLine(CheckNumber("текст"));

            Console.WriteLine("\n7. Паттерн с switch expression (заменено на statement):");
            Console.WriteLine(GetTrafficLightAction("Red"));
            Console.WriteLine(GetTrafficLightAction("Yellow"));
            Console.WriteLine(GetTrafficLightAction("Green"));
            Console.WriteLine(GetTrafficLightAction("Blue"));

            Console.WriteLine("\n8. Паттерн с логическими операторами:");
            Console.WriteLine(GetNumberRange(5));
            Console.WriteLine(GetNumberRange(15));
            Console.WriteLine(GetNumberRange(0));
            Console.WriteLine(GetNumberRange(-3));
            Console.WriteLine(GetNumberRange(25));

            Console.WriteLine("\n9.  УДАЛЕНО - РЕКУРСИВНЫЕ ШАБЛОНЫ НЕ ПОДДЕРЖИВАЮТСЯ В C# 8.0");
            //Console.WriteLine(CalculateArea(new Circle { Radius = 3 }));
            //Console.WriteLine(CalculateArea(new Rectangle { Width = 4, Height = 5 }));

            Console.WriteLine("\n10. Паттерн в цикле и коллекциях:");
            object[] items = { 1, "two", 3.0, null, true, new Person() { Name = "Иван", Age = 30 } };
            foreach (var item in items)
            {
                string info;
                switch (item)
                {
                    case string s:
                        info = $"Это строка: {s}";
                        break;
                    case int i:
                        info = $"Это число: {i}";
                        break;
                    case bool b:
                        info = $"Это булево значение: {b}";
                        break;
                    case double d:
                        info = $"Это дробное число: {d}";
                        break;
                    case Person p:
                        info = $"Это человек: {p.Name} ({p.Age} лет)";
                        break;
                    case null:
                        info = "Объект null";
                        break;
                    default:
                        info = $"Неизвестный тип: {item?.GetType().Name}";
                        break;
                }
                Console.WriteLine(info);
            }
        }

        static string GetObjectTypeInfo(object obj)
        {
            switch (obj)
            {
                case string s:
                    return $"Это строка: {s}";
                case int i:
                    return $"Это число: {i}";
                case bool b:
                    return $"Это булево значение: {b}";
                default:
                    return "Неизвестный тип";
            }
        }

        static string CheckNull(object obj)
        {
            switch (obj)
            {
                case null:
                    return "Объект null";
                default:
                    return $"Не null: {obj.GetType().Name}";
            }
        }

        static string GetPersonCategory(Person person)
        {
            switch (person)
            {
                case null:
                    return "Неизвестно";
                case Person p when p.Age < 18:
                    return "Ребенок";
                case Person p when p.Age >= 18 && p.Age < 60:
                    return "Взрослый";
                case Person p when p.Age >= 60:
                    return "Пенсионер";
                default:
                    return "Неизвестно";
            }
        }

        static string GetQuadrantInfo((int x, int y) coords)
        {
            int x = coords.x;
            int y = coords.y;

            if (x > 0 && y > 0)
            {
                return "Обе координаты положительные (Квадрант I)";
            }
            else if (x < 0 && y < 0)
            {
                return "Обе координаты отрицательные (Квадрант III)";
            }
            else if (x < 0 && y > 0)
            {
                return "X отрицательное, Y положительное (Квадрант II)";
            }
            else if (x > 0 && y < 0)
            {
                return "X положительное, Y отрицательное (Квадрант IV)";
            }
            else if (x == 0 && y == 0)
            {
                return "Начало координат";
            }
            else if (x == 0)
            {
                return "На оси Y";
            }
            else
            {
                return "На оси X";
            }
        }


        static string GetPointPosition(Point point)
        {
            switch (point)
            {
                case Point p when p.X == 0 && p.Y == 0:
                    return "Начало координат";
                case Point p when p.Y == 0:
                    return "На оси X";
                case Point p when p.X == 0:
                    return "На оси Y";
                case Point p when p.X > 0 && p.Y > 0:
                    return "В квадранте I";
                case Point p when p.X < 0 && p.Y > 0:
                    return "В квадранте II";
                case Point p when p.X < 0 && p.Y < 0:
                    return "В квадранте III";
                case Point p when p.X > 0 && p.Y < 0:
                    return "В квадранте IV";
                default:
                    return "Неизвестная позиция";
            }
        }

        static string CheckNumber(object obj)
        {
            switch (obj)
            {
                case int i when i % 2 == 0:
                    return $"Четное число: {i}";
                case int i:
                    return $"Нечетное число: {i}";
                default:
                    return "Не число";
            }
        }

        static string GetTrafficLightAction(string color)
        {
            string result;
            switch (color)
            {
                case "Red":
                    result = "Stop";
                    break;
                case "Yellow":
                    result = "Wait";
                    break;
                case "Green":
                    result = "Go";
                    break;
                default:
                    result = "Unknown";
                    break;
            }
            return result;
        }

        static string GetNumberRange(int number)
        {
            switch (number)
            {
                case int n when n >= 1 && n <= 10:
                    return "От 1 до 10";
                case int n when n >= 11 && n <= 20:
                    return "От 11 до 20";
                case int n when n <= 0:
                    return "0 или отрицательное";
                default:
                    return "Больше 20";
            }
        }
    }
}
