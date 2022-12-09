namespace Day07;

public interface IDirectoryNode
{
    public string Name { get; init; }
    public bool IsFile { get; }
    public long GetSize();
    public void PrintTree(int indentation = 0);
}
