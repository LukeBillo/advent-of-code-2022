using Day10;

var input = await File.ReadAllLinesAsync("input.txt");
var instructions = input.Select(Instruction.Parse).ToList();

var cpu = new CPU();
cpu.ExecuteInstructions(instructions);

Console.WriteLine($"Recorded signal strengths: {string.Join(',', cpu.RecordedSignalStrengths)}");

var summedSignalStrengths = cpu.RecordedSignalStrengths.Take(6).Sum();
Console.WriteLine(summedSignalStrengths);