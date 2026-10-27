using Microsoft.AspNetCore.Mvc;
using MenuExplorer.Models;
using MenuExplorer.Services;

namespace MenuExplorer.Controllers;

public class MenuController : Controller
{
    private readonly IMenuService _menu;

    public MenuController(IMenuService menu)
    {
        _menu = menu;
    }

    public IActionResult Index(string? category)
    {
        var items = _menu.GetByCategory(category);

        var model = new MenuIndexViewModel
        {
            // use the casing from the data so ?category=desserts still reads "Desserts"
            Category = items.FirstOrDefault()?.Category ?? category,
            Items = items,
            // Only show specials on the unfiltered page, otherwise they
            // just repeat whatever's in the grid underneath.
            Specials = string.IsNullOrWhiteSpace(category) ? _menu.GetSpecials() : []
        };

        return View(model);
    }

    public IActionResult Details(int id)
    {
        var item = _menu.GetById(id);
        if (item == null)
            return NotFound();

        var related = _menu.GetByCategory(item.Category)
            .Where(i => i.Id != item.Id)
            .Take(3)
            .ToList();

        return View(new MenuDetailsViewModel { Item = item, Related = related });
    }

    public IActionResult Error() => View();
}
