using Day11;

var input = await File.ReadAllLinesAsync("input.txt");
var monkeys = NotesParser.Parse(input);

var engine = new MonkeyInTheMiddleEngine(monkeys);
engine.Run(20);

var orderedMonkeysByInspections = engine.Monkeys
    .OrderBy(monkey => monkey.Inspections)
    .ToList();
    
var output = orderedMonkeysByInspections.Select(monkey => $"ID {monkey.Identifier}: {monkey.Inspections} inspections");

Console.WriteLine($"Monkeys:\n{string.Join('\n', output)}");

var first = orderedMonkeysByInspections[^1];
var second = orderedMonkeysByInspections[^2];

Console.WriteLine(first.Inspections * second.Inspections);