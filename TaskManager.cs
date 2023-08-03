public enum TaskCategory{
    Personal,
    Work,
    Errands
}

public class TodoTask{
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskCategory Category { get; set; }
    public bool IsCompleted { get; set; }

    public TodoTask(string name, string description, TaskCategory category){
        Name = name;
        Description = description;
        Category = category;
        IsCompleted = false;
    }
}

class DemoTasks{
    const string FilePath = "tasks.csv";

    static async Task Main(string[] args){
        
        List<TodoTask> tasks = await ReadTasksFromFileAsync();

        Console.WriteLine("Tasks:");
        foreach (TodoTask task in tasks){
            Console.WriteLine($"- {task.Name} ({task.Category}, {task.Description})");
        }

        Console.WriteLine("\nEnter a category to filter tasks (Personal, Work, or Errands): ");
        string category = Console.ReadLine();

        if (Enum.TryParse(category, true, out TaskCategory taskCategory)){
            List<TodoTask> filteredTasks = tasks.Where(task => task.Category == taskCategory).ToList();
            Console.WriteLine("Filtered tasks:");

            foreach (TodoTask task in filteredTasks){
                Console.WriteLine($"- {task.Name} ({task.Category}, {task.Description})");
            }
        }
        else
            Console.WriteLine("Invalid category.");

        Console.WriteLine("\nEnter a new task name: ");
        string newName = Console.ReadLine();

        Console.WriteLine("Enter a new task description: ");
        string newDescription = Console.ReadLine();

        Console.WriteLine("Enter the category for the new task (Personal, Work, or Errands): ");
        string newCategory = Console.ReadLine();

        if (Enum.TryParse(newCategory, true, out TaskCategory newTaskCategory)){
            TodoTask newTask = new TodoTask(newName, newDescription, newTaskCategory);
            tasks.Add(newTask);

            await SaveTasksToFileAsync(tasks);

            Console.WriteLine("New task added successfully!");
        }
        else
            Console.WriteLine("Invalid category. Task not added.");

    }

    static async Task SaveTasksToFileAsync(List<TodoTask> tasks){
        try{
            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                foreach (TodoTask task in tasks){
                    await writer.WriteLineAsync($"{task.Name},{task.Description},{task.Category},{task.IsCompleted}");
                }
            }
        }
        catch (Exception ex){
            Console.WriteLine($"Error writing to file: {ex.Message}");
        }
    }
    static async Task<List<TodoTask>> ReadTasksFromFileAsync(){
        List<TodoTask> tasks = new List<TodoTask>();

        try{
            using (StreamReader reader = new StreamReader(FilePath))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 4 && Enum.TryParse(parts[2], true, out TaskCategory category)){
                        
                        tasks.Add(new TodoTask(parts[0], parts[1], category));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading from file: {ex.Message}");
        }

        return tasks;
    }
}
