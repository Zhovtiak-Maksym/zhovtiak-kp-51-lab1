using System.Diagnostics; 

namespace Lab_1
{
    public class Sorter
    {
        private List<Record> books;
        private List<string> stepsOfSorting = new List<string>();
        private SortStatistics stats;

        public void InitCollection()
        {
            books = new List<Record>();
        }

        public void AddRecord(Record record)
        {
            if (books == null)
            {
                throw new InvalidOperationException("Колекцію не ініціалізовано");
            }

            if (record == null)
            {
                throw new ArgumentNullException(nameof(record), "Не можна додавати книжку при порожньому записі");
            }

            books.Add(record);
        }

        public void RemoveRecord(int bookId)
        {
            if (books == null)
            {
                throw new InvalidOperationException("Колекцію не ініціалізовано");
            }

            Record toRemove = books.Find(b => b.BookId == bookId);

            if (toRemove == null)
            {
                throw new ArgumentException($"Книгу з ID: {bookId} не знайдено в колекції");
            }

            books.Remove(toRemove);
        }

        public void PrintCollection()
        {
            if (books == null)
            {
                throw new InvalidOperationException("Колекцію не ініціалізовано");
            }

            if (books.Count == 0)
            {
                Console.WriteLine("Книжок ще немає");
                return;
            }

            foreach (Record book in books)
            {
                Console.WriteLine($"ID: {book.BookId}");
                Console.WriteLine($"Назва: {book.Title}");
                Console.WriteLine($"Автор: {book.Author}");
                Console.WriteLine($"Рік видання: {book.Year}");
                Console.WriteLine("-------------------------");
            }
        }

        public void GenerateControlData()
        {
            InitCollection();

            books.Add(new Record(1, "Don Quixote", "Miguel de Cervantes", 1605));
            books.Add(new Record(2, "A Tale of Two Cities", "Charles Dickens", 1859));
            books.Add(new Record(3, "The Lord of the Rings", "J.R.R. Tolkien", 1954));
            books.Add(new Record(4, "The Little Prince", "Antoine de Saint-Exupery", 1943));
            books.Add(new Record(5, "Harry Potter and the Philosopher's Stone", "J.K. Rowling", 1997));
            books.Add(new Record(6, "And Then There Were None", "Agatha Christie", 1939));
            books.Add(new Record(7, "Dream of the Red Chamber", "Cao Xueqin", 1791));
            books.Add(new Record(8, "The Hobbit", "J.R.R. Tolkien", 1937));
            books.Add(new Record(9, "The Lion, the Witch and the Wardrobe", "C.S. Lewis", 1950));
            books.Add(new Record(10, "The Da Vinci Code", "Dan Brown", 2003));
            books.Add(new Record(11, "The Catcher in the Rye", "J.D. Salinger", 1951));
            books.Add(new Record(12, "The Alchemist", "Paulo Coelho", 1988));
            books.Add(new Record(13, "To Kill a Mockingbird", "Harper Lee", 1960));
            books.Add(new Record(14, "Frankenstein", "Mary Shelley", 1831));
            books.Add(new Record(15, "Frankenstein", "Mary Shelley", 1818));
        }

        public void SortCollection()
        {
            if (books == null)
            {
                throw new InvalidOperationException("Колекцію не ініціалізовано");
            }

            if (books.Count == 0)
            {
                throw new InvalidOperationException("Ще немає книжок");
            }

            stats = new SortStatistics();
            stepsOfSorting.Clear();

            Stopwatch watch = Stopwatch.StartNew();

            MergeSort(books, 0, books.Count - 1);

            watch.Stop();
            stats.ExecutionTime = watch.Elapsed;
        }

        private void MergeSort(List<Record> array, int left, int right)
        {
            stats.RecursiveCalls++;

            if (left < right)
            {
                int mid = left + (right - left) / 2;

                MergeSort(array, left, mid);
                MergeSort(array, mid + 1, right);

                Merge(array, left, mid, right);
            }
        }

        private void Merge(List<Record> array, int left, int mid, int right)
        {
            stats.Passes++;

            int n1 = mid - left + 1;
            int n2 = right - mid;

            List<Record> leftArray = new List<Record>(n1);
            List<Record> rightArray = new List<Record>(n2);

            for (int i = 0; i < n1; i++)
            {
                leftArray.Add(array[left + i]);
                stats.SwapsOrCopies++;
            }

            for (int j = 0; j < n2; j++)
            {
                rightArray.Add(array[mid + 1 + j]);
                stats.SwapsOrCopies++;
            }

            int k = left;
            int iIdx = 0, jIdx = 0;

            while (iIdx < n1 && jIdx < n2)
            {
                stats.Comparisons++;

                if (CompareBooks(leftArray[iIdx], rightArray[jIdx]) <= 0)
                {
                    array[k] = leftArray[iIdx];
                    iIdx++;
                }
                else
                {
                    array[k] = rightArray[jIdx];
                    jIdx++;
                }
                stats.SwapsOrCopies++;
                k++;
            }

            while (iIdx < n1)
            {
                array[k] = leftArray[iIdx];
                iIdx++;
                k++;
                stats.SwapsOrCopies++;
            }

            while (jIdx < n2)
            {
                array[k] = rightArray[jIdx];
                jIdx++;
                k++;
                stats.SwapsOrCopies++;
            }

            string step = $"Злиття - [індекси {left}-{right}]: ";

            for (int m = left; m <= right; m++)
            {
                step += $"\"{array[m].Title}\" ({array[m].Year}) | ";
            }

            stepsOfSorting.Add(step);
        }

        private int CompareBooks(Record a, Record b)
        {
            int titleComparison = string.Compare(a.Title, b.Title, StringComparison.OrdinalIgnoreCase);

            if (titleComparison != 0)
            {
                return titleComparison;
            }

            return a.Year.CompareTo(b.Year);
        }

        public void PrintIntermediateSteps()
        {
            if (stepsOfSorting == null || stepsOfSorting.Count == 0)
            {
                throw new InvalidOperationException("Ще не відбулося сортування");
            }

            Console.WriteLine("\nЕтапи злиття підмасивів:");

            for (int i = 0; i < stepsOfSorting.Count; i++)
            {
                Console.WriteLine($"Крок {i + 1}: {stepsOfSorting[i]}");
                Console.WriteLine();
            }
        }

        public void PrintStatistics()
        {
            if (stats == null)
            {
                throw new InvalidOperationException("Статистика недоступна, бо не відбулося сортування");
            }

            Console.WriteLine("\n--- Статистика алгоритму Merge Sort ---");
            Console.WriteLine($"Кількість порівнянь: {stats.Comparisons}");
            Console.WriteLine($"Кількість копіювань елементів: {stats.SwapsOrCopies}");
            Console.WriteLine($"Кількість рекурсивних викликів: {stats.RecursiveCalls}");
            Console.WriteLine($"Кількість злиттів: {stats.Passes}");
            Console.WriteLine($"Час виконання: {stats.ExecutionTime.TotalMilliseconds} мс");
            Console.WriteLine("---------------------------------------");
        }

        public void FindRangeByLetter(char letter)
        {
            if (books == null)
            {
                throw new InvalidOperationException("Колекцію не ініціалізовано");
            }

            if (books.Count == 0)
            {
                throw new InvalidOperationException("Ще немає книжок");
            }

            int start = -1;
            int end = -1;

            char searchLetter = char.ToLower(letter);

            for (int i = 0; i < books.Count; i++)
            {
                if (string.IsNullOrEmpty(books[i].Title))
                {
                    continue;
                }

                if (char.ToLower(books[i].Title[0]) == searchLetter)
                {
                    if (start == -1)
                    {
                        start = i; 
                    }

                    end = i; 
                }
            }

            if (start == -1)
            {
                Console.WriteLine($"\nКниг, які починаютиься з - '{letter}', немає");
            }
            else
            {
                Console.WriteLine($"\nДіапазон книг на літеру '{letter}':");

                for (int i = start; i <= end; i++)
                {
                    Console.WriteLine($"[{i + 1}] - {books[i].Title}, Автор: {books[i].Author}, Рік: {books[i].Year}");
                }
            }
        }
    }
}
