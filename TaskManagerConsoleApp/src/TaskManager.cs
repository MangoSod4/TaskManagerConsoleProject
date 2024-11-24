using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Manager
{
    public class TaskEqualityComparer : IEqualityComparer<Task>
    {
        public bool Equals(Task? x, Task? y)
        {
            return x?.Id == y?.Id;
        }

        public int GetHashCode(Task obj)
        {
            return obj?.Id.GetHashCode() ?? 0;
        }
    }

    public class TaskManager
    {
        private readonly Dictionary<string, bool> statusMap = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase)
        {
            {"finished", true},
            {"unfinished", false}
        };

        private readonly ConcurrentDictionary<Task, bool> tasks = new ConcurrentDictionary<Task, bool>();

        // Method to get the status value ("finished": true, "unfinished": false)
        public bool GetStatusValue(string status)
        {
            statusMap.TryGetValue(status, out bool taskStatus);
            return taskStatus;
        }

        // Method to ensure that the task is not null
        public void EnsureTaskIsNotNull(Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task), "Task cannot be null.");
            }
        }

        // Method to add a task
        public void AddTask(Task taskName)
        {
            EnsureTaskIsNotNull(taskName);

            if (!tasks.ContainsKey(taskName))
            {
                tasks.TryAdd(taskName, GetStatusValue("Unfinished"));
                Console.WriteLine("Task successfully added.");
            }
            else
            {
                Console.WriteLine($"Task {taskName} is already registered.");
                return;
            }
        }

        // Method to remove a task
        public void RemoveTask(Task taskName)
        {
            EnsureTaskIsNotNull(taskName);
            if (!tasks.TryGetValue(taskName, out bool isFinished))
            {
                Console.WriteLine("Task not found.");
                return;
            }

            if (!isFinished)
            {
                Console.WriteLine("You haven't finished the task yet, do you really want to remove? (y/n)");
                var userRequest = Console.ReadLine();

                if (string.IsNullOrEmpty(userRequest) || !userRequest.Equals("y", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }

            if (tasks.TryRemove(taskName, out _))
            {
                Console.WriteLine("Task successfully removed.");
            }
        }

        // Method to list all tasks in the list
        public void ListTasks()
        {
            if (tasks.IsEmpty)
            {
                Console.WriteLine("Tasks list is empty.");
                return;
            }

            Console.WriteLine("Tasks List:");
            foreach (var task in tasks)
            {
                Console.WriteLine($"Task ID: {task.Key.Id}, Status: {(task.Value ? "Finished" : "Unfinished")}");
            }
        }

        // Method to update task status
        public void UpdateTask(Task taskName)
        {
            EnsureTaskIsNotNull(taskName);

            if (!tasks.TryGetValue(taskName, out bool isFinished))
            {
                Console.WriteLine("Task not found.");
                return;
            }

            if (!isFinished)
            {
                tasks[taskName] = GetStatusValue("Finished");
                Console.WriteLine("Task successfully marked as finished. Great work!");
            }
            else
            {
                Console.WriteLine("Task was already marked as finished.");
            }
        }
    }
}
