namespace ConsoleApp36
{

namespace UnifiedApp
    {
        // Класс "Студент"
        public class Student
        {
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string Patronymic { get; set; }
            public DateTime BirthDate { get; set; }
            public string Address { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public int Year { get; set; }
            public string Group { get; set; }
            public string RecordBookID { get; set; }

            public Student()
            {
                LastName = string.Empty;
                FirstName = string.Empty;
                Patronymic = string.Empty;
                BirthDate = DateTime.MinValue;
                Address = string.Empty;
                PhoneNumber = string.Empty;
                Email = string.Empty;
                Year = 0;
                Group = string.Empty;
                RecordBookID = string.Empty;
            }

            public Student(string lastName, string firstName, string patronymic, DateTime birthDate,
                           string address, string phoneNumber, string email, int year, string group, string recordBookID)
            {
                LastName = lastName;
                FirstName = firstName;
                Patronymic = patronymic;
                BirthDate = birthDate;
                Address = address;
                PhoneNumber = phoneNumber;
                Email = email;
                Year = year;
                Group = group;
                RecordBookID = recordBookID;
            }

            public override string ToString()
            {
                return $"Студент: {LastName} {FirstName} {Patronymic}\n" +
                       $"Дата рождения: {BirthDate.ToShortDateString()}\n" +
                       $"Курс: {Year}\nГруппа: {Group}\nНомер зачетной книжки: {RecordBookID}\n";
            }

            public static bool CompareByYear(Student s1, Student s2) => s1.Year == s2.Year;

            public static bool CompareByLastName(Student s1, Student s2) =>
                string.Equals(s1.LastName, s2.LastName, StringComparison.OrdinalIgnoreCase);
        }

        // Класс "Пациент"
        public class Patient
        {
            private string lastName;
            private string firstName;
            private string patronymic;
            private string address;
            private string cardNumber;
            private string diagnosis;

            public Patient()
            {
                lastName = string.Empty;
                firstName = string.Empty;
                patronymic = string.Empty;
                address = string.Empty;
                cardNumber = string.Empty;
                diagnosis = string.Empty;
            }

            public Patient(string lastName, string firstName, string patronymic, string address, string cardNumber, string diagnosis)
            {
                this.lastName = lastName;
                this.firstName = firstName;
                this.patronymic = patronymic;
                this.address = address;
                this.cardNumber = cardNumber;
                this.diagnosis = diagnosis;
            }

            public string GetCardNumber() => cardNumber;
            public string GetDiagnosis() => diagnosis;

            public void Show()
            {
                Console.WriteLine($"Пациент: {lastName} {firstName} {patronymic}\n" +
                                  $"Адрес: {address}\nНомер карты: {cardNumber}\nДиагноз: {diagnosis}\n");
            }
        }

        // Основной класс с точкой входа
        class Program
        {
            static void Main()
            {
                // Работа со студентами
                Student student1 = new Student("Смирнова", "Екатерина", "Олеговна", new DateTime(2006, 4, 10),
                    "Россия, г. Ярославль, ул. Ленина, д. 8 кв. 34", "+7(4912)456-78-90", "katya.smirnova@example.com",
                    3, "ИС-31", "556001");

                Student student2 = new Student("Орлов", "Дмитрий", "Сергеевич", new DateTime(2005, 11, 3),
                    "Россия, г. Иваново, пр. Мира, д. 12 кв. 9", "+7(4912)123-45-67", "d.orlov@example.com",
                    3, "ИС-22", "556002");

                Console.WriteLine("Информация о студентах:\n");
                Console.WriteLine(student1);

                Console.WriteLine(student2);

                Console.WriteLine("Сравнение по курсу: " + Student.CompareByYear(student1, student2));
                Console.WriteLine("Сравнение по фамилии: " + Student.CompareByLastName(student1, student2));
                Console.WriteLine("\n-----------------------------\n");

                // Работа с пациентами
                Patient[] patients = new Patient[]
                {
                new Patient("Кузнецов", "Алексей", "Игоревич", "ул. Гагарина, д. 5", "A12345", "Ангина"),
                new Patient("Федорова", "Мария", "Александровна", "ул. Победы, д. 15", "B23456", "ОРЗ"),
                new Patient("Волков", "Сергей", "Петрович", "ул. Лесная, д. 3", "C34567", "Ангина"),
                new Patient("Лебедев", "Артем", "Васильевич", "ул. Солнечная, д. 9", "D45678", "Бронхит")
                };

                Console.WriteLine("Пациенты с диагнозом 'Ангина':");
                foreach (var p in patients)
                {
                    if (p.GetDiagnosis() == "Ангина")
                    {
                        p.Show();
                    }
                }

                Console.WriteLine("Введите нижнюю границу интервала номера карты:");
                string minCard = Console.ReadLine();

                Console.WriteLine("Введите верхнюю границу интервала номера карты:");
                string maxCard = Console.ReadLine();

                Console.WriteLine($"Пациенты с номерами карт в интервале {minCard} - {maxCard}:");
                foreach (var p in patients)
                {
                    if (string.Compare(p.GetCardNumber(), minCard) >= 0 &&
                        string.Compare(p.GetCardNumber(), maxCard) <= 0)
                    {
                        p.Show();
                    }
                }

                Console.WriteLine("Новый пациент с пустыми данными:");
                Patient newPatient = new Patient();
                newPatient.Show();
            }
        }
    }
}
