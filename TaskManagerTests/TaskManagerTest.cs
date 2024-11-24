using Manager;
using Xunit;

namespace TaskManagerTests;

public class TaskManagerTests
{
    [Fact]
    public void AddTask_ShouldAddTaskToList_WhenValidTaskIsProvided()
    {
        var taskManager = new TaskManager();
        var task = Task.Run(() => { });
        taskManager.AddTask(task);
    }

    [Fact]
    public void ManageTask_ShouldThrowException_WhenTaskIsNull()
    {
        var taskManager = new TaskManager();

        var ex = Assert.Throws<ArgumentNullException>(() => taskManager.AddTask(null));
        Assert.Equal("Task cannot be null. (Parameter 'task')", ex.Message);
    }
}
