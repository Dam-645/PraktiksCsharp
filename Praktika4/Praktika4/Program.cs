using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UnifiedApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Главное меню ===");
                Console.WriteLine("1) Игра Blackjack");
                Console.WriteLine("2) Справочник Person (Student / Employee)");
                Console.WriteLine("3) Выход");
                Console.Write("Выберите номер задачи: ");

                string input = Console.ReadLine();
                if (input == "1")
                {
                    Blackjack.Run();
                }
                else if (input == "2")
                {
                    Directory.Run();
                }
                else if (input == "3")
                {
                    Console.WriteLine("Завершение программы.");
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный выбор. Нажмите Enter и попробуйте снова.");
                    Console.ReadLine();
                }

                Console.WriteLine("\nНажмите Enter для возврата в меню...");
                Console.ReadLine();
            }
        }
    }

    // === Модуль Blackjack ===
    class Blackjack
    {
        static Random random = new Random();

        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("=== Добро пожаловать в Blackjack ===");

            List<int> playerHand = new List<int>();
            List<int> dealerHand = new List<int>();

            playerHand.Add(DrawCard());
            playerHand.Add(DrawCard());
            dealerHand.Add(DrawCard());
            dealerHand.Add(DrawCard());

            while (true)
            {
                Console.WriteLine($"\nВаши карты: {string.Join(", ", playerHand)} (Сумма: {HandTotal(playerHand)})");
                Console.WriteLine($"Открытая карта дилера: {dealerHand[0]}");

                if (HandTotal(playerHand) > 21)
                {
                    Console.WriteLine("Перебор! Вы проиграли.");
                    return;
                }

                Console.Write("Взять карту (h) или остановиться (s)? ");
                string input = Console.ReadLine();

                if (input.ToLower() == "h")
                    playerHand.Add(DrawCard());
                else
                    break;
            }

            Console.WriteLine($"\nКарты дилера: {string.Join(", ", dealerHand)}");

            while (HandTotal(dealerHand) < 17)
            {
                dealerHand.Add(DrawCard());
                Console.WriteLine($"Дилер взял карту: {dealerHand[dealerHand.Count - 1]} (Сумма: {HandTotal(dealerHand)})");
            }

            int playerTotal = HandTotal(playerHand);
            int dealerTotal = HandTotal(dealerHand);

            Console.WriteLine($"\nВаш счёт: {playerTotal}");
            Console.WriteLine($"Счёт дилера: {dealerTotal}");

            if (dealerTotal > 21 || playerTotal > dealerTotal)
                Console.WriteLine("Вы выиграли!");
            else if (dealerTotal == playerTotal)
                Console.WriteLine("Ничья.");
            else
                Console.WriteLine("Вы проиграли.");
        }

        static int DrawCard() => random.Next(2, 12);

        static int HandTotal(List<int> hand)
        {
            int total = 0;
            foreach (var card in hand)
                total += card;
            return total;
        }
    }

    // === Модуль Directory (Person) ===
    static class Directory
    {
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("=== Справочник Person ===");

            Person student = new Student("Алексей", 21, "МГУ", "Программная инженерия");
            Person employee = new Employee("Мария", 30, "Яндекс", "Аналитик данных");

            Console.WriteLine(student.GetInfo());
            Console.WriteLine();
            Console.WriteLine(employee.GetInfo());

}
    }

    abstract class Person
    {
        protected string Name { get; set; }
        protected int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public abstract string GetInfo();
    }

    class Student : Person
    {
        private string University { get; set; }
        private string Major { get; set; }

        public Student(string name, int age, string university, string major)
            : base(name, age)
        {
            University = university;
            Major = major;
        }

        public override string GetInfo()
        {
            return $"[Студент]\nИмя: {Name}\nВозраст: {Age}\nУниверситет: {University}\nСпециальность: {Major}";
        }
    }

    class Employee : Person
    {
        private string Company { get; set; }
        private string Position { get; set; }

        public Employee(string name, int age, string company, string position)
            : base(name, age)
        {
            Company = company;
            Position = position;
        }

        public override string GetInfo()
        {
            return $"[Сотрудник]\nИмя: {Name}\nВозраст: {Age}\nКомпания: {Company}\nДолжность: {Position}";
        }
    }
}

