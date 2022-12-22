using Day10;

var input = await File.ReadAllLinesAsync("input.txt");
var instructions = input.Select(Instruction.Parse).ToList();

var cpu = new CPU();
cpu.ExecuteInstructions(instructions);

cpu.Display();