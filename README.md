# Menu Explorer

A small ASP.NET Core MVC app for browsing a (made up) restaurant menu.

## Running it

You need the .NET 8 SDK.

```
dotnet run
```

Then open the URL it prints (http://localhost:5043 by default). In Visual Studio or Rider just open `MenuExplorer.csproj` and hit run.

## How it's put together

- The menu lives in `Data/menu.json` and is loaded once by `JsonMenuService`, registered as a singleton. Controllers only talk to `IMenuService`, so swapping the JSON for a database later wouldn't touch them.
- `MenuController` has `Index` (with an optional `?category=` filter, case-insensitive) and `Details/{id}`, which returns a 404 for unknown ids.
- `Views/Shared/_MenuItemCard.cshtml` is the card partial. It's used for the main grid, the "Today's specials" block on the index page, and the "More from this category" row on the details page.
- The category nav is a view component (`CategoryNavViewComponent`) called from `_Layout.cshtml`. I went with a view component over a plain partial because the layout doesn't have a model, and this way it can get the categories from the service itself rather than every action passing them through ViewBag.
- `<star-rating value="4" />` is a custom tag helper in `TagHelpers/`.
- There's a small search box on the index page (`wwwroot/js/site.js`) that filters the visible cards as you type, plus empty states for an unknown category and for a search with no matches.

Specials are just the three highest rated non-drink items, since the model doesn't have a "special" flag and I didn't want to add fields beyond the spec.

## Styling

Plain CSS in `wwwroot/css/site.css`, with no framework. Colours are CSS variables at the top of the file. The values come from the colour tokens in King Price's own stylesheet on kingprice.co.za: primary red `#B10F1F`, red shade `#8B010E`, red tint `#F8EAEB`, golden yellow `#E1C06E` and their grey scale.
