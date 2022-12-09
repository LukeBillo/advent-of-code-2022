namespace Day07;

public class CommandLineContext
{
    public FolderNode Root { get; private set; } = null!;

    private FolderNode? _currentFolder;

    public void RunCdCommand(string line)
    {
        if (!line.StartsWith("$ cd"))
        {
            throw new InvalidOperationException($"Expected cd command, but was {line}");
        }
        
        var cdTarget = line.Replace("$ cd ", "");

        if (cdTarget is "..")
        {
            _currentFolder = _currentFolder?.Parent ?? throw new InvalidOperationException($"Attempted to cd upwards, but current folder or parent folder is null");
            return;
        }
        
        var folder = _currentFolder is null ?
            new FolderNode(cdTarget, null) :
            (FolderNode) _currentFolder.Children.Single(f => f.Name == cdTarget);

        if (cdTarget == "/")
        {
            Root = folder;
        }

        _currentFolder = folder;
    }

    public void RunLsCommand(string command, string[] lines)
    {
        if (!command.StartsWith("$ ls"))
        {
            throw new InvalidOperationException($"Expected ls command, but was {command}");
        }

        if (_currentFolder is null)
        {
            throw new InvalidOperationException("Tried to use ls, but we aren't even in a folder!");
        }

        foreach (var line in lines)
        {
            if (line.StartsWith("dir"))
            {
                var name = line.Replace("dir ", "");
                _currentFolder.Children.Add(new FolderNode(name, _currentFolder));
            }
            else
            {
                var fileSizeAndName = line.Split(' ');
                var name = fileSizeAndName.Last();
                var size = long.Parse(fileSizeAndName.First());
                
                _currentFolder.Children.Add(new FileNode(name, size));
            }
        }
    }
}
