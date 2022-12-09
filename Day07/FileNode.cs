namespace Day07;

public class FileNode : IDirectoryNode
{
    public FileNode(string name, long size)
    {
        Name = name;
        Size = size;
    }
    
    public string Name { get; init; }
    public long Size { get; init; }

    public bool IsFile => true;
    public long GetSize() => Size;
    
    public void PrintTree(int indentation = 0)
    {
        var lineStart = new string(' ', indentation);
        lineStart = $"{lineStart}|- ";
        
        Console.WriteLine($"{lineStart}{Size} {Name}");
    }
}
