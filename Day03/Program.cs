var backpacks = await File.ReadAllLinesAsync("input.txt");

var sumOfPriorities = backpacks
    .Chunk(3)
    .Select(GetDuplicateItem)
    .Select(GetPriority)
    .Sum();

Console.WriteLine(sumOfPriorities);

List<(string firstCompartment, string secondCompartment)> SplitBackpackCompartments(string[] backpacks)
{
    var compartments = new List<(string first, string second)>();
    
    foreach (var backpack in backpacks)
    {
        var halfway = backpack.Length / 2;
        var firstCompartment = backpack[..halfway];
        var secondCompartment = backpack[halfway..];
        
        compartments.Add((firstCompartment, secondCompartment));
    }

    return compartments;
}

char GetDuplicateItem(string[] groupedBackpacks)
{
    var first = groupedBackpacks[0];
    var second = groupedBackpacks[1];
    var third = groupedBackpacks[2];

    return first
        .Intersect(second)
        .Intersect(third)
        .Single();
}

int GetPriority(char item)
{
    const int startOfLowercaseAscii = 96;
    const int endOfLowercaseAscii = 122;

    const int startOfUppercaseAscii = 64;
    const int endOfUppercaseAscii = 90;
    
    var asciiValue = (int)item;

    return asciiValue switch
    {
        > startOfLowercaseAscii and <= endOfLowercaseAscii => asciiValue - 96,
        > startOfUppercaseAscii and <= endOfUppercaseAscii => asciiValue - 38,
        _ => throw new ArgumentOutOfRangeException()
    };
}