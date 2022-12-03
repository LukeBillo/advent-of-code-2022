var inputLines = await File.ReadAllLinesAsync("input.txt");

var elf = new ElfCalories(new List<decimal>());
var elves = new List<ElfCalories>();

foreach (var line in inputLines)
{
    if (string.IsNullOrEmpty(line))
    {
        elves.Add(elf);
        elf = new ElfCalories(new List<decimal>());
        continue;
    }
    
    elf.CaloriesHeld.Add(int.Parse(line));
}

elves.Add(elf);

//var highestCalorieElf = elves.Max(e => e.TotalCalories);
var top3Elves = elves.OrderByDescending(e => e.TotalCalories).Take(3);
var sumOfTop3Elves = top3Elves.Sum(e => e.TotalCalories);
Console.WriteLine(sumOfTop3Elves);

internal record ElfCalories(List<decimal> CaloriesHeld)
{
    public decimal TotalCalories => CaloriesHeld.Sum();
}