//THE COUNT
//A brute force wordlist generator
//aka count up in any number base using any symbol mapping
//aka generate all passwords possible, using these symbols

//Symbol map; number system is in base (map.Length),
//eg 0-1 would be base 2, 0-9 would be base 10, 0-F base 16, and 0-; base 93
string[] map = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A",
                "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L",
                "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W",
                "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h",
                "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s",
                "t", "u", "v", "w", "x", "y", "z", "!", "@", "#", "$",
                "%", "^", "&", "(", ")", "`", "~", "/", "*", "-", "+",
                ".", ",", "?", "<", ">", "_", "=", " ", "[", "]", "{",
                "}", "|", "\\", ":", ";" };

//Start counting from this symbol:
string[] arr = { "0" };

//We will use strings to represent our numbers, if you haven't already noticed
//We create a .txt file named with the same name as the starting symbol
string number;
number = String.Join("", arr);
string path = "B:/" + number + ".txt";

//Next we start writing lines to this .txt file, stopping when either:
//-we write the fixed number of lines (aka a good filesize) that we wanted, or
//-stop when we hit a requested length in the number system we are in, or
//-add your own stopping conditions
using (StreamWriter file = new StreamWriter(path))
{
    int count = 0;
    int requested_lines = 50000000;   //Max int 2147483647
    int requested_length = 4;

    float display_1 = 0;
    float display_2 = 0;

    while ((count <= requested_lines) && (arr.Length) <= requested_length)
    {
        display_1 = ((float)location(arr[0], map) / (float)map.Length * 100);   //Measures % complete until next length increase (uses only MSB/most significant symbol location; could modify to use all)
        display_2 = (float)count / (float)requested_lines * 100;                //Measures the % complete for total desired lines

        number = String.Join("", arr);
        Console.WriteLine(number + "\t" + count + "\t\t" + display_1.ToString("0.00000") + "% til size inc\t\t" + display_2.ToString("0.00000") + "% tot req");
        file.WriteLine(number);
        plusplus(ref arr);
        count++;
    }
}

int location(string find, string[] map)
{
    for (int i = 0; i < map.Length; i++)
    {
        if (find == map[i])
        {
            return i;
        }
    }
    return -1;
}

void plusplus(ref string[] arr)
{
    int index = arr.Length - 1;
    bool done = false;

    while (!done)
    {
        //check current symbol and determine its position in our global map
        int loc = location(arr[index], map);

        //if it is the last symbol, plusplus increment will cause rollover
        if (loc == map.Length - 1)
        {
            arr[index] = map[0];
        }
        //otherwise, increment to next symbol in global map and mark current symbol as done processing
        else
        {
            arr[index] = map[loc + 1];
            done = true;
        }
        index--;

        //if final position in array is reached and we are not done,
        //then we must add new digit to continue counting
        if ((index == -1) && (!done))
        {
            Array.Resize(ref arr, arr.Length + 1);
            for (int i = arr.Length - 1; i > 0; i--)
            {
                arr[i] = arr[i - 1];
            }
            arr[0] = map[1];
            done = true;
        }
    }
}