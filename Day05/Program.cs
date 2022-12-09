using Day05;

var input = await File.ReadAllLinesAsync("input.txt");

var stacks = StacksParser.Parse(input);
var instructions = InstructionsParser.Parse(input);

foreach (var stack in stacks)
{
    Console.WriteLine($"{stack.Key}: {string.Join(',', stack.Value)}");
}

Console.WriteLine();

foreach (var instruction in instructions)
{
    Console.WriteLine($"Move from {instruction.MoveFrom}, Move to {instruction.MoveTo}, Amount {instruction.AmountToMove}");
}

Console.WriteLine();

foreach (var instruction in instructions)
{
    var movedItems = stacks[instruction.MoveFrom]
        .TakeLast(instruction.AmountToMove);
        //.Reverse(); - from part 1

    stacks[instruction.MoveTo].AddRange(movedItems);
    stacks[instruction.MoveFrom].RemoveLast(instruction.AmountToMove);
    
    foreach (var stack in stacks)
    {
        Console.WriteLine($"{stack.Key}: {string.Join(',', stack.Value)}");
    }
    
    Console.WriteLine();
}

var lastOfEachStackMessage = stacks
    .Select(stack => stack.Value.LastOrDefault())
    .Aggregate((current, next) => $"{current}{next}");
    
Console.WriteLine($"Message: {lastOfEachStackMessage}");