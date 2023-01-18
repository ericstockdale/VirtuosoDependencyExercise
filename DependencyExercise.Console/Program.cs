// See https://aka.ms/new-console-template for more information
using DependencyExercise;
using System.Text;

var input = GetTestDependentItemsInput();
var sequencer = new DependencySequencer();


var output = sequencer.GetDependentItemsOutput(sequencer.GetDependentItemsList(input));

Console.WriteLine("Original Input");
Console.WriteLine("****************************************************************************");
Console.WriteLine(GetTestDependentItemsInputAsString(input));
Console.WriteLine("****************************************************************************");
Console.WriteLine();
Console.WriteLine();
Console.WriteLine("Sorted by Dependency Output");
Console.WriteLine("****************************************************************************");
Console.WriteLine(output.ToString());
Console.WriteLine("****************************************************************************");


static string[,] GetTestDependentItemsInput()
{
    return new string[,]
    {
                //dependency    //item
                {"t-shirt",     "dress shirt"},
                {"dress shirt", "pants"},
                {"dress shirt", "suit jacket"},
                {"tie",         "suit jacket"},
                {"pants",       "suit jacket"},
                {"belt",        "suit jacket"},
                {"suit jacket", "overcoat"},
                {"dress shirt", "tie"},
                {"suit jacket", "sun glasses"},
                {"sun glasses", "overcoat"},
                {"left sock",   "pants"},
                {"pants",       "belt"},
                {"suit jacket", "left shoe"},
                {"suit jacket", "right shoe"},
                {"left shoe",   "overcoat"},
                {"right sock",  "pants"},
                {"right shoe",  "overcoat"},
                {"t-shirt",     "suit jacket"}
    };
}

static string GetTestDependentItemsInputAsString(string[,] input)
{
    StringBuilder sb = new StringBuilder();

    int index = 0;

    while (index < (input.Length/2))
    {
        sb.AppendLine($"{input[index, 0]}, {input[index, 1]}");
        index++; 
    }

    return sb.ToString();
}