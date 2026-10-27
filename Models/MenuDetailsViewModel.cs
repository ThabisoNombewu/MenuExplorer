namespace MenuExplorer.Models;

public class MenuDetailsViewModel
{
    public required MenuItem Item { get; init; }
    public IReadOnlyList<MenuItem> Related { get; init; } = [];
}
