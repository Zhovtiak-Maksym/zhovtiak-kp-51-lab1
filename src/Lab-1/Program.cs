namespace Lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; 

            Sorter sorter = new Sorter();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n--- Головне меню ---");
                Console.WriteLine("1. Створити порожню колекцію");
                Console.WriteLine("2. Заповнити контрольними даними");
                Console.WriteLine("3. Вивести колекцію на екран");
                Console.WriteLine("4. Додати книгу");
                Console.WriteLine("5. Видалити книгу за ID");
                Console.WriteLine("6. Відсортувати колекцію");
                Console.WriteLine("7. Показати етапи сортування");
                Console.WriteLine("8. Показати статистику сортування");
                Console.WriteLine("9. Знайти діапазон книг за літерою (спочатку треба відсортувати!)");
                Console.WriteLine("0. Вийти з програми");
                Console.Write("\nОберіть опцію: ");

                string choice = Console.ReadLine();
                Console.WriteLine(); 

                try
                {
                    switch (choice)
                    {
                        case "1":
                            sorter.InitCollection();
                            break;

                        case "2":
                            sorter.GenerateControlData();
                            break;

                        case "3":
                            sorter.PrintCollection();
                            break;

                        case "4":
                            AddManually(sorter);
                            break;

                        case "5":
                            RemoveManually(sorter);
                            break;

                        case "6":
                            sorter.SortCollection();
                            break;

                        case "7":
                            sorter.PrintIntermediateSteps();
                            break;

                        case "8":
                            sorter.PrintStatistics();
                            break;

                        case "9":
                            FindRangeManually(sorter);
                            break;

                        case "0":
                            running = false;
                            Console.WriteLine("Роботу завершено");
                            break;

                        default:
                            Console.WriteLine("Такої опції не існує");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }
            }
        }

        static void AddManually(Sorter sorter)
        {
            try
            {
                Console.Write("Введіть ID книги: ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("Введіть назву книги: ");
                string title = Console.ReadLine();

                Console.Write("Введіть автора: ");
                string author = Console.ReadLine();

                Console.Write("Введіть рік видання: ");
                int year = int.Parse(Console.ReadLine());

                Record newBook = new Record(id, title, author, year);
                sorter.AddRecord(newBook);
            }
            catch (FormatException)
            {
                throw new ArgumentException("Некоректний формат вводу");
            }
        }

        static void RemoveManually(Sorter sorter)
        {
            try
            {
                Console.Write("Введіть ID книги для видалення: ");
                int id = int.Parse(Console.ReadLine());

                sorter.RemoveRecord(id);
            }
            catch (FormatException)
            {
                throw new ArgumentException("ID має бути числом");
            }
        }

        static void FindRangeManually(Sorter sorter)
        {
            Console.Write("Введіть початкову літеру для пошуку: ");
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Ввід не може бути порожнім");
            }

            char letter = input[0];
            sorter.FindRangeByLetter(letter);
        }
    }
}