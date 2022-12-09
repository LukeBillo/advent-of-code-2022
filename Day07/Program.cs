using Day07;

var commandLineOutputs = await File.ReadAllLinesAsync("input.txt");

var context = new CommandLineContext();
var currentLine = 0;
var endOfLines = false;

while (!endOfLines)
{
    var command = commandLineOutputs[currentLine];

    if (!command.StartsWith("$"))
    {
        throw new InvalidOperationException($"We've done something wrong; our current command is {command}, line {currentLine}");
    }
    
    if (command.StartsWith("$ cd"))
    {
        context.RunCdCommand(command);
        currentLine += 1;
    }

    if (command.StartsWith("$ ls"))
    {
        var linesForLsCommand = commandLineOutputs
            .Skip(currentLine + 1)
            .TakeWhile(line => !line.StartsWith("$"))
            .ToArray();
        
        context.RunLsCommand(command, linesForLsCommand);
        currentLine += linesForLsCommand.Length + 1;
    }

    if (currentLine == commandLineOutputs.Length)
    {
        endOfLines = true;
    }
}

context.Root.PrintTree();

const int spaceNeededForUpdate = 30000000;

var unusedSpace = 70000000 - context.Root.GetSize();
var requiredDeletionSize = spaceNeededForUpdate - unusedSpace;

var directoriesForCleanUp = context.Root.GetFoldersGreaterThanOrEqualToSize(requiredDeletionSize)
    .OrderByDescending(node => node.GetSize())
    .ToList();

var smallest = directoriesForCleanUp.Last();

Console.WriteLine($"Smallest: {smallest.Name} {smallest.GetSize()}");