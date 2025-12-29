using Microsoft.AspNetCore.Mvc;

namespace LioTecnica.Web.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index() => View();
}
