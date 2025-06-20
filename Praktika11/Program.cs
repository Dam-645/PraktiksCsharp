using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Praktika11
{
 
    public class Player
    {
        public string Name { get; set; }
        public int Number { get; set; }
    }

    public class Team
    {
        private readonly Player[] _players = new Player[11];

        public Player this[int index]
        {
            get
            {
                if (index >= 0 && index < _players.Length)
                    return _players[index];
                return null;
            }
            set
            {
                if (index >= 0 && index < _players.Length)
                    _players[index] = value;
            }
        }

        public void ShowTeam()
        {
            Console.WriteLine("Состав команды:");
            for (int i = 0; i < _players.Length; i++)
            {
                if (_players[i] != null)
                    Console.WriteLine($"{_players[i].Number}. {_players[i].Name}");
            }
        }
    }

    public class FootballTeam
    {
        private readonly Player[] _players = new Player[11];

        public Player this[int index]
        {
            get
            {
                if (index < 0 || index >= _players.Length)
                {
                    Console.WriteLine($"Ошибка: Индекс {index} вне диапазона (0-{_players.Length - 1})");
                    return null;
                }
                return _players[index];
            }
            set
            {
                if (index < 0 || index >= _players.Length)
                {
                    Console.WriteLine($"Ошибка: Индекс {index} вне диапазона (0-{_players.Length - 1})");
                    return;
                }
                _players[index] = value;
            }
        }
    }

    public class TranslationDictionary
    {
        private readonly Word[] _words;

        public TranslationDictionary()
        {
            _words = new[]
            {
                new Word("red", "красный"),
                new Word("blue", "синий"),
                new Word("green", "зеленый")
            };
        }

        public string this[string source]
        {
            get
            {
                foreach (var word in _words)
                {
                    if (word.Source == source)
                        return word.Target;
                }
                return null;
            }
            set
            {
                for (int i = 0; i < _words.Length; i++)
                {
                    if (_words[i].Source == source)
                    {
                        _words[i].Target = value;
                        return;
                    }
                }
            }
        }
    }

    public class Word
    {
        public string Source { get; }
        public string Target { get; set; }

        public Word(string source, string target)
        {
            Source = source;
            Target = target;
        }
    }

    public class CyclicArray
    {
        private readonly int[] _array;
        private int _currentIndex;

        public CyclicArray(int[] values)
        {
            _array = values ?? throw new ArgumentNullException(nameof(values));
        }

        public int CurrentValue
        {
            get
            {
                int value = _array[_currentIndex];
                _currentIndex = (_currentIndex + 1) % _array.Length;
                return value;
            }
            set
            {
                _array[_currentIndex] = value;
            }
        }

        public void PrintArray()
        {
            Console.WriteLine($"Массив: [{string.Join(", ", _array)}]");
        }
    }

    public class DigitManager
    {
        private int _number;

        public int this[int digitPosition]
        {
            set
            {
                if (digitPosition < 0)
                {
                    Console.WriteLine("Ошибка: Позиция разряда не может быть отрицательной");
                    return;
                }

                int digit = Math.Abs(value) % 10;
                int power = (int)Math.Pow(10, digitPosition);
                _number = (_number / (power * 10)) * (power * 10) + digit * power + (_number % power);
            }
        }

        public int Number => _number;

        public void PrintNumber()
        {
            Console.WriteLine($"Текущее число: {_number}");
        }
    }

    public class TextArray
    {
        private readonly string[] _texts;

        public TextArray(string[] values)
        {
            _texts = values ?? throw new ArgumentNullException(nameof(values));
        }

        public string this[int index]
        {
            get => GetCyclicValue(_texts, index);
            set => SetCyclicValue(_texts, index, value);
        }

        public char this[int stringIndex, int charIndex]
        {
            get
            {
                string text = GetCyclicValue(_texts, stringIndex);
                if (string.IsNullOrEmpty(text)) return '\0';

                int safeCharIndex = GetCyclicIndex(text.Length, charIndex);
                return text[safeCharIndex];
            }
        }

        public void PrintAll()
        {
            Console.WriteLine("Содержимое массива:");
            for (int i = 0; i < _texts.Length; i++)
            {
                Console.WriteLine($"[{i}]: \"{_texts[i]}\"");
            }
        }

        private static string GetCyclicValue(string[] array, int index)
        {
            if (array.Length == 0) return string.Empty;
            return array[GetCyclicIndex(array.Length, index)];
        }

        private static void SetCyclicValue(string[] array, int index, string value)
        {
            if (array.Length == 0) return;
            array[GetCyclicIndex(array.Length, index)] = value;
        }

        private static int GetCyclicIndex(int length, int index)
        {
            return (index % length + length) % length;
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            DemoTask1();
            DemoTask2();
            DemoTask3();
            DemoTask4();
            DemoTask5();
            DemoTask6();
        }

        static void DemoTask1()
        {
            Console.WriteLine("=== Задача 1: Команда с игроками ===");
            var team = new Team();
            team[0] = new Player { Name = "Иванов", Number = 10 };
            team[1] = new Player { Name = "Петров", Number = 7 };
            team[2] = new Player { Name = "Сидоров", Number = 1 };
            team[15] = new Player { Name = "Ошибочный", Number = 99 };

            team.ShowTeam();

            var player = team[1];
            Console.WriteLine($"\nИгрок под номером 1: {player.Name}");

            Console.WriteLine("\nПопытка получить игрока с индексом 15:");
            Console.WriteLine(team[15] == null ? "Такого игрока нет!" : "Игрок найден");
        }

        static void DemoTask2()
        {
            Console.WriteLine("\n=== Задача 2: Команда с обработкой ошибок ===");
            var inter = new FootballTeam();
            inter[0] = new Player { Name = "Роналду", Number = 7 };
            inter[1] = new Player { Name = "Месси", Number = 10 };
            inter[20] = new Player { Name = "Неймар", Number = 11 };

            var player = inter[1];
            Console.WriteLine($"{player.Number}. {player.Name}");
        }

        static void DemoTask3()
        {
            Console.WriteLine("\n=== Задача 3: Словарь перевода ===");
            var dict = new TranslationDictionary();

            Console.WriteLine("red -> " + dict["red"]);
            Console.WriteLine("blue -> " + dict["blue"]);

            dict["blue"] = "голубой";
            Console.WriteLine("Измененный blue -> " + dict["blue"]);

            Console.WriteLine("Несуществующее слово -> " + (dict["yellow"] ?? "null"));
        }

        static void DemoTask4()
        {
            Console.WriteLine("\n=== Задача 4: Циклический массив ===");
            var cyclic = new CyclicArray(new[] { 1, 2, 3, 4, 5 });

            Console.WriteLine("Чтение значений:");
            for (int i = 0; i < 7; i++)
            {
                Console.Write(cyclic.CurrentValue + " ");
            }

            Console.WriteLine("\n\nИзменение значений:");
            cyclic.CurrentValue = 10;
            cyclic.CurrentValue = 20;
            cyclic.PrintArray();

            Console.WriteLine("\nПоследующие чтения:");
            for (int i = 0; i < 3; i++)
            {
                Console.Write(cyclic.CurrentValue + " ");
            }
            Console.WriteLine();
        }

        static void DemoTask5()
        {
            Console.WriteLine("\n=== Задача 5: Менеджер цифр ===");
            var dm = new DigitManager();

            Console.WriteLine("Начальное состояние:");
            dm.PrintNumber();

            dm[0] = 5;
            dm[1] = 3;
            dm[2] = 7;
            dm[1] = 25;
            dm[0] = -8;

            Console.WriteLine("\nФинальное состояние:");
            dm.PrintNumber();
        }

        static void DemoTask6()
        {
            Console.WriteLine("\n=== Задача 6: Текстовый массив ===");
            var textArray = new TextArray(new[] { "Привет", "Мир", "Программирование", "C#" });

            Console.WriteLine("Одномерный индексатор:");
            Console.WriteLine("textArray[0]: " + textArray[0]);
            Console.WriteLine("textArray[5]: " + textArray[5]);
            textArray[1] = "КОСМОС";
            Console.WriteLine("После изменения textArray[1]: " + textArray[1]);

            Console.WriteLine("\nДвумерный индексатор:");
            Console.WriteLine("textArray[0, 2]: '" + textArray[0, 2] + "'");
            Console.WriteLine("textArray[3, 10]: '" + textArray[3, 10] + "'");
            Console.WriteLine("textArray[5, 2]: '" + textArray[5, 2] + "'");

            Console.WriteLine("\nТекущее состояние массива:");
            textArray.PrintAll();
            Console.ReadLine();
        }
    }
}