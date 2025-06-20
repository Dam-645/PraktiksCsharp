using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktika3 { }
   

public class IntArrayHandler
{
    private int[] _intArray;

    public IntArrayHandler(int size)
    {
        _intArray = new int[size];
    }

    public void InputElements()
    {
        Console.WriteLine("Введите элементы массива:");
        for (int i = 0; i < _intArray.Length; i++)
        {
            Console.Write($"Элемент {i + 1}: ");
            _intArray[i] = Convert.ToInt32(Console.ReadLine());
        }
    }

    public void PrintElements()
    {
        Console.WriteLine("Элементы массива:");
        foreach (var element in _intArray)
        {
            Console.Write($"{element} ");
        }
        Console.WriteLine();
    }

    public void SortArray()
    {
        Array.Sort(_intArray);
    }

    public int Size => _intArray.Length;

    public int Scalar
    {
        set
        {
            for (int i = 0; i < _intArray.Length; i++)
            {
                _intArray[i] *= value;
            }
        }
    }

    public int this[int index]
    {
        get => _intArray[index];
        set => _intArray[index] = value;
    }

    public static IntArrayHandler operator ++(IntArrayHandler arr)
    {
        for (int i = 0; i < arr._intArray.Length; i++)
        {
            arr._intArray[i]++;
        }
        return arr;
    }

    public static IntArrayHandler operator --(IntArrayHandler arr)
    {
        for (int i = 0; i < arr._intArray.Length; i++)
        {
            arr._intArray[i]--;
        }
        return arr;
    }

    public static bool operator !(IntArrayHandler arr)
    {
        for (int i = 1; i < arr._intArray.Length; i++)
        {
            if (arr._intArray[i] < arr._intArray[i - 1])
                return true;
        }
        return false;
    }

    public static IntArrayHandler operator *(IntArrayHandler arr, int scalar)
    {
        IntArrayHandler result = new IntArrayHandler(arr.Size);
        for (int i = 0; i < arr._intArray.Length; i++)
        {
            result._intArray[i] = arr._intArray[i] * scalar;
        }
        return result;
    }

    public static implicit operator int[](IntArrayHandler arr) => arr._intArray;

    public static implicit operator IntArrayHandler(int[] arr)
    {
        IntArrayHandler handler = new IntArrayHandler(arr.Length);
        Array.Copy(arr, handler._intArray, arr.Length);
        return handler;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Работа с массивом целых чисел");
        Console.WriteLine("-----------------------------");

        // Создаем и заполняем массив
        IntArrayHandler arrayHandler = new IntArrayHandler(5);
        arrayHandler.InputElements();

        // Выводим исходный массив
        Console.WriteLine("\nИсходный массив:");
        arrayHandler.PrintElements();

        // Проверяем, не отсортирован ли массив
        Console.WriteLine($"Массив не отсортирован: {!arrayHandler}");

        // Сортируем массив
        arrayHandler.SortArray();
        Console.WriteLine("\nПосле сортировки:");
        arrayHandler.PrintElements();

        // Увеличиваем все элементы на 1
        arrayHandler++;
        Console.WriteLine("\nПосле увеличения всех элементов на 1:");
        arrayHandler.PrintElements();

        // Умножаем все элементы на 2
        arrayHandler.Scalar = 2;
        Console.WriteLine("\nПосле умножения всех элементов на 2:");
        arrayHandler.PrintElements();

        // Преобразуем в обычный массив и выводим
        int[] simpleArray = arrayHandler;
        Console.WriteLine("\nПреобразованный в int[] массив:");
        Console.WriteLine(string.Join(", ", simpleArray));
    }
  }
