using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

class Program 
{
    private const int AsciiCodeThreshold = 70; 

    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("ВЫБЕРИТЕ ЗАДАНИЕ (1-24) ");
            Console.WriteLine("0 - Выход из программы\n ");

            string[] descriptions = {
                "1. Удалить текст в скобках",
                "2. Подсчитать количество заданного слова",
                "3. Заменить пробелы на запятые",
                "4. Процент слов на букву К/к",
                "5. Удалить все цифры из строки",
                "6. Проверить, есть ли символы кроме букв и пробелов",
                "7. Удалить буквы с ASCII-кодами 70-75",
                "8. Проверить, есть ли две '4' подряд",
                "9. Вставить пробел после каждой цифры",
                "10. Увеличить цифры на 1 (9→0)",
                "11. Сначала цифры, потом буквы",
                "12. Преобразовать в верхний регистр",
                "13. Шифр сдвига букв (последняя становится первой)",
                "14. Вывести цифры по 5 в строке",
                "15. Увеличить ASCII-коды символов на 3",
                "16. Сумма цифр кратных 3 в трёх строках",
                "17. Сумма всех цифр в двух строках",
                "18. Количество символов с ASCII >= 70",
                "19. Добавить символы в конец строки",
                "20. Удалить все пробелы из строки",
                "21. Зеркально отразить слово",
                "22. Разбить текст на строки по k символов",
                "23. Вставлять пробелы каждые n символов",
                "24. Заменить слово A1 на слово A2"
            };

            foreach (string desc in descriptions)
            {
                Console.WriteLine(desc);
            }

            Console.Write("\n> Введите номер задания: ");
            string input = Console.ReadLine();

            if (input == "0")
            {
                Console.WriteLine("\nЗавершение работы программы...");
                break;
            }

            if (!int.TryParse(input, out int task) || task < 1 || task > 24)
            {
                Console.WriteLine("\nОшибка! Введите число от 1 до 24");
                WaitForInput();
                continue;
            }

            ExecuteTask(task);
            WaitForInput();
        }
    }

    static void WaitForInput()
    {
        Console.WriteLine("\nНажмите любую клавишу чтобы вернуться в меню...");
        Console.ReadKey();
    }

    static void ExecuteTask(int taskNumber)
    {
        Console.Clear();
        string[] descriptions = {
            "Удаление текста в скобках",
            "Подсчет заданного слова",
            "Замена пробелов на запятые",
            "Процент слов на букву К/к",
            "Удаление всех цифр",
            "Проверка символов (только буквы и пробелы?)",
            "Удаление букв (ASCII 70-75)",
            "Поиск двух '4' подряд",
            "Добавление пробелов после цифр",
            "Инкремент цифр (9→0)",
            "Сортировка: цифры → буквы",
            "Преобразование в верхний регистр",
            "Шифр сдвига букв",
            "Разбиение цифр по 5 в строке",
            "Увеличение ASCII-кодов на 3",
            "Сумма цифр кратных 3 (3 строки)",
            "Сумма всех цифр (2 строки)",
            "Количество символов (ASCII >=70)",
            "Добавление символов в конец",
            "Удаление всех пробелов",
            "Зеркальное отражение слова",
            "Разбиение текста по k символов",
            "Вставка пробелов каждые n символов",
            "Замена слова A1 на A2"
        };


        Console.WriteLine($" ЗАДАНИЕ {taskNumber}: {descriptions[taskNumber - 1]}\n");

        try
        {
            switch (taskNumber)
            {
                case 1: Task1(); break;
                case 2: Task2(); break;
                case 3: Task3(); break;
                case 4: Task4(); break;
                case 5: Task5(); break;
                case 6: Task6(); break;
                case 7: Task7(); break;
                case 8: Task8(); break;
                case 9: Task9(); break;
                case 10: Task10(); break;
                case 11: Task11(); break;
                case 12: Task12(); break;
                case 13: Task13(); break;
                case 14: Task14(); break;
                case 15: Task15(); break;
                case 16: Task16(); break;
                case 17: Task17(); break;
                case 18: Task18(); break;
                case 19: Task19(); break;
                case 20: Task20(); break;
                case 21: Task21(); break;
                case 22: Task22(); break;
                case 23: Task23(); break;
                case 24: Task24(); break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка в задаче {taskNumber}: {ex.Message}");
        }
    }

    static void Task1()
    {
        Console.WriteLine("Введите текст:");
        string text = Console.ReadLine();
        string result = "";
        bool inBrackets = false;

        foreach (char c in text)
        {
            if (c == '(') inBrackets = true;
            else if (c == ')') inBrackets = false;
            else if (!inBrackets) result += c;
        }
        Console.WriteLine("Результат: " + result);
    }

    static void Task2()
    {
        Console.WriteLine("Введите текст:");
        string text = Console.ReadLine();
        Console.WriteLine("Введите слово для поиска:");
        string word = Console.ReadLine();

        string pattern = $@"\b{Regex.Escape(word)}\b"; 
        MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);

        Console.WriteLine($"Слово '{word}' встречается {matches.Count} раз(а)");
    }

    static void Task3()
    {
        Console.WriteLine("Введите текст:");
        string text = Console.ReadLine();
        string result = text.Replace(" ", ", ");
        Console.WriteLine("Результат: " + result);
    }

    static void Task4()
    {
        Console.WriteLine("Введите текст:");
        string text = Console.ReadLine();
        string[] words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        int count = 0;

        foreach (string word in words)
        {
            if (word.Length > 0 && word.ToLower()[0] == 'к')
                count++;
        }

        double percent = words.Length > 0 ? (double)count / words.Length * 100 : 0;
        Console.WriteLine($"Процент слов на 'К/к': {percent:F2}%");
    }

    static void Task5()
    {
        Console.WriteLine("Введите строку:");
        string text = Console.ReadLine();
        string result = new string(text.Where(c => !char.IsDigit(c)).ToArray());
        Console.WriteLine("Результат: " + result);
    }

    static void Task6()
    {
        Console.WriteLine("Введите текст:");
        string text = Console.ReadLine();
        bool hasOther = text.Any(c => !char.IsLetter(c) && c != ' ');
        Console.WriteLine(hasOther ? "Есть другие символы" : "Только буквы и пробелы");
    }

    static void Task7()
    {
        Console.WriteLine("Введите строку:");
        string text = Console.ReadLine();
        string result = new string(text.Where(c => c < AsciiCodeThreshold || c > 75).ToArray());
        Console.WriteLine("Результат: " + result);
    }

    static void Task8()
    {
        Console.WriteLine("Введите строку:");
        string text = Console.ReadLine();
        bool hasTwoFours = text.Contains("44");
        Console.WriteLine(hasTwoFours ? "Есть две 4 подряд" : "Нет двух 4 подряд");
    }

    static void Task9()
    {
        Console.WriteLine("Введите строку:");
        string text = Console.ReadLine();
        StringBuilder result = new StringBuilder();

        foreach (char c in text)
        {
            result.Append(c);
            if (char.IsDigit(c))
                result.Append(' ');
        }

        Console.WriteLine("Результат: " + result);
    }

    static void Task10()
    {
        Console.WriteLine("Введите строку:");
        string text = Console.ReadLine();
        StringBuilder result = new StringBuilder();

        foreach (char c in text)
        {
            if (char.IsDigit(c))
                result.Append(((c - '0' + 1) % 10).ToString());
            else
                result.Append(c);
        }

        Console.WriteLine("Результат: " + result);
    }

    static void Task11()
    {
        Console.WriteLine("Введите строку:");
        string text = Console.ReadLine();
        var digits = new string(text.Where(char.IsDigit).ToArray());
        var letters = new string(text.Where(char.IsLetter).ToArray());
        Console.WriteLine("Результат: " + digits + letters);
    }

    static void Task12()
    {
        Console.WriteLine("Введите строку:");
        string text = Console.ReadLine();
        Console.WriteLine("Результат: " + text.ToUpper());
    }

    static void Task13()
    {
        Console.WriteLine("Введите слово:");
        string word = Console.ReadLine();
        if (string.IsNullOrEmpty(word))
        {
            Console.WriteLine("Пустая строка");
            return;
        }
        Console.WriteLine("Зашифрованное слово: " + word[word.Length - 1] + word.Substring(0, word.Length - 1));
    }

    static void Task14()
    {
        Console.WriteLine("Введите строку цифр:");
        string digits = Console.ReadLine();
        for (int i = 0; i < digits.Length; i += 5)
        {
            int length = Math.Min(5, digits.Length - i);
            Console.WriteLine(digits.Substring(i, length));
        }
    }

    static void Task15()
    {
        Console.WriteLine("Введите строку:");
        string text = Console.ReadLine();
        string result = new string(text.Select(c => (char)(c + 3)).ToArray());
        Console.WriteLine("Результат: " + result);
    }

    static int SumDigitsDivisibleBy3(string s)
    {
        return s.Where(char.IsDigit)
                .Select(c => c - '0')
                .Where(n => n % 3 == 0)
                .Sum();
    }

    static void Task16()
    {
        Console.WriteLine("Введите 3 строки:");
        string[] lines = new string[3];
        for (int i = 0; i < 3; i++)
            lines[i] = Console.ReadLine();

        foreach (string line in lines)
            Console.WriteLine($"Сумма цифр, кратных 3: {SumDigitsDivisibleBy3(line)}");
    }

    static int SumDigits(string s)
    {
        return s.Where(char.IsDigit).Sum(c => c - '0');
    }

    static void Task17()
    {
        Console.WriteLine("Введите 2 строки:");
        string s1 = Console.ReadLine();
        string s2 = Console.ReadLine();
        Console.WriteLine($"Сумма цифр в строке 1: {SumDigits(s1)}");
        Console.WriteLine($"Сумма цифр в строке 2: {SumDigits(s2)}");
    }

    static void Task18()
    {
        Console.WriteLine("Введите строку:");
        string text = Console.ReadLine();
        int count = text.Count(c => c >= AsciiCodeThreshold);
        Console.WriteLine($"Количество символов с ASCII >= {AsciiCodeThreshold}: {count}");
    }

    static void Task19()
    {
        Console.WriteLine("Введите строку:");
        string text = Console.ReadLine();

        Console.WriteLine("Введите символ:");
        string symbolInput = Console.ReadLine();
        if (string.IsNullOrEmpty(symbolInput))
        {
            Console.WriteLine("Ошибка: Символ не введен.");
            return;
        }
        char symbol = symbolInput[0];

        Console.WriteLine("Введите количество:");
        if (!int.TryParse(Console.ReadLine(), out int count) || count < 0)
        {
            Console.WriteLine("Ошибка: Некорректный ввод для количества. Должно быть неотрицательным числом.");
            return;
        }

        Console.WriteLine("Результат: " + text + new string(symbol, count));
    }

    static void Task20()
    {
        Console.WriteLine("Введите строку:");
        string text = Console.ReadLine();
        string result = new string(text.Where(c => c != ' ').ToArray());
        Console.WriteLine("Результат: " + result);
    }

    static void Task21()
    {
        Console.WriteLine("Введите слово:");
        string word = Console.ReadLine();
        Console.WriteLine("Результат: " + new string(word.Reverse().ToArray()));
    }

    static void Task22()
    {
        Console.WriteLine("Введите текст:");
        string text = Console.ReadLine();
        Console.WriteLine("Введите k:");
        if (!int.TryParse(Console.ReadLine(), out int k) || k <= 0)
        {
            Console.WriteLine("Ошибка: Некорректный ввод для k. Должно быть положительным числом.");
            return;
        }

        for (int i = 0; i < text.Length; i += k)
            Console.WriteLine(text.Substring(i, Math.Min(k, text.Length - i)));
    }

    static void Task23()
    {
        Console.WriteLine("Введите строку:");
        string text = Console.ReadLine();
        Console.WriteLine("Введите n (каждые сколько символов вставлять пробел):");
        if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
        {
            Console.WriteLine("Ошибка: Некорректный ввод для n.  Должно быть положительным числом.");
            return;
        }

        StringBuilder result = new StringBuilder();
        for (int i = 0; i < text.Length; i++)
        {
            result.Append(text[i]);
            if ((i + 1) % n == 0 && i != text.Length - 1)
            {
                result.Append(' ');
            }
        }
        Console.WriteLine("Результат: " + result);
    }

    static void Task24()
    {
        Console.WriteLine("Введите текст:");
        string text = Console.ReadLine();
        Console.WriteLine("Введите слово A1 для замены:");
        string a1 = Console.ReadLine();
        Console.WriteLine("Введите слово A2 для замены:");
        string a2 = Console.ReadLine();

        string result = text.Replace(a1, a2);
        Console.WriteLine("Результат: " + result);
    }
}
