namespace Day11;

public record Monkey(int Identifier, List<Item> Items, Func<Item, Item> Operation, Func<Item, int> WhichMonkeyGetsItem)
{
    public int Inspections { get; private set; } = 0;

    public (int monkeyToHoldItem, Item updatedItem) InspectItem(Item item)
    {
        if (!Items.Contains(item))
        {
            throw new InvalidOperationException();
        }
        
        //var updatedItem = new Item(WorryAmount: Operation(item).WorryAmount.DivideByAndRoundDown(3));
        var updatedItem = Operation(item);
        var nextMonkeyIdentifier = WhichMonkeyGetsItem(updatedItem);

        Inspections += 1;

        return (nextMonkeyIdentifier, updatedItem);
    }
    
    public void ThrowItems()
    {
        Items.Clear();
    }

    public void TakeItem(Item item)
    {
        Items.Add(item);
    }

    public void OutputItems()
    {
        var worryAmounts = Items.Select(i => i.WorryAmount);
        Console.WriteLine($"Monkey {Identifier}: {string.Join(',', worryAmounts)}");
    }
}
