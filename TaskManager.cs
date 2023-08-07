enum TaskCategory{
    Personal,
    Work,
    Errands
}

class TaskItem{
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskCategory Category { get; set; }
    public bool IsCompleted { get; set; }
}

class TaskManager{
    private List<TaskItem> tasks = new List<TaskItem>();

    public void AddTask(TaskItem task){
        tasks.Add(task);
    }

    public void ViewTasks(Func<TaskItem, bool> filter = null){
        var filteredTasks = filter != null ? tasks.Where(filter) : tasks;

        if (!filteredTasks.Any()){
            Console.WriteLine("No tasks found.");
            return;
        }

        foreach (var task in filteredTasks){
            Console.WriteLine($"Name: {task.Name}");
            Console.WriteLine($"Description: {task.Description}");
            Console.WriteLine($"Category: {task.Category}");
            Console.WriteLine($"Is Completed: {task.IsCompleted}");
            Console.WriteLine();
        }
    }

    public async Task SaveTasksToFileAsync(string filePath){
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var task in tasks)
            {
                await writer.WriteLineAsync($"{task.Name},{task.Description},{task.Category},{task.IsCompleted}");
            }
        }
    }

    public async Task LoadTasksFromFileAsync(string filePath){
        if (!File.Exists(filePath))
            return;

        using (StreamReader reader = new StreamReader(filePath)){
            string line;
            while ((line = await reader.ReadLineAsync()) != null){
                string[] parts = line.Split(',');
                
                if (parts.Length == 4){
                    TaskItem task = new TaskItem{
                        Name = parts[0],
                        Description = parts[1],
                        Category = (TaskCategory)Enum.Parse(typeof(TaskCategory), parts[2], true),
                        IsCompleted = bool.Parse(parts[3])
                    };

                    tasks.Add(task);
                }
            }
        }
    }
}

class Program
{
    static async Task Main(string[] args){
        TaskManager taskManager = new TaskManager();

        // Load tasks from file asynchronously
        await taskManager.LoadTasksFromFileAsync("tasks.csv");

        while (true){
            Console.WriteLine("\nTask Manager");
            Console.WriteLine("------------------");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. View All Tasks");
            Console.WriteLine("3. View Tasks by Category");
            Console.WriteLine("4. Save and Exit");


            Console.Write("\nSelect an option: ");
            string choice = Console.ReadLine();

            switch (choice){
                case "1":
                    // Add Task
                    Console.Write("Task Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Description: ");
                    string description = Console.ReadLine();
                    Console.Write("Category (Personal/Work/Errands): ");
                    if (Enum.TryParse(Console.ReadLine(), true, out TaskCategory category)){
                    
                        TaskItem newTask = new TaskItem { Name = name, Description = description, Category = category };
                        taskManager.AddTask(newTask);
                        Console.WriteLine("Task added successfully.");
                    }
                    else
                        Console.WriteLine("Invalid category.");

                    break;

                case "2":
                    // View All Tasks
                    taskManager.ViewTasks();
                    break;

                case "3":
                    // View Tasks by Category
                    Console.Write("Category (Personal/Work/Errands): ");
                    if (Enum.TryParse(Console.ReadLine(), true, out TaskCategory filterCategory)){
                        taskManager.ViewTasks(task => task.Category == filterCategory);
                    }
                    else
                        Console.WriteLine("Invalid category.");
                    break;

                case "4":
                    // Save and Exit
                    await taskManager.SaveTasksToFileAsync("tasks.csv");
                    Console.WriteLine("Tasks saved to file. Good bye...");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }
}
