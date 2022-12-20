namespace Day10;

public class CPU
{
    public int Cycle = 1;
    public int RegisterX = 1;

    private const int ScreenWidth = 40;

    public List<int> RecordedSignalStrengths = new();

    public void ExecuteInstructions(List<Instruction> instructions)
    {
        Console.WriteLine($"Cycle {Cycle}: RegisterX is {RegisterX}");
        
        var isExecuting = true;
        var currentInstruction = 0;
        
        while (isExecuting)
        {
            var instruction = instructions[currentInstruction];
            var isInstructionDone = Tick(instruction);
            
            if (isInstructionDone)
            {
                currentInstruction += 1;
            }

            isExecuting = currentInstruction != instructions.Count;

            for (var i = 0; i < ScreenWidth; i++)
            {
                
            }
        }
    }

    private bool Tick(Instruction instruction)
    {
        Cycle += 1;
        instruction.NextCycle();

        if (!instruction.IsComplete)
        {
            return false;
        }
        
        Execute(instruction);
        return true;
    }

    private void Execute(Instruction instruction)
    {
        var updatedRegisterValue = instruction.Type switch
        {
            InstructionType.Noop => RegisterX,
            InstructionType.Addx => Addx(RegisterX, instruction.Arguments),
            _ => throw new ArgumentOutOfRangeException()
        };

        RegisterX = updatedRegisterValue;
    }

    private int Addx(int registerValue, string[] arguments)
    {
        if (arguments.Length is not 1)
        {
            throw new InvalidOperationException();
        }

        var amountToAdd = int.Parse(arguments.Single());
        return registerValue + amountToAdd;
    }
}
