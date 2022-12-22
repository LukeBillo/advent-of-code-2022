namespace Day10;

public class CPU
{
    private int Cycle = 1;
    private int RegisterX = 1;

    private const int ScreenWidth = 40;
    private const int ScreenHeight = 6;

    private List<List<bool>> ScreenPixels;

    public CPU()
    {
        ScreenPixels = new List<List<bool>>();

        for (var rowOfPixels = 0; rowOfPixels < ScreenHeight; rowOfPixels++)
        {
            var row = new List<bool>();
            
            for (var pixel = 0; pixel < ScreenWidth; pixel++)
            {
                row.Add(false);
            }

            ScreenPixels.Add(row);
        }
    }

    public void Display()
    {
        for (var y = 0; y < ScreenHeight; y++)
        {
            Console.WriteLine();
            
            for (var x = 0; x < ScreenWidth; x++)
            {
                var isLit = ScreenPixels[y][x];
                Console.Write(isLit ? "#" : ".");
            }
        }

        Console.WriteLine();
    }

    public void ExecuteInstructions(List<Instruction> instructions)
    {
        Console.WriteLine($"Cycle {Cycle}: RegisterX is {RegisterX}");
        
        var isExecuting = true;
        var currentInstruction = 0;
        
        while (isExecuting)
        {
            var spritePositionLeft = RegisterX - 1;
            var spritePositionRight = RegisterX + 1;

            var horizontalPosition = (Cycle - 1) % ScreenWidth;
            
            if (horizontalPosition >= spritePositionLeft && horizontalPosition <= spritePositionRight)
            {
                var pixelRow = (Cycle - 1) / ScreenWidth % 6;
                
                ScreenPixels[pixelRow][horizontalPosition] = true;
            }

            var instruction = instructions[currentInstruction];
            var isInstructionDone = Tick(instruction);
            
            if (isInstructionDone)
            {
                currentInstruction += 1;
            }

            isExecuting = currentInstruction != instructions.Count;
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
