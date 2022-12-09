using System.Text.RegularExpressions;

namespace Day05;

public static class InstructionsParser
{
    private static readonly Regex InstructionRegex = new("move (\\d+) from (\\d+) to (\\d+)");
    
    public static List<Instruction> Parse(string[] inputLines) =>
        inputLines
            .SkipWhile(line => !InstructionRegex.IsMatch(line))
            .Select(line => InstructionRegex.Matches(line))
            .Select(matches => matches.Single().Groups.Values.Skip(1).Select(group => int.Parse(group.Value)).ToList())
            .Select(instructions => new Instruction(instructions[0], instructions[1], instructions[2]))
            .ToList();
}
