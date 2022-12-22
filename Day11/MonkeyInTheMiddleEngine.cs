namespace Day11;

public class MonkeyInTheMiddleEngine
{
    public List<Monkey> Monkeys { get; private set; }
    
    public MonkeyInTheMiddleEngine(List<Monkey> monkeys)
    {
        Monkeys = monkeys;
    }
    
    public void Run(int numberOfRounds)
    {
        for (var round = 0; round < numberOfRounds; round++)
        {
            foreach (var monkey in Monkeys)
            {
                InspectAllMonkeyItems(monkey);
            }

            foreach (var monkey in Monkeys)
            {
                monkey.OutputItems();
            }
            
            Console.WriteLine("\n");
        }
    }

    private void InspectAllMonkeyItems(Monkey inspectingMonkey)
    {
        foreach (var item in inspectingMonkey.Items)
        {
            var (nextMonkeyIdentifier, updatedItem) = inspectingMonkey.InspectItem(item);
            var monkeyToTakeItem = Monkeys.Single(monkey => monkey.Identifier == nextMonkeyIdentifier);
            monkeyToTakeItem.TakeItem(updatedItem);
        }
        
        inspectingMonkey.ThrowItems();
    }
}
