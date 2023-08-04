using Newtonsoft.Json;

class Student{
    public string Name { get; }
    public int Age { get; }
    public readonly int RollNumber;
    public string Grade { get; }

    public Student(string name, int age, int rollNumber, string grade){
        this.Name = name;
        this.Age = age;
        this.RollNumber = rollNumber;
        this.Grade = grade;
    }
}

class StudentList<T> where T : Student{
    private List<T> students = new List<T>();

    public void AddStudent(T student){
        this.students.Add(student);
    }

    public void DisplayAllStudents(){

        foreach (var student in this.students){
            Console.WriteLine($"Name: {student.Name}, Age: {student.Age}, RollNumber: {student.RollNumber}, Grade: {student.Grade}");
        }
    }

public IEnumerable<T> SearchStudents(string key){

    return  this.students.Where(student => student.Name.Contains(
            key, StringComparison.OrdinalIgnoreCase) || student.RollNumber.ToString().Contains(key));
}

    public void SerializeToJson(string filePath){

        var json = JsonConvert.SerializeObject(this.students, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public void DeserializeFromJson(string filePath){

        if (File.Exists(filePath)){
            var json = File.ReadAllText(filePath);
            this.students = JsonConvert.DeserializeObject<List<T>>(json);
        }
    }
}
class Program{

    static void Main(string[] args){

        var studentList = new StudentList<Student>();
        
        while (true){

            Console.WriteLine("\nStudent Management System\n");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Search Students");
            Console.WriteLine("3. Display All Students");
            Console.WriteLine("4. Save Students to JSON");
            Console.WriteLine("5. Load Students from JSON");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option: ");
            
            var choice = Console.ReadLine();
            
            switch (choice){
        
                case "1":
                    Console.Write("Name: ");
                    var name = Console.ReadLine();
                    Console.Write("Age: ");
                    var age = int.Parse(Console.ReadLine());
                    Console.Write("RollNumber: ");
                    var rollNumber = int.Parse(Console.ReadLine());
                    Console.Write("Grade: ");
                    var grade = Console.ReadLine();
                    
                    var student = new Student(name, age, rollNumber, grade);
                    studentList.AddStudent(student);
                    Console.WriteLine("Student added successfully!");
                    break;
                    
                case "2":
                    Console.Write("Search by Name or Roll Number: ");
                    var key = Console.ReadLine();
                    var searchResults = studentList.SearchStudents(key);

                    if (searchResults.Any()){
                
                        foreach (var result in searchResults){
                            Console.WriteLine($"Name: {result.Name}, Age: {result.Age}, RollNumber: {result.RollNumber}, Grade: {result.Grade}");
                        }
                    }

                    else
                        Console.WriteLine("No students found.");
                    break;
                    
                case "3":
                    studentList.DisplayAllStudents();
                    break;
                    
                case "4":
                    Console.Write("Enter file path: ");
                    var saveFilePath = Console.ReadLine();
                    studentList.SerializeToJson(saveFilePath);
                    Console.WriteLine("Students saved to JSON successfully!");
                    break;
                    
                case "5":
                    Console.Write("Enter file path: ");
                    var loadFilePath = Console.ReadLine();
                    studentList.DeserializeFromJson(loadFilePath);
                    Console.WriteLine("Students loaded from JSON successfully!");
                    break;
                    
                case "6":
                    Console.WriteLine("Good bye...");
                    return;
            }
            
            Console.WriteLine();
        }
    }
}
