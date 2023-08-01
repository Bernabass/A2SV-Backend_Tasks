Console.Write("Enter a string: ");

string input = Console.ReadLine();

static Dictionary<string, int> Counter(string input){
    string[] str = input.Split(' ');
    var dict = new Dictionary<string, int>();

    foreach (string word in str){
        string curr_word = "";
        foreach (char chr in word){
            if (char.IsLetter(chr)){
                curr_word += char.ToLower(chr);
            }
            else
                curr_word += chr;

        }
        if (dict.ContainsKey(curr_word)){
            dict[curr_word] += 1;
        }
        else
            dict[curr_word] = 1;
    }

    return dict;
}

var freq = Counter(input);

foreach (var pair in freq){

    Console.WriteLine($"{pair.Key}: {pair.Value}");

}
