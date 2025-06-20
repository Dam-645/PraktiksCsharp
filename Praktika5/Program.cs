using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GenericClass<T>
{
    private T _value;

    public T Value
    {
        get { return _value; }
        set { _value = value; }
    }
}


public class ArrayOperations
{
    public static T FindMax<T>(T[] array) where T : IComparable<T>
    {
        if (array == null || array.Length == 0)
            throw new ArgumentException("Массив не может быть пустым");

        T max = array[0];
        foreach (var item in array)
        {
            if (item.CompareTo(max) > 0)
                max = item;
        }
        return max;
    }
}

public class GenericArray<T>
{
    private readonly T[] _array;

    public GenericArray(T[] array)
    {
        _array = array ?? throw new ArgumentNullException(nameof(array));
    }

    public static GenericArray<T> operator +(GenericArray<T> left, GenericArray<T> right)
    {
        var newArray = new T[left._array.Length + right._array.Length];
        Array.Copy(left._array, newArray, left._array.Length);
        Array.Copy(right._array, 0, newArray, left._array.Length, right._array.Length);
        return new GenericArray<T>(newArray);
    }

    public void Print()
    {
        Console.WriteLine($"Array: [{string.Join(", ", _array)}]");
    }
}

public class GenericStorage<T>
{
    private T[] _items = new T[0];

    public void Add(T item)
    {
        Array.Resize(ref _items, _items.Length + 1);
        _items[_items.Length - 1] = item; 
    }

    public bool Remove(T item)
    {
        int index = Array.IndexOf(_items, item);
        if (index < 0) return false;

        var newArray = new T[_items.Length - 1];
        Array.Copy(_items, 0, newArray, 0, index);
        Array.Copy(_items, index + 1, newArray, index, _items.Length - index - 1);
        _items = newArray;
        return true;
    }

    public T Get(int index)
    {
        return _items[index];
    }

    public int Count
    {
        get { return _items.Length; }
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Демонстрация задачи 1 ===");
        DemoTask1();
        Console.WriteLine("\n=== Демонстрация задачи 2 ===");
        DemoTask2();
        Console.WriteLine("\n=== Демонстрация задачи 3 ===");
        DemoTask3();
        Console.WriteLine("\n=== Демонстрация задачи 4 ===");
        DemoTask4();
    }

    private static void DemoTask1()
    {
        var intContainer = new GenericClass<int>();
        intContainer.Value = 42;
        Console.WriteLine($"Int value: {intContainer.Value}");

        var stringContainer = new GenericClass<string>();
        stringContainer.Value = "Hello, Generics!";
        Console.WriteLine($"String value: {stringContainer.Value}");

        var doubleContainer = new GenericClass<double>();
        doubleContainer.Value = 3.14159;
        Console.WriteLine($"Double value: {doubleContainer.Value}");
    }

    private static void DemoTask2()
    {
        int[] intArray = { 3, 7, 2, 9, 5 };
        Console.WriteLine($"Max int: {ArrayOperations.FindMax(intArray)}");

        double[] doubleArray = { 1.5, 2.7, 3.1, 0.9 };
        Console.WriteLine($"Max double: {ArrayOperations.FindMax(doubleArray)}");

        string[] stringArray = { "a", "b", "c", "d" };
        Console.WriteLine($"Max string: {ArrayOperations.FindMax(stringArray)}");
    }

    private static void DemoTask3()
    {
        var intArr1 = new GenericArray<int>(new[] { 1, 2, 3 });
        var intArr2 = new GenericArray<int>(new[] { 4, 5, 6 });
        var intResult = intArr1 + intArr2;
        intResult.Print();

        var strArr1 = new GenericArray<string>(new[] { "a", "b" });
        var strArr2 = new GenericArray<string>(new[] { "c", "d", "e" });
        var strResult = strArr1 + strArr2;
        strResult.Print();
    }

    private static void DemoTask4()
    {
        var intStorage = new GenericStorage<int>();
        intStorage.Add(10);
        intStorage.Add(20);
        intStorage.Add(30);
        Console.WriteLine($"Items count: {intStorage.Count}");
        Console.WriteLine($"Item at index 1: {intStorage.Get(1)}");

        Console.WriteLine($"Remove 20: {intStorage.Remove(20)}");
        Console.WriteLine($"Items count after removal: {intStorage.Count}");

        var stringStorage = new GenericStorage<string>();
        stringStorage.Add("first");
        stringStorage.Add("second");
        Console.WriteLine($"First item: {stringStorage.Get(0)}");
        Console.ReadLine();
    }
}
