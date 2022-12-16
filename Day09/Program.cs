using Day09;

var input = await File.ReadAllLinesAsync("input.txt");
var instructions = input.Select(Instruction.Parse).ToList();

var ropeContext = new RopeContext();
ropeContext.Execute(instructions);

ropeContext.PrintVisitedPositions();

Console.WriteLine($"Visited: {string.Join(',', ropeContext.PositionsVisitedByTail.Select(p => $"({p.X}, {p.Y})"))}");
Console.WriteLine($"Visited {ropeContext.PositionsVisitedByTail.Count} positions");
