namespace DayTwo;

public static class ScoreArbiter
{
    private static readonly Dictionary<Result, int> ResultToScore = new Dictionary<Result, int>
    {
        {
            Result.Loss, 0
        },
        {
            Result.Draw, 3
        },
        {
            Result.Win, 6
        },
    };

    private static readonly Dictionary<Choice, int> ChoiceToScore = new Dictionary<Choice, int>
    {
        {
            Choice.Rock, 1
        },
        {
            Choice.Paper, 2
        },
        {
            Choice.Scissors, 3
        },
    };

    public static int GetScore(Choice opponentsChoice, Result desiredResult)
    {
        var choiceForDesiredResult = GetChoice(opponentsChoice, desiredResult);

        var resultScore = ResultToScore[desiredResult];
        var choiceScore = ChoiceToScore[choiceForDesiredResult];

        return resultScore + choiceScore;
    }

    private static Choice GetChoice(Choice opponentsChoice, Result desiredResult) => desiredResult switch
    {
        Result.Draw => opponentsChoice,
        Result.Loss => GetLossChoice(opponentsChoice),
        Result.Win => GetWinChoice(opponentsChoice),
        _ => throw new ArgumentOutOfRangeException(nameof(desiredResult), desiredResult, null)
    };
    private static Choice GetLossChoice(Choice opponentsChoice) => opponentsChoice switch
    {
        Choice.Paper => Choice.Rock,
        Choice.Rock => Choice.Scissors,
        Choice.Scissors => Choice.Paper,
        _ => throw new ArgumentOutOfRangeException(nameof(opponentsChoice), opponentsChoice, null)
    };

    private static Choice GetWinChoice(Choice opponentsChoice) => opponentsChoice switch
    {
        Choice.Rock => Choice.Paper,
        Choice.Paper => Choice.Scissors,
        Choice.Scissors => Choice.Rock,
        _ => throw new ArgumentOutOfRangeException(nameof(opponentsChoice), opponentsChoice, null)
    };

    private static Result GetResult(Choice opponentsChoice, Choice ourChoice)
    {
        if (opponentsChoice == ourChoice)
        {
            return Result.Draw;
        }

        return ourChoice switch
        {
            Choice.Rock => opponentsChoice is Choice.Scissors ? Result.Win : Result.Loss,
            Choice.Paper => opponentsChoice is Choice.Rock ? Result.Win : Result.Loss,
            Choice.Scissors => opponentsChoice is Choice.Paper ? Result.Win : Result.Loss,
            _ => throw new ArgumentOutOfRangeException(nameof(opponentsChoice), opponentsChoice, null)
        };
    }
}
