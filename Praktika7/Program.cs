using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktika7
{
    class DayGenerator
    {
        delegate string GetNextDayDelegate();

        public static void RunDemo()
        {
            Console.WriteLine("=== Демонстрация генератора дней недели ===");

            string[] daysOfWeek =
            {
                "Понедельник",
                "Вторник",
                "Среда",
                "Четверг",
                "Пятница",
                "Суббота",
                "Воскресенье"
            };

            int currentDayIndex = 0;

            GetNextDayDelegate getNextDay = () =>
            {
                string day = daysOfWeek[currentDayIndex];
                currentDayIndex = (currentDayIndex + 1) % daysOfWeek.Length;
                return day;
            };

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(getNextDay());
            }
        }
    }

    class QuadraticFunction
    {
        public static Func<double, double> Create(double a, double b, double c)
        {
            return x => a * x * x + b * x + c;
        }

        public static void RunDemo()
        {
            Console.WriteLine("\n=== Демонстрация квадратичной функции ===");

            var quadraticFunc = Create(2, 4, 3);
            double result = quadraticFunc(5);

            Console.WriteLine($"Результат для x=5: {result}");
        }
    }

    class EventSender
    {
        public event Action<string> OnTextEvent;
        private readonly string _name;

        public EventSender(string name)
        {
            _name = name;
        }

        public void RaiseEvent()
        {
            OnTextEvent?.Invoke(_name);
        }
    }

    class EventReceiver
    {
        public void DisplayMessage(string senderName)
        {
            Console.WriteLine($"Получено сообщение от: {senderName}");
        }
    }

    class EventSystemDemo
    {
        public static void RunDemo()
        {
            Console.WriteLine("\n=== Демонстрация системы событий ===");

            var sender1 = new EventSender("Саша");
            var sender2 = new EventSender("Петя");
            var receiver = new EventReceiver();

            sender1.OnTextEvent += receiver.DisplayMessage;
            sender2.OnTextEvent += receiver.DisplayMessage;

            sender1.RaiseEvent();
            sender2.RaiseEvent();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
           
            DayGenerator.RunDemo();
            QuadraticFunction.RunDemo();
            EventSystemDemo.RunDemo();
        }
    }
}