namespace Day07;

public class FolderNode : IDirectoryNode
{
    public FolderNode(string name, FolderNode? parent)
    {
        Name = name;
        Parent = parent;
        Children = new List<IDirectoryNode>();
    }
    
    public string Name { get; init; }
    public List<IDirectoryNode> Children { get; init; }
    public FolderNode? Parent { get; init; }

    public bool IsFile => false;
    public long GetSize() => Children.Sum(c => c.GetSize());

    public void PrintTree(int indentation = 0)
    {
        var lineStart = new string(' ', indentation);
        lineStart = $"{lineStart}|- ";

        Console.WriteLine($"{lineStart}{Name}");

        foreach (var child in Children)
        {
            child.PrintTree(indentation + 1);
        }
    }

    public IEnumerable<FolderNode> GetFoldersLessThanOrEqualToSize(long size, List<FolderNode>? foldersLessThanSize = null)
    {
        foldersLessThanSize ??= new List<FolderNode>();

        if (GetSize() <= size)
        {
            foldersLessThanSize.Add(this);
        }

        var folderChildren = Children.OfType<FolderNode>();
        foreach (var folderChild in folderChildren)
        {
            folderChild.GetFoldersLessThanOrEqualToSize(size, foldersLessThanSize);
        }
        
        return foldersLessThanSize;
    }
    
    public IEnumerable<FolderNode> GetFoldersGreaterThanOrEqualToSize(long size, List<FolderNode>? foldersGreaterThanSize = null)
    {
        foldersGreaterThanSize ??= new List<FolderNode>();

        if (GetSize() >= size)
        {
            foldersGreaterThanSize.Add(this);
        }

        var folderChildren = Children.OfType<FolderNode>();
        foreach (var folderChild in folderChildren)
        {
            folderChild.GetFoldersGreaterThanOrEqualToSize(size, foldersGreaterThanSize);
        }
        
        return foldersGreaterThanSize;
    }
}
