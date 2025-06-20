using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Praktika9
{
    public abstract class BaseArray
    {
        protected int[] array;

        public BaseArray(int size)
        {
            if (size <= 0)
                throw new ArgumentException("Размер массива должен быть положительным", nameof(size));

            array = new int[size];
        }

        public int Size => array.Length;

        public abstract void PrintArray();

        public int this[int index]
        {
            get => index >= 0 && index < array.Length
                ? array[index]
                : throw new IndexOutOfRangeException("Индекс выходит за границы массива");
            set
            {
                if (index < 0 || index >= array.Length)
                    throw new IndexOutOfRangeException("Индекс выходит за границы массива");
                array[index] = value;
            }
        }
    }

    public class DerivedArray : BaseArray
    {
        public DerivedArray(int size) : base(size) { }

        public override void PrintArray()
        {
            Console.WriteLine($"Массив ({Size} элементов): {string.Join(", ", array)}");
        }
    }

    public abstract class BaseCalculation
    {
        protected int field1;
        protected int field2;

        protected BaseCalculation(int value1, int value2)
        {
            field1 = value1;
            field2 = value2;
        }

        public abstract int this[int index] { get; set; }
    }

    public interface ICalculatable
    {
        int Calculate(int multiplier);
    }

    public class CalculationClass : BaseCalculation, ICalculatable
    {
        public CalculationClass(int val1, int val2) : base(val1, val2) { }

        public override int this[int index]
        {
            get => index % 2 == 0 ? field1 : field2;
            set
            {
                if (index % 2 == 0)
                    field1 = value;
                else
                    field2 = value;
            }
        }

        public int Calculate(int multiplier) => (field1 + field2) * multiplier;
    }

    public interface IDataContainer1
    {
        string Data { get; set; }
        int this[int index] { get; set; }
        void Display();
    }

    public interface IDataContainer2
    {
        string Data { get; set; }
        int this[int index] { get; set; }
        void Display();
    }

    public abstract class DataContainerBase
    {
        public abstract string Data { get; set; }
        public abstract int this[int index] { get; set; }
        public abstract void Display();
    }

    public class MultiInterfaceClass : DataContainerBase, IDataContainer1, IDataContainer2
    {
        private string _data = "Default";
        private readonly int[] _storage = new int[5];

        public override string Data
        {
            get => _data;
            set => _data = value;
        }

        public override int this[int index]
        {
            get => _storage[index];
            set => _storage[index] = value;
        }

        public override void Display()
        {
            Console.WriteLine($"Основные данные: {_data}, Хранилище: [{string.Join(", ", _storage)}]");
        }

        string IDataContainer1.Data
        {
            get => $"I1_{_data}";
            set => _data = $"I1_{value}";
        }

        int IDataContainer1.this[int index]
        {
            get => _storage[index] + 10;
            set => _storage[index] = value + 10;
        }

        void IDataContainer1.Display()
        {
            Console.WriteLine($"IDataContainer1: {((IDataContainer1)this).Data}");
        }

        string IDataContainer2.Data
        {
            get => $"I2_{_data}";
            set => _data = $"I2_{value}";
        }

        int IDataContainer2.this[int index]
        {
            get => _storage[index] * 2;
            set => _storage[index] = value * 2;
        }

        void IDataContainer2.Display()
        {
            Console.WriteLine($"IDataContainer2: {((IDataContainer2)this).Data}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Задача 1: Работа с массивом ===");
            Task1Demo();

            Console.WriteLine("\n=== Задача 2: Калькуляция ===");
            Task2Demo();

            Console.WriteLine("\n=== Задача 3: Множественные интерфейсы ===");
            Task3Demo();
        }

        static void Task1Demo()
        {
            var arr = new DerivedArray(5);
            for (int i = 0; i < arr.Size; i++)
            {
                arr[i] = i * 2;
            }

            arr.PrintArray();
            Console.WriteLine($"Элемент [2]: {arr[2]}");
            arr[2] = 100;
            Console.WriteLine($"Измененный элемент [2]: {arr[2]}");
            arr.PrintArray();
        }

        static void Task2Demo()
        {
            var calculator = new CalculationClass(10, 20);
            Console.WriteLine($"Исходные значения: [0]={calculator[0]}, [1]={calculator[1]}");

            calculator[0] = 5;
            calculator[1] = 15;
            Console.WriteLine($"Новые значения: [0]={calculator[0]}, [1]={calculator[1]}");

            Console.WriteLine($"Результат расчета (x3): {calculator.Calculate(3)}");
        }

        static void Task3Demo()
        {
            var obj = new MultiInterfaceClass();
            IDataContainer1 i1 = obj;
            IDataContainer2 i2 = obj;

            obj.Data = "Test";
            obj[0] = 5;
            obj[1] = 10;
            Console.WriteLine("\nЧерез базовый класс:");
            obj.Display();
            Console.WriteLine($"Data: {obj.Data}, [0]: {obj[0]}, [1]: {obj[1]}");

            i1.Data = "Interface1";
            i1[0] = 1;
            i1[1] = 2;
            Console.WriteLine("\nЧерез IDataContainer1:");
            i1.Display();
            Console.WriteLine($"Data: {i1.Data}, [0]: {i1[0]}, [1]: {i1[1]}");

            i2.Data = "Interface2";
            i2[0] = 3;
            i2[1] = 4;
            Console.WriteLine("\nЧерез IDataContainer2:");
            i2.Display();
            Console.WriteLine($"Data: {i2.Data}, [0]: {i2[0]}, [1]: {i2[1]}");

            Console.WriteLine("\nФактическое состояние:");
            obj.Display();
            Console.WriteLine($"Data: {obj.Data}, [0]: {obj[0]}, [1]: {obj[1]}");
            Console.ReadLine();
        }
    }
}