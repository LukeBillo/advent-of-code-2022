using System.Text.RegularExpressions;

namespace Day11;

public static partial class NotesParser
{
    private const int IdentifierLine = 0;
    private const int StartingItemsLine = 1;
    private const int OperationLine = 2;
    private const int StartOfMonkeyThrowActionLines = 3;
    private const int EndOfMonkeyThrowActionLines = EndOfMonkeyLine - 1;
    private const int EndOfMonkeyLine = 7;

    private const int TestLine = 0;
    private const int IfTestTrueLine = 1;
    private const int IfTestFalseLine = 2;

    private const int LinesPerMonkey = 7;
    
    [GeneratedRegex("Operation: new = old ([+-/*]) ([0-9]+|old)")]
    private static partial Regex OperationRegex();
    
    [GeneratedRegex("divisible by ([0-9]+)")]
    private static partial Regex DivisibleByRegex();
    
    [GeneratedRegex("throw to monkey ([0-9]+)")]
    private static partial Regex ThrowToMonkeyRegex();
    
    public static List<Monkey> Parse(string[] lines) => lines.Chunk(LinesPerMonkey).Select(ParseMonkeyBehaviour).ToList();

    private static Monkey ParseMonkeyBehaviour(string[] lines)
    {
        if (lines.Length != LinesPerMonkey)
        {
            throw new InvalidOperationException();
        }

        
        var identifier = GetMonkeyIdentifier(lines[IdentifierLine]);
        var startingItems = GetStartingItems(lines[StartingItemsLine]);
        var operation = GetOperation(lines[OperationLine]);
        var throwAction = GetMonkeyThrowAction(lines[StartOfMonkeyThrowActionLines..EndOfMonkeyThrowActionLines]);

        return new Monkey(identifier, startingItems, operation, throwAction);
    }

    private static int GetMonkeyIdentifier(string line)
    {
        var identifier = line[7].ToString();
        return int.Parse(identifier);
    }

    private static List<Item> GetStartingItems(string line) =>
        line.Replace("Starting items: ", "")
            .Split(',')
            .Select(int.Parse)
            .Select(worryAmount => new Item(worryAmount))
            .ToList();

    private static Func<Item, Item> GetOperation(string line)
    {
        var operationRegex = OperationRegex();
        var matchGroupValues = operationRegex.Matches(line)
            .SelectMany(match => match.Groups.Values)
            .Select(group => group.Value)
            .Skip(1)
            .ToList();

        if (matchGroupValues.Count != 2)
        {
            throw new InvalidOperationException();
        }

        var mathOperation = matchGroupValues.First();
        var argumentForMathOperation = matchGroupValues.Last();

        return mathOperation switch
        {
            "+" => item => new Item(WorryAmount: item.WorryAmount + ParseArgument(item, argumentForMathOperation)),
            "-" => item => new Item(WorryAmount: item.WorryAmount - ParseArgument(item, argumentForMathOperation)),
            "*" => item => new Item(WorryAmount: item.WorryAmount * ParseArgument(item, argumentForMathOperation)),
            "/" => item => new Item(WorryAmount: item.WorryAmount / ParseArgument(item, argumentForMathOperation)),
            _ => throw new InvalidOperationException()
        };
    }

    private static double ParseArgument(Item item, string argument) => argument == "old" ? item.WorryAmount : double.Parse(argument);

    private static Func<Item, int> GetMonkeyThrowAction(string[] lines)
    {
        var testLine = lines[TestLine];
        var ifTestTrueLine = lines[IfTestTrueLine];
        var ifTestFalseLine = lines[IfTestFalseLine];

        var testDivisibleBy = ParseDivisibleByTest(testLine);
        var ifTrueThrowToMonkeyIdentifier = ParseThrowToAction(ifTestTrueLine);
        var ifFalseThrowToMonkeyIdentifier = ParseThrowToAction(ifTestFalseLine);

        return item => item.WorryAmount % testDivisibleBy == 0 ? 
            ifTrueThrowToMonkeyIdentifier : 
            ifFalseThrowToMonkeyIdentifier;
    }

    private static double ParseDivisibleByTest(string line)
    {
        var testRegex = DivisibleByRegex();
        var divideByValue = testRegex.Matches(line)
            .SelectMany(match => match.Groups.Values)
            .Skip(1)
            .Single().Value;

        return double.Parse(divideByValue);
    }
    
    private static int ParseThrowToAction(string line)
    {
        var throwToMonkeyRegex = ThrowToMonkeyRegex();
        var monkeyIdentifier = throwToMonkeyRegex.Matches(line)
            .SelectMany(match => match.Groups.Values)
            .Skip(1)
            .Single().Value;

        return int.Parse(monkeyIdentifier);
    }
}
