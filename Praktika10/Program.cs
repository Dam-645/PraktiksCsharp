using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktika10
{
 
    public class State
    {
        public decimal Population { get; set; }
        public decimal Area { get; set; }

        public static State operator +(State s1, State s2)
        {
            return new State
            {
                Population = s1.Population + s2.Population,
                Area = s1.Area + s2.Area
            };
        }

        public static bool operator >(State s1, State s2)
        {
            return s1.Population > s2.Population;
        }

        public static bool operator <(State s1, State s2)
        {
            return s1.Population < s2.Population;
        }

        public static bool operator >=(State s1, State s2)
        {
            return s1.Population >= s2.Population;
        }

        public static bool operator <=(State s1, State s2)
        {
            return s1.Population <= s2.Population;
        }
    }

    // Задача 2: Перегрузка оператора + между разными классами
    public class Bread
    {
        public int Weight { get; set; }

        public static Sandwich operator +(Bread bread, Butter butter)
        {
            return new Sandwich
            {
                Weight = bread.Weight + butter.Weight
            };
        }
    }

    public class Butter
    {
        public int Weight { get; set; }
    }

    public class Sandwich
    {
        public int Weight { get; set; }
    }

    // Задача 3: Множественное наследование интерфейсов
    public interface IFirst
    {
        string Property { get; set; }
        int this[int index] { get; set; }
        void Method();
    }

    public interface ISecond
    {
        string Property { get; set; }
        int this[int index] { get; set; }
        void Method();
    }

    public abstract class AbstractBase
    {
        public abstract string Property { get; set; }
        public abstract int this[int index] { get; set; }
        public abstract void Method();
    }

    public class MultiInterfaceClass : AbstractBase, IFirst, ISecond
    {
        private string _property = "Default";
        private readonly int[] _data = new int[10];

        // Реализация IFirst
        string IFirst.Property
        {
            get { return _property + " (First)"; }
            set { _property = value; }
        }

        int IFirst.this[int index]
        {
            get { return _data[index]; }
            set { _data[index] = value; }
        }

        void IFirst.Method()
        {
            Console.WriteLine($"IFirst Method: {((IFirst)this).Property}");
        }

        // Реализация ISecond
        string ISecond.Property
        {
            get { return _property + " (Second)"; }
            set { _property = value; }
        }

        int ISecond.this[int index]
        {
            get { return _data[index]; }
            set { _data[index] = value; }
        }

        void ISecond.Method()
        {
            Console.WriteLine($"ISecond Method: {((ISecond)this).Property}");
        }

        // Реализация AbstractBase
        public override string Property
        {
            get { return _property; }
            set { _property = value; }
        }

        public override int this[int index]
        {
            get { return _data[index]; }
            set { _data[index] = value; }
        }

        public override void Method()
        {
            Console.WriteLine($"Base Method: {Property}");
            ((IFirst)this).Method();
            ((ISecond)this).Method();
        }
    }

    // Задача 4: Перегрузка логических операторов
    public class NumberClass
    {
        public int Value { get; set; }

        public NumberClass(int value)
        {
            Value = value;
        }

        public static NumberClass operator &(NumberClass a, NumberClass b)
        {
            return new NumberClass(a.Value & b.Value);
        }

        public static NumberClass operator |(NumberClass a, NumberClass b)
        {
            return new NumberClass(a.Value | b.Value);
        }

        public static bool operator true(NumberClass obj)
        {
            return obj.Value == 2 || obj.Value == 3 || obj.Value == 5 || obj.Value == 7;
        }

        public static bool operator false(NumberClass obj)
        {
            return obj.Value < 1 || obj.Value > 10;
        }
    }

    // Задача 5: Преобразование Clock <-> int
    public class Clock
    {
        public int Hours { get; set; }

        public static implicit operator Clock(int value)
        {
            return new Clock { Hours = value % 24 };
        }

        public static explicit operator int(Clock clock)
        {
            return clock.Hours;
        }
    }

    // Задача 6: Преобразование температур
    public class Celcius
    {
        public double Gradus { get; set; }

        public static explicit operator Celcius(Fahrenheit f)
        {
            return new Celcius
            {
                Gradus = 5.0 / 9 * (f.Gradus - 32)
            };
        }
    }

    public class Fahrenheit
    {
        public double Gradus { get; set; }

        public static explicit operator Fahrenheit(Celcius c)
        {
            return new Fahrenheit
            {
                Gradus = 9.0 / 5 * c.Gradus + 32
            };
        }
    }

    // Задача 7: Преобразование валют
    public class Dollar
    {
        public decimal Sum { get; set; }

        public static implicit operator Euro(Dollar dollar)
        {
            return new Euro
            {
                Sum = dollar.Sum / 1.14m
            };
        }
    }

    public class Euro
    {
        public decimal Sum { get; set; }

        public static explicit operator Dollar(Euro euro)
        {
            return new Dollar
            {
                Sum = euro.Sum * 1.14m
            };
        }
    }

    // Задача 8: Преобразование TextHolder
    public class TextHolder
    {
        public string Text { get; set; } = "";

        public static explicit operator int(TextHolder holder)
        {
            return holder.Text.Length;
        }

        public static explicit operator char(TextHolder holder)
        {
            return holder.Text.Length > 0 ? holder.Text[0] : '\0';
        }

        public static implicit operator TextHolder(int length)
        {
            return new TextHolder
            {
                Text = new string('A', length)
            };
        }
    }

    class Program
    {
        static void Main()
        {
            // Демонстрация всех задач
            DemoTask1();
            DemoTask2();
            DemoTask3();
            DemoTask4();
            DemoTask5();
            DemoTask6();
            DemoTask7();
            DemoTask8();
        }

        static void DemoTask1()
        {
            Console.WriteLine("=== Задача 1: Операторы для State ===");
            var state1 = new State { Population = 10000000, Area = 100000 };
            var state2 = new State { Population = 15000000, Area = 120000 };

            var state3 = state1 + state2;
            Console.WriteLine($"Объединенное государство: Население = {state3.Population}, Площадь = {state3.Area}");
            Console.WriteLine($"state1 > state2: {state1 > state2}");
            Console.WriteLine($"state1 < state2: {state1 < state2}");
        }

        static void DemoTask2()
        {
            Console.WriteLine("\n=== Задача 2: Бутерброд ===");
            var bread = new Bread { Weight = 80 };
            var butter = new Butter { Weight = 20 };
            var sandwich = bread + butter;
            Console.WriteLine($"Бутерброд весом: {sandwich.Weight}г");
        }

        static void DemoTask3()
        {
            Console.WriteLine("\n=== Задача 3: Множественные интерфейсы ===");
            var obj = new MultiInterfaceClass();

            obj.Property = "Test";
            obj[0] = 10;
            obj.Method();

            IFirst first = obj;
            first.Property = "First";
            first.Method();

            ISecond second = obj;
            second.Property = "Second";
            second.Method();
        }

        static void DemoTask4()
        {
            Console.WriteLine("\n=== Задача 4: Логические операторы ===");
            var num1 = new NumberClass(3);
            var num2 = new NumberClass(8);

            if (num1) Console.WriteLine("num1 истинный");
            if (num2) Console.WriteLine("num2 ложный");
        }

        static void DemoTask5()
        {
            Console.WriteLine("\n=== Задача 5: Часы ===");
            Clock clock = 34;
            int hours = (int)clock;
            Console.WriteLine($"Часы: {clock.Hours}, Значение: {hours}");
        }

        static void DemoTask6()
        {
            Console.WriteLine("\n=== Задача 6: Температура ===");
            var c = new Celcius { Gradus = 25 };
            var f = (Fahrenheit)c;
            Console.WriteLine($"{c.Gradus}°C = {f.Gradus}°F");

            var f2 = new Fahrenheit { Gradus = 100 };
            var c2 = (Celcius)f2;
            Console.WriteLine($"{f2.Gradus}°F = {c2.Gradus}°C");
        }

        static void DemoTask7()
        {
            Console.WriteLine("\n=== Задача 7: Валюта ===");
            var usd = new Dollar { Sum = 100 };
            Euro eur = usd;
            Console.WriteLine($"{usd.Sum} USD = {eur.Sum} EUR");

            var eur2 = new Euro { Sum = 100 };
            var usd2 = (Dollar)eur2;
            Console.WriteLine($"{eur2.Sum} EUR = {usd2.Sum} USD");
        }

        static void DemoTask8()
        {
            Console.WriteLine("\n=== Задача 8: Текст ===");
            var textObj = new TextHolder { Text = "Hello" };
            Console.WriteLine($"Длина: {(int)textObj}, Первый символ: {(char)textObj}");

            TextHolder aString = 5;
            Console.WriteLine($"Строка из 'A': {aString.Text}");
            Console.ReadLine();
        }
    }
}