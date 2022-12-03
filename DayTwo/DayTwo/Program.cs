

using DayTwo;

var LetterToChoice = new Dictionary<string, Choice>
{
    { "A", Choice.Rock},
    { "B", Choice.Paper },
    { "C", Choice.Scissors },
};

var LetterToResult = new Dictionary<string, Result>
{
    { "X", Result.Loss },
    { "Y", Result.Draw },
    { "Z", Result.Win }
};


var input = await File.ReadAllLinesAsync("input.txt");
var rounds = input.Select(line => line.Split(' '));

var totalScore = 0;

foreach (var round in rounds)
{
    var opponentChoice = LetterToChoice[round.First()];
    var desiredResult = LetterToResult[round.Last()];

    var score = ScoreArbiter.GetScore(opponentChoice, desiredResult);
    totalScore += score;
}

Console.WriteLine($"Total score: {totalScore}");