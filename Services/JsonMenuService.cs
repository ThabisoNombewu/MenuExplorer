using System.Text.Json;
using MenuExplorer.Models;

namespace MenuExplorer.Services;

public class JsonMenuService : IMenuService
{
    // Order categories the way a menu reads, not alphabetically.
    private static readonly string[] CategoryOrder = ["Starters", "Mains", "Desserts", "Drinks"];

    private readonly List<MenuItem> _items;

    public JsonMenuService(IWebHostEnvironment env)
    {
        var path = Path.Combine(env.ContentRootPath, "Data", "menu.json");
        var json = File.ReadAllText(path);

        _items = JsonSerializer.Deserialize<List<MenuItem>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? [];
    }

    public IReadOnlyList<MenuItem> GetAll() => _items;

    public IReadOnlyList<MenuItem> GetByCategory(string? category)
    {
        if (string.IsNullOrWhiteSpace(category))
            return _items;

        return _items
            .Where(i => i.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    // No "special" flag in the data, so the specials are just the
    // best rated dishes (drinks left out, nobody features a cola).
    public IReadOnlyList<MenuItem> GetSpecials(int count = 3) =>
        _items
            .Where(i => i.Category != "Drinks")
            .OrderByDescending(i => i.Rating)
            .ThenBy(i => i.Id)
            .Take(count)
            .ToList();

    public IReadOnlyList<string> GetCategories() =>
        _items
            .Select(i => i.Category)
            .Distinct()
            .OrderBy(c => Array.IndexOf(CategoryOrder, c) is var idx && idx >= 0 ? idx : int.MaxValue)
            .ThenBy(c => c)
            .ToList();

    public MenuItem? GetById(int id) => _items.FirstOrDefault(i => i.Id == id);
}
