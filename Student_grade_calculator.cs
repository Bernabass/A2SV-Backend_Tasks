using System.Collections.Generic;

string Title = """
             _ _ _ _ _ _ _ _ _ _ _ _ _ __
            |  STUDENT GRADE CALCULATOR  |
            |_ _ _ _ _ _ _ _ _ _ _ _ _ __|

            """;

Console.WriteLine(Title);

Console.Write("Enter student's name: ");
string studentName = Console.ReadLine();

Console.Write("Enter the number of subjects: ");

int n;
while (!int.TryParse(Console.ReadLine(), out n) || n <= 0){

    Console.Write("Invalid input!. Please enter a valid number of subjects: ");
}
Dictionary<string, double> grades = new Dictionary<string, double>();

for (int i = 0; i < n; i++){

    Console.Write($"Enter the name of subject {i + 1}: ");
    string curr_subject = Console.ReadLine();
    Console.Write($"Enter the grade for {curr_subject}: ");

    double curr_grade;
   
    while (!double.TryParse(Console.ReadLine(), out curr_grade) || curr_grade < 0 || curr_grade > 100){
        Console.Write($"Invalid input. Please enter a valid grade for {curr_subject}: ");
    }

    grades.Add(curr_subject, curr_grade);
}

double average = calc_average(grades);

string result = """
                 _ _ _ _ _ 
                | RESULT  |
                | _ _ _ _ |

                """;

Console.WriteLine(result);
Console.WriteLine($"Student Name: {studentName}");

foreach (var pair in grades){
    Console.WriteLine($"{pair.Key}: {pair.Value}");

    }

Console.WriteLine($"Average grade: {average}");

static double calc_average(Dictionary<string, double> grades){
    double total = 0;

    foreach (var curr_grade in grades.Values){
        total += curr_grade;
    }

    return total / grades.Count;

}
