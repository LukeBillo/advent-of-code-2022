using Day08;

var input = await File.ReadAllLinesAsync("input.txt");
var treeGrid = TreeGrid.Parse(input);

treeGrid.Print();

var highestScenicScore = treeGrid.Trees.Max(tree => treeGrid.CalculateScenicScore(tree));

Console.WriteLine($"Highest scenic score: {highestScenicScore}");