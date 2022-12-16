namespace Day09;

public record Instruction(Direction Direction, int AmountToMove)
{
    public static Instruction Parse(string line)
    {
        var split = line.Split(' ');
        var direction = ParseDirection(split.First());
        var amountToMove = int.Parse(split.Last());

        return new Instruction(direction, amountToMove);
    }
    
    private static Direction ParseDirection(string direction)
    {
        return direction switch
        {
            "U" => Direction.Up,
            "D" => Direction.Down,
            "L" => Direction.Left,
            "R" => Direction.Right,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}
