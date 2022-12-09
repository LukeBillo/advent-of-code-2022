using System.Text.RegularExpressions;

namespace Day05;

public static class StacksParser
{
    private static readonly Regex CrateItemRegex = new Regex("(\\[[A-Z]\\])");
    
    public static Dictionary<int, List<string>> Parse(string[] inputLines)
    {
        var itemsInRows = inputLines
            .TakeWhile(line => !line.Contains('1'))
            .Reverse();

        var numberOfStacks = int.Parse(inputLines
            .First(line => line.Contains('1'))
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Last());

        var stacksByNumber = new Dictionary<int, List<string>>();

        for (var i = 0; i < numberOfStacks; i++)
        {
            stacksByNumber.Add(i + 1, new List<string>());
        }

        foreach (var rowOfItems in itemsInRows)
        {
            // "[A] " -> "[A]" or
            // "    " -> ""
            var items = rowOfItems.SplitIntoLengthsOf(4).Select(item => item.Trim()).ToArray();

            for (var i = 0; i < numberOfStacks; i++)
            {
                // "[A]" => "A"
                var item = items[i].Trim(' ', '[', ']');

                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }
                
                stacksByNumber[i + 1].Add(item);
            }
        }

        /*
         * 1: A, B, C
         * 2: D, E
         * 3: F, G, H, I
         */
        return stacksByNumber;
    }
}
