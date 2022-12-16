namespace Day08;

public class TreeGrid
{
    public List<Tree> Trees => _grid.SelectMany(trees => trees.Select(tree => tree)).ToList();
    
    private readonly List<List<Tree>> _grid;
    
    public TreeGrid(List<List<Tree>> grid)
    {
        _grid = grid;
    }

    public bool IsVisible(Tree tree)
    {
        if (tree.Position.X is 0 || tree.Position.Y is 0)
        {
            return true;
        }
        
        var gridXLength = _grid.Count;
        var gridYLength = _grid.First().Count;

        var (north, south, east, west) = tree.Position.GetPositionsOnSameColumnOrRow(gridXLength, gridYLength);

        return north.All(position => !_grid[position.Y][position.X].IsHigherThanOrSameHeightAs(tree)) ||
               south.All(position => !_grid[position.Y][position.X].IsHigherThanOrSameHeightAs(tree)) ||
               east.All(position => !_grid[position.Y][position.X].IsHigherThanOrSameHeightAs(tree)) ||
               west.All(position => !_grid[position.Y][position.X].IsHigherThanOrSameHeightAs(tree));
    }

    public int CalculateScenicScore(Tree tree)
    {
        var gridXLength = _grid.Count;
        var gridYLength = _grid.First().Count;
        
        var (north, south, east, west) = tree.Position.GetPositionsOnSameColumnOrRow(gridXLength, gridYLength);
        north.Reverse();
        west.Reverse();

        var northVisibilityScore = north.CountUpUntilAndIncludingWhere(position => !_grid[position.Y][position.X].IsHigherThanOrSameHeightAs(tree));
        var southVisibilityScore = south.CountUpUntilAndIncludingWhere(position => !_grid[position.Y][position.X].IsHigherThanOrSameHeightAs(tree));
        var eastVisibilityScore = east.CountUpUntilAndIncludingWhere(position => !_grid[position.Y][position.X].IsHigherThanOrSameHeightAs(tree));
        var westVisibilityScore = west.CountUpUntilAndIncludingWhere(position => !_grid[position.Y][position.X].IsHigherThanOrSameHeightAs(tree));

        return northVisibilityScore * southVisibilityScore * eastVisibilityScore * westVisibilityScore;
    }

    public void Print()
    {
        foreach (var trees in _grid)
        {
            Console.WriteLine(string.Join(' ', trees.Select(CalculateScenicScore)));
        }
    }

    public static TreeGrid Parse(string[] input)
    {
        var grid = new List<List<Tree>>();

        for (var y = 0; y < input.Length; y++)
        {
            var rowOfTrees = new List<Tree>();
            
            for (var x = 0; x < input[y].Length; x++)
            {
                var height = int.Parse(input[y][x].ToString());
                var position = new Position(x, y);
                
                rowOfTrees.Add(new Tree(position, height));
            }
            
            grid.Add(rowOfTrees);
        }

        return new TreeGrid(grid);
    }
}