Console.Write("Enter a string: ");

string str = Console.ReadLine();

 static bool palidrome(string str){
    int left = 0;
    int right = str.Length - 1;

    while (left <= right){
        if (str[left] == str[right]){
            left += 1;
            right -= 1;
        }
        else{
            return false;
        }
    }
    return true;
}

if (palidrome(str))
    Console.WriteLine($"{str} is a palindrome.");

else
    Console.WriteLine($"{str} is not a palindrome.");
