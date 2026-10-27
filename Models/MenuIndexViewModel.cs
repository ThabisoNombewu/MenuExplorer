namespace MenuExplorer.Models;

public class MenuIndexViewModel
{
    public IReadOnlyList<MenuItem> Items { get; init; } = [];
    public IReadOnlyList<MenuItem> Specials { get; init; } = [];
    public string? Category { get; init; }
}
