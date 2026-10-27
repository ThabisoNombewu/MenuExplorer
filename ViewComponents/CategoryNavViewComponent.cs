using Microsoft.AspNetCore.Mvc;
using MenuExplorer.Services;

namespace MenuExplorer.ViewComponents;

// The nav lives in _Layout, which has no model of its own, so a view
// component lets it fetch the category list itself instead of every
// action having to stuff it into ViewBag.
public class CategoryNavViewComponent : ViewComponent
{
    private readonly IMenuService _menu;

    public CategoryNavViewComponent(IMenuService menu)
    {
        _menu = menu;
    }

    public IViewComponentResult Invoke()
    {
        ViewData["Current"] = HttpContext.Request.Query["category"].ToString();
        return View(_menu.GetCategories());
    }
}
