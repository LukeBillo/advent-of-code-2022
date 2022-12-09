var assignments = await File.ReadAllLinesAsync("input.txt");

var pairAssignments = assignments
    .Select(pair => pair.Split(','))
    .ToList();

var parsedPairs = new List<(SectionAssignment first, SectionAssignment second)>();

foreach (var pairAssignment in pairAssignments)
{
    var firstAssignment = SectionAssignment.Parse(pairAssignment.First());
    var secondAssignment = SectionAssignment.Parse(pairAssignment.Last());
    
    parsedPairs.Add((firstAssignment, secondAssignment));
}

var numberOfPairsWhichOverlap = parsedPairs.Count(pair => SectionAssignment.DoesPartiallyOverlap(pair.first, pair.second));
Console.WriteLine(numberOfPairsWhichOverlap);

public record SectionAssignment(int Start, int End)
{
    public static SectionAssignment Parse(string assignment)
    {
        var split = assignment.Split('-');
        
        var start = int.Parse(split.First());
        var end = int.Parse(split.Last());

        return new SectionAssignment(start, end);
    }

    public bool Contains(SectionAssignment other) => Start <= other.Start && End >= other.End;

    public bool PartiallyContains(SectionAssignment other) => Start <= other.End && other.Start <= End;


    public static bool DoesPartiallyOverlap(SectionAssignment first, SectionAssignment second) => first.PartiallyContains(second);

    public static bool DoesFullyOverlap(SectionAssignment first, SectionAssignment second) => first.Contains(second) || second.Contains(first);
};