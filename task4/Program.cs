using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task4
{
    public class TaskScheduler<TTask, TPriority>
    {
        private readonly SortedDictionary<TPriority, Queue<TTask>> taskQueue = new SortedDictionary<TPriority, Queue<TTask>>();
        private readonly Func<TTask, TPriority> prioritySelector;

        public delegate void TaskExecution(TTask task);

        public TaskScheduler(Func<TTask, TPriority> prioritySelector)
        {
            this.prioritySelector = prioritySelector ?? throw new ArgumentNullException(nameof(prioritySelector));
        }

        public void AddTask(TTask task)
        {
            TPriority priority = prioritySelector(task);

            if (!taskQueue.ContainsKey(priority))
            {
                taskQueue[priority] = new Queue<TTask>();
            }

            taskQueue[priority].Enqueue(task);
        }

        public void ExecuteNext(TaskExecution taskExecution)
        {
            if (taskQueue.Count > 0)
            {
                var highestPriority = taskQueue.Keys.Last();
                var nextTask = taskQueue[highestPriority].Dequeue();
                taskExecution(nextTask);

                if (taskQueue[highestPriority].Count == 0)
                {
                    taskQueue.Remove(highestPriority);
                }
            }
            else
            {
                Console.WriteLine("No tasks in the queue.");
            }
        }

        public void PrintTasks()
        {
            foreach (var priority in taskQueue.Keys)
            {
                Console.WriteLine($"Priority {priority}:");

                foreach (var task in taskQueue[priority])
                {
                    Console.WriteLine($"  - {task}");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TaskScheduler<string, int> taskScheduler = new TaskScheduler<string, int>(s => s.Length);

            Console.WriteLine("Enter tasks (empty line to stop):");
            string input;
            do
            {
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    taskScheduler.AddTask(input);
                }
            } while (!string.IsNullOrEmpty(input));

            Console.WriteLine("\nExecuting next task:");
            taskScheduler.ExecuteNext(task => Console.WriteLine($"Task executed: {task}"));

            taskScheduler.AddTask("New Task 1");
            taskScheduler.AddTask("New Task 2");

            Console.WriteLine("\nCurrent tasks in the queue:");
            taskScheduler.PrintTasks();

            Console.WriteLine("\nExecuting next task:");
            taskScheduler.ExecuteNext(task => Console.WriteLine($"Task executed: {task}"));

            Console.WriteLine("\nCurrent tasks in the queue:");
            taskScheduler.PrintTasks();
        }
    }
}
