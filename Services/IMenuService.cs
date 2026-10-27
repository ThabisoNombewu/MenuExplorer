using MenuExplorer.Models;

namespace MenuExplorer.Services;

public interface IMenuService
{
    IReadOnlyList<MenuItem> GetAll();
    IReadOnlyList<MenuItem> GetByCategory(string? category);
    IReadOnlyList<MenuItem> GetSpecials(int count = 3);
    IReadOnlyList<string> GetCategories();
    MenuItem? GetById(int id);
}
