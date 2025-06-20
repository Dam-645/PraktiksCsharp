using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace praktika14
{
    using System;
    using System.Globalization;
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            RunMenu();
        }

        static void RunMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Работа с датами");
                Console.WriteLine("2. Дополнительные функции");
                Console.WriteLine("0. Выход");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        RunDateTasks();
                        break;
                    case "2":
                        RunAdditionalTasks();
                        break;
                    case "0":
                        Console.WriteLine("Завершение работы...");
                        return;
                    default:
                        Console.WriteLine("Некорректный ввод. Повторите.");
                        WaitForAnyKey();
                        break;
                }
            }
        }

        static void RunDateTasks()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите задание:");
                Console.WriteLine("1. Разница между датами");
                Console.WriteLine("2. День недели");
                Console.WriteLine("3. Добавить дни к дате");
                Console.WriteLine("4. Проверка високосного года");
                Console.WriteLine("5. Форматирование даты");
                Console.WriteLine("6. Назад");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        DateDifference();
                        break;
                    case "2":
                        DayOfWeek();
                        break;
                    case "3":
                        AddDaysToDate();
                        break;
                    case "4":
                        LeapYearCheck();
                        break;
                    case "5":
                        DateFormatting();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Некорректный ввод. Повторите.");
                        WaitForAnyKey();
                        break;
                }
                WaitForAnyKey();
            }
        }

        static void RunAdditionalTasks()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите задание:");
                Console.WriteLine("7. Дней до Нового года");
                Console.WriteLine("8. Возраст человека");
                Console.WriteLine("9. Валидация даты");
                Console.WriteLine("10. Часовые пояса");
                Console.WriteLine("11. Таймер обратного отсчёта");
                Console.WriteLine("12. Назад");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "7":
                        DaysUntilNewYear();
                        break;
                    case "8":
                        CalculateAge();
                        break;
                    case "9":
                        DateValidation();
                        break;
                    case "10":
                        TimeZoneConversion();
                        break;
                    case "11":
                        CountdownTimer();
                        break;
                    case "12":
                        return;
                    default:
                        Console.WriteLine("Некорректный ввод. Повторите.");
                        WaitForAnyKey();
                        break;
                }
                WaitForAnyKey();
            }
        }

        static void DateDifference()
        {
            DateTime date1 = ReadDate("Введите первую дату (dd.MM.yyyy HH:mm):");
            DateTime date2 = ReadDate("Введите вторую дату (dd.MM.yyyy HH:mm):");

            TimeSpan difference = date1 > date2 ? date1 - date2 : date2 - date1;

            Console.WriteLine($"Разница: {difference.Days} дн., {difference.Hours} ч., {difference.Minutes} мин.");
        }

        static void DayOfWeek()
        {
            DateTime date = ReadDate("Введите дату (dd.MM.yyyy):", "dd.MM.yyyy");

            Console.WriteLine($"Это {date.ToString("dddd", new CultureInfo("ru-RU"))}");
        }

        static void AddDaysToDate()
        {
            DateTime date = ReadDate("Введите дату (dd.MM.yyyy):", "dd.MM.yyyy");

            Console.WriteLine("Введите количество дней для добавления:");
            int days = int.Parse(Console.ReadLine());

            DateTime newDate = date.AddDays(days);
            Console.WriteLine($"Новая дата: {newDate:dd.MM.yyyy}");
        }

        static void LeapYearCheck()
        {
            Console.WriteLine("Введите год:");
            int year = int.Parse(Console.ReadLine());

            bool isLeap = DateTime.IsLeapYear(year);
            Console.WriteLine(isLeap ? "Високосный год" : "Не високосный год");
        }

        static void DateFormatting()
        {
            DateTime date = ReadDate("Введите дату и время (dd.MM.yyyy HH:mm):", "dd.MM.yyyy HH:mm");

            Console.WriteLine($"Форматы:\n" +
                $"1. {date:dd.MM.yyyy}\n" +
                $"2. {date:MM/dd/yyyy}\n" +
                $"3. {date:yyyy-MM-dd HH:mm:ss}\n" +
                $"4. {date:dddd, dd MMMM yyyy года}");
        }

        static void DaysUntilNewYear()
        {
            DateTime today = DateTime.Today;
            DateTime newYear = new DateTime(today.Year + 1, 1, 1);

            TimeSpan remaining = newYear - today;
            Console.WriteLine($"До Нового года осталось {remaining.Days} дней");
        }

        static void CalculateAge()
        {
            DateTime birthDate = ReadDate("Введите дату рождения (dd.MM.yyyy):", "dd.MM.yyyy");
            DateTime today = DateTime.Today;

            int years = today.Year - birthDate.Year;
            int months = today.Month - birthDate.Month;
            int days = today.Day - birthDate.Day;

            if (days < 0)
            {
                months--;
                days += DateTime.DaysInMonth(today.Year, today.Month);
            }
            if (months < 0)
            {
                years--;
                months += 12;
            }

            Console.WriteLine($"Возраст: {years} лет, {months} месяцев, {days} дней");
        }

        static void DateValidation()
        {
            Console.WriteLine("Введите дату (dd.MM.yyyy):");
            string input = Console.ReadLine();

            if (DateTime.TryParseExact(input, "dd.MM.yyyy", null, DateTimeStyles.None, out _))
            {
                Console.WriteLine("Дата корректна");
            }
            else
            {
                Console.WriteLine("Дата некорректна");
            }
        }


        static void TimeZoneConversion()
        {
            DateTime utcNow = DateTime.UtcNow;
            DateTime localNow = DateTime.Now;

            Console.WriteLine($"UTC время: {utcNow:HH:mm:ss}");
            Console.WriteLine($"Локальное время: {localNow:HH:mm:ss}");

            Console.WriteLine("\nКонвертация:");
            Console.WriteLine("1. UTC -> Локальное");
            Console.WriteLine("2. Локальное -> UTC");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                DateTime utcTime = ReadDate("Введите UTC время (HH:mm:ss):", "HH:mm:ss");
                Console.WriteLine($"Локальное время: {utcTime.ToLocalTime():HH:mm:ss}");
            }
            else
            {
                DateTime localTime = ReadDate("Введите локальное время (HH:mm:ss):", "HH:mm:ss");
                Console.WriteLine($"UTC время: {localTime.ToUniversalTime():HH:mm:ss}");
            }
        }


        static void CountdownTimer()
        {
            Console.WriteLine("Введите количество секунд:");
            int seconds = int.Parse(Console.ReadLine());

            for (int i = seconds; i >= 0; i--)
            {
                Console.Clear();
                Console.WriteLine($"Осталось: {i} сек.");
                if (i > 0) Thread.Sleep(1000);
            }
            Console.WriteLine("Время вышло!");
        }

        static DateTime ReadDate(string prompt, string format = "dd.MM.yyyy HH:mm")
        {
            DateTime date;
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();

                if (DateTime.TryParseExact(input, format, null, DateTimeStyles.None, out date))
                {
                    return date;
                }
                else
                {
                    Console.WriteLine("Некорректный формат даты. Попробуйте снова.");
                }
            }
        }

        static void WaitForAnyKey()
        {
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}