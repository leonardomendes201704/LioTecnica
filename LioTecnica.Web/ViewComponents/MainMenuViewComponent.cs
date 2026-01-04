using LioTecnica.Web.Infrastructure.ApiClients;
using LioTecnica.Web.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;

namespace LioTecnica.Web.ViewComponents;

public sealed class MainMenuViewComponent : ViewComponent
{
    private readonly MenusApiClient _menusApi;

    public MainMenuViewComponent(MenusApiClient menusApi)
    {
        _menusApi = menusApi;
    }

    public async Task<IViewComponentResult> InvokeAsync(string linkClass = "nav-link")
    {
        var menus = await _menusApi.ListForCurrentUserAsync(HttpContext.RequestAborted);
        ViewData["LinkClass"] = linkClass;
        return View(menus.OrderBy(x => x.Order).ToList());
    }
}
