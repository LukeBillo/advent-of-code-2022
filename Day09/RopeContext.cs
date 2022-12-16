namespace Day09;

public class RopeContext
{
    public List<Position> PositionsVisitedByTail { get; private set; } = new() { new Position(0, 0) };

    private List<Position> RopePositions = new List<Position>
    {
        new(0, 0),
        new(0, 0),
        new(0, 0),
        new(0, 0),
        new(0, 0),
        new(0, 0),
        new(0, 0),
        new(0, 0),
        new(0, 0),
        new(0, 0),
    };
    
    public void PrintVisitedPositions()
    {
        var maxX = RopePositions.Max(p => p.X);
        var maxY = RopePositions.Max(p => p.Y);

        for (var y = maxY; y >= 0; y--)
        {
            for (var x = 0; x <= maxX; x++)
            {
                var position = new Position(x, y);
                var wasVisited = PositionsVisitedByTail.Contains(position);
                var ropePieceInPosition = RopePositions.IndexOf(position);

                var output = wasVisited ? "#" : ropePieceInPosition is not -1 ? $"{ropePieceInPosition}" : ".";
                
                Console.Write(output);
            }
            
            Console.WriteLine();
        }
    }

    public void Execute(List<Instruction> instructions)
    {
        foreach (var visitedPositions in instructions.Select(ExecuteInstruction))
        {
            PositionsVisitedByTail.AddRange(visitedPositions);
        }

        PositionsVisitedByTail = PositionsVisitedByTail.Distinct().ToList();
    }
    
    private List<Position> ExecuteInstruction(Instruction instruction)
    {
        var positionsVisitedByTail = new List<Position>();

        for (var step = 0; step < instruction.AmountToMove; step++)
        {
            var headPosition = RopePositions.First();
            RopePositions[0] = CalculateHeadPosition(headPosition, instruction.Direction, 1);

            for (var ropePiece = 1; ropePiece < RopePositions.Count; ropePiece++)
            {
                var positionToFollow = RopePositions[ropePiece - 1];
                var thisPosition = RopePositions[ropePiece];
                
                RopePositions[ropePiece] = CalculateFollowerPosition(positionToFollow, thisPosition);
            }

            var tail = RopePositions.Last();
            positionsVisitedByTail.Add(tail);
        }

        return positionsVisitedByTail;
    }

    private static Position CalculateHeadPosition(Position headPosition, Direction direction, int amountToMove) => direction switch
    {
        Direction.Up => headPosition with { Y = headPosition.Y + amountToMove },
        Direction.Down => headPosition with { Y = headPosition.Y - amountToMove },
        Direction.Left => headPosition with { X = headPosition.X - amountToMove },
        Direction.Right => headPosition with { X = headPosition.X + amountToMove },
        _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
    };

    private static Position CalculateFollowerPosition(Position positionBeingFollowed, Position currentPosition)
    {
        var difference = positionBeingFollowed - currentPosition;
        var absoluteDifference = difference.ToAbsolute();

        var updatedTailPosition = absoluteDifference switch
        {
            { X: <= 1, Y: <= 1 } => currentPosition,
            { X: 0 } => currentPosition with { Y = currentPosition.Y + Math.Sign(difference.Y) },
            { Y: 0 } => currentPosition with { X = currentPosition.X + Math.Sign(difference.X) },
            _ => new Position(currentPosition.X + Math.Sign(difference.X), currentPosition.Y + Math.Sign(difference.Y))
        };

        return updatedTailPosition;
    }
}
