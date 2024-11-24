using Manager;

namespace TaskManagerConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            TaskManager manager = new TaskManager();

            // Creating empty tasks for exemple:
            var task1 = Task.Run(() => { });
            var task2 = Task.Run(() => { });
            var task3 = Task.Run(() => { });

            manager.AddTask(task1);
            manager.AddTask(task2);
            manager.AddTask(task3);

            manager.UpdateTask(task1);
            manager.RemoveTask(task2);
            manager.ListTasks();

            /*
            Output:
            Task successfully removed.
            Tasks List:
            Task ID: 9, Status: Finished
            Task ID: 11, Status: Unfinished
            */
        }
    }
}
