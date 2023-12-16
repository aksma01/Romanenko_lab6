using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2
{
    public delegate bool Criteria<T>(T item);
    public class Repository<T>
    {
        private List<T> items;

        public Repository()
        {
            items = new List<T>();
        }

        public void Add(T item)
        {
            items.Add(item);
        }

        public IEnumerable<T> Find(Criteria<T> criteria)
        {
            foreach (T item in items)
            {
                if (criteria(item))
                {
                    yield return item;
                }
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Repository<int> intRepository = new Repository<int>();
            intRepository.Add(1);
            intRepository.Add(2);
            intRepository.Add(3);

            Criteria<int> evenNumberCriteria = x => x % 2 == 0;

            Console.WriteLine("Even numbers in the repository:");
            foreach (int number in intRepository.Find(evenNumberCriteria))
            {
                Console.WriteLine(number);
            }

            Repository<string> stringRepository = new Repository<string>();
            stringRepository.Add("apple");
            stringRepository.Add("banana");
            stringRepository.Add("cherry");

            Criteria<string> startsWithBCriteria = s => s.StartsWith("b", StringComparison.OrdinalIgnoreCase);

            Console.WriteLine("\nStrings starting with 'b' in the repository:");
            foreach (string str in stringRepository.Find(startsWithBCriteria))
            {
                Console.WriteLine(str);
            }
        }
    }
}