using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics;


namespace Praktika12
{
    public class Person
    {
        public string Name { get; }
        public int Age { get; }

        public Person(string name, int age)
        {
            Name = name ?? "Безымянный";
            Age = age;
        }

        public override string ToString() => $"{Name} ({Age} лет)";
    }

    public class Student : Person
    {
        public string Group { get; }
        public double GPA { get; }

        public Student(string name, int age, string group, double gpa)
            : base(name, age)
        {
            Group = group ?? "Без группы";
            GPA = gpa;
        }

        public Person GetBasePerson() => new Person(Name, Age);
    }

    public class TestCollections
    {
        private readonly List<Person> _persons;
        private readonly List<string> _personStrings;
        private readonly Dictionary<Person, Student> _personStudentDict;
        private readonly Dictionary<string, Student> _stringStudentDict;

        public TestCollections(int count)
        {
            _persons = new List<Person>(count);
            _personStrings = new List<string>(count);
            _personStudentDict = new Dictionary<Person, Student>(count);
            _stringStudentDict = new Dictionary<string, Student>(count);

            for (int i = 0; i < count; i++)
            {
                var student = CreateStudent(i);
                var person = student.GetBasePerson();

                _persons.Add(person);
                _personStrings.Add(person.ToString());
                _personStudentDict.Add(person, student);
                _stringStudentDict.Add(person.ToString(), student);
            }
        }

        private Student CreateStudent(int index)
        {
            return new Student(
                $"Студент {index + 1}",
                18 + index,
                $"Группа {index % 3 + 1}",
                3.0 + index * 0.1
            );
        }

        public void MeasurePerformance()
        {
            if (_persons.Count == 0 || _personStudentDict.Count == 0)
            {
                Console.WriteLine("Коллекции пусты!");
                return;
            }

            var firstPerson = _persons[0];
            var firstStudent = _personStudentDict[firstPerson];
            var firstString = firstPerson.ToString();

            Measure("List<Person>", () => _persons.Contains(firstPerson));
            Measure("List<string>", () => _personStrings.Contains(firstString));
            Measure("Dictionary<Person, Student> (key)", () => _personStudentDict.ContainsKey(firstPerson));
            Measure("Dictionary<string, Student> (key)", () => _stringStudentDict.ContainsKey(firstString));
            Measure("Dictionary<Person, Student> (value)", () => _personStudentDict.ContainsValue(firstStudent));
        }

        private void Measure(string name, Func<bool> action)
        {
            var sw = Stopwatch.StartNew();
            bool result = action();
            sw.Stop();
            Console.WriteLine($"{name}: {sw.ElapsedTicks} тактов, результат: {result}");
        }

        public void PrintCollections()
        {
            Console.WriteLine("\nСодержимое коллекций:");
            Console.WriteLine("Persons:");
            foreach (var p in _persons)
                Console.WriteLine(p);

            Console.WriteLine("\nPerson-Student Dictionary:");
            foreach (var kvp in _personStudentDict)
                Console.WriteLine($"{kvp.Key} -> {kvp.Value}");
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Программа запущена ===");

            try
            {
                var test = new TestCollections(5);
                test.MeasurePerformance();
                test.PrintCollections();

                DemoAdditionalFeatures();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                Debug.WriteLine($"Детали ошибки: {ex}");
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        static void DemoAdditionalFeatures()
        {
            Console.WriteLine("\n=== Дополнительные примеры ===");

            Person person = null;
            Console.WriteLine($"Имя: {person?.Name ?? "Не указано"}");

            var numbers = new ArrayList { 1, 2, 3, null, 5 };
            Console.WriteLine("\nЭлементы коллекции:");
            foreach (var num in numbers)
            {
                Console.WriteLine(num?.ToString() ?? "NULL");
            }
        }
    }
}