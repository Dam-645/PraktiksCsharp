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
                Console.WriteLine("3) ToDo List");
                Console.WriteLine("4) Выход");
                Console.Write("Выберите номер задачи: ");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Blackjack.Run();
                        break;
                    case "2":
                        Directory.Run();
                        break;
                    case "3":
                        ToDoList.Run();
                        break;
                    case "4":
                        Console.WriteLine("Завершение программы.");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите Enter и попробуйте снова.");
                        Console.ReadLine();
                        break;
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

    // === Модуль Directory ===
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

    // === Модуль ToDoList ===
    enum TaskStatus { New, InProgress, Completed }

    class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatus Status { get; set; }

        public TaskItem(int id, string title, DateTime dueDate)
        {
            Id = id;
            Title = title;
            DueDate = dueDate;
            Status = TaskStatus.New;
        }

        public void Display()
        {
            Console.WriteLine($"{Id}) {Title} — до {DueDate.ToShortDateString()} — статус: {Status}");
        }
    }

    static class ToDoList
    {
        private static List<TaskItem> tasks = new List<TaskItem>();
        private static int nextId = 1;
        private const int MaxTasks = 100;

        public static void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ToDo List Меню ===");
                Console.WriteLine("1) Показать задачи");
                Console.WriteLine("2) Добавить задачу");
                Console.WriteLine("3) Редактировать задачу");
                Console.WriteLine("4) Удалить задачу");
                Console.WriteLine("0) Назад в меню");
                Console.Write("Ваш выбор: ");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        ShowAll();
                        break;
                    case "2":
                        AddTask();
                        break;
                    case "3":
                        EditTask();
                        break;
                    case "4":
                        DeleteTask();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный ввод.");
                        break;
                }

                Console.WriteLine("\nНажмите Enter для продолжения...");
                Console.ReadLine();
            }
        }

        static void ShowAll()
        {
            Console.WriteLine("\n=== Список задач ===");
            if (tasks.Count == 0)
            {
                Console.WriteLine("Пусто.");
                return;
            }
            foreach (var task in tasks)
                task.Display();
        }

        static void AddTask()
        {
            if (tasks.Count >= MaxTasks)
            {

Console.WriteLine("Достигнут лимит задач.");
                return;
            }

            Console.Write("Введите название задачи: ");
            string title = Console.ReadLine();

            Console.Write("Введите дату (гггг-мм-дд): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dueDate))
            {
                Console.WriteLine("Неверный формат даты.");
                return;
            }

            tasks.Add(new TaskItem(nextId++, title, dueDate));
            Console.WriteLine("Задача добавлена.");
        }

        static void EditTask()
        {
            Console.Write("ID задачи для редактирования: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID.");
                return;
            }

            var task = tasks.Find(t => t.Id == id);
            if (task == null)
            {
                Console.WriteLine("Не найдено.");
                return;
            }

            Console.Write("Новое название (пусто — без изменений): ");
            string newTitle = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newTitle))
                task.Title = newTitle;

            Console.Write("Новая дата (пусто — без изменений): ");
            string dateInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(dateInput) && DateTime.TryParse(dateInput, out DateTime newDate))
                task.DueDate = newDate;

            Console.WriteLine("Выберите статус: 0-New, 1-InProgress, 2-Completed");
            if (Enum.TryParse(Console.ReadLine(), out TaskStatus newStatus))
                task.Status = newStatus;

            Console.WriteLine("Обновлено.");
        }

        static void DeleteTask()
        {
            Console.Write("ID задачи для удаления: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID.");
                return;
            }

            var task = tasks.Find(t => t.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
                Console.WriteLine("Удалено.");
            }
            else
            {
                Console.WriteLine("Не найдено.");
            }
        }
    }
}
