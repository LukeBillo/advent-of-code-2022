var input = await File.ReadAllLinesAsync("input.txt");
var dataStream = input.Single();

var foundMarker = false;
var position = 0;

while (!foundMarker)
{
    var endOfNextFourCharacters = position + 14;
    var nextFourCharacters = dataStream[position..endOfNextFourCharacters].ToList();
    
    if (AreAllDistinct(nextFourCharacters))
    {
        foundMarker = true;
        position += 14;
        continue;
    }

    position += 1;
}

Console.WriteLine($"Start of packet marker position: {position}");

bool AreAllDistinct(IList<char> characters)
{
    var visitedCharacters = new List<char>();

    foreach (var character in characters)
    {
        if (visitedCharacters.Contains(character))
        {
            return false;
        }
        
        visitedCharacters.Add(character);
    }

    return true;
}