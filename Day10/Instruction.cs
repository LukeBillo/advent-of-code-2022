namespace Day10;

public record Instruction(InstructionType Type, params string[] Arguments)
{
    public int CyclesRan { get; private set; } = 0;

    public bool IsComplete => Type switch
    {
        InstructionType.Noop => CyclesRan is 1,
        InstructionType.Addx => CyclesRan is 2,
        _ => throw new ArgumentOutOfRangeException()
    };

    public void NextCycle()
    {
        CyclesRan += 1;
    }

    public static Instruction Parse(string line)
    {
        var split = line.Split(' ');

        var instructionType = Enum.Parse<InstructionType>(split.First(), ignoreCase: true);
        var arguments = split.Skip(1).ToArray();

        return new Instruction(instructionType, arguments);
    }
}
