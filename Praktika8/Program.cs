using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AnimalZooDemo
{
    public class Animal
    {
        public string Name { get; set; }

        public Animal(string name)
        {
            Name = name;
        }

        public virtual void MakeSound()
        {
            Console.WriteLine("Some animal sound");
        }
    }

    public class Dog : Animal
    {
        public Dog(string name) : base(name) { }

        public override void MakeSound()
        {
            Console.WriteLine($"{Name} the dog says: Woof!");
        }

        public void Fetch()
        {
            Console.WriteLine($"{Name} is fetching the stick");
        }
    }

    public class Cat : Animal
    {
        public Cat(string name) : base(name) { }

        public override void MakeSound()
        {
            Console.WriteLine($"{Name} the cat says: Meow!");
        }

        public void Purr()
        {
            Console.WriteLine($"{Name} is purring");
        }
    }

    public class Zoo
    {
        private List<Animal> _animals = new List<Animal>();

        public void AddAnimal<T>(T animal, Action<T> addAction) where T : Animal
        {
            addAction(animal);
            _animals.Add(animal);
            Console.WriteLine($"Added {animal.GetType().Name}: {animal.Name}");
        }

        public T GetAnimal<T>(Func<T> getFunc) where T : Animal
        {
            T animal = getFunc();
            _animals.Add(animal);
            Console.WriteLine($"Got {animal.GetType().Name}: {animal.Name}");
            return animal;
        }

        public void ProcessAnimal<T>(T animal, Func<T, Animal> processFunc) where T : Animal
        {
            Animal processed = processFunc(animal);
            Console.WriteLine($"Processed {animal.GetType().Name} as {processed.GetType().Name}: {processed.Name}");
        }

        public void ListAnimals()
        {
            Console.WriteLine("\nAnimals in zoo:");
            foreach (var animal in _animals)
            {
                Console.WriteLine($"{animal.GetType().Name}: {animal.Name}");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Демонстрация ковариантности и контравариантности ===");

            Zoo zoo = new Zoo();

            Console.WriteLine("\n[Ковариантность]");
            Func<Animal> createAnimal = () => new Animal("Generic Animal");
            Func<Dog> createDog = () => new Dog("Rex");
            Func<Cat> createCat = () => new Cat("Mittens");

            Func<Animal> animalCreator = createDog;
            Animal dogAsAnimal = animalCreator();
            dogAsAnimal.MakeSound();

            zoo.GetAnimal(createDog);
            zoo.GetAnimal(createCat);

            Console.WriteLine("\n[Контравариантность]");
            Action<Animal> logAnimal = (animal) =>
                Console.WriteLine($"Logging {animal.GetType().Name}: {animal.Name}");

            Action<Dog> logDog = logAnimal;
            Action<Cat> logCat = logAnimal;

            zoo.AddAnimal(new Dog("Buddy"), logDog);
            zoo.AddAnimal(new Cat("Whiskers"), logCat);

            Console.WriteLine("\n[Комбинированные примеры]");
            Func<Dog, Animal> processDog = (dog) =>
            {
                dog.Fetch();
                return dog;
            };

            Func<Cat, Animal> processCat = (cat) =>
            {
                cat.Purr();
                return cat;
            };

            zoo.ProcessAnimal(new Dog("Max"), processDog);
            zoo.ProcessAnimal(new Cat("Luna"), processCat);

 
            zoo.ListAnimals();

            Console.WriteLine("\n[Дополнительные примеры]");
            Action<Animal> complexAction = (animal) =>
            {
                animal.MakeSound();
                if (animal is Dog d) d.Fetch();
                else if (animal is Cat c) c.Purr();
            };

            complexAction(new Dog("Bobby"));
            complexAction(new Cat("Kitty"));
            Console.ReadLine();
        }
    }
}