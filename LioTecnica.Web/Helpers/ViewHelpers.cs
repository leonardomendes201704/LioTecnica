using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace LioTecnica.Web.Helpers;

public static class ViewHelpers
{
    public static string NavActive(ViewContext viewContext, string controller, string activeClass = "active")
    {
        var current = viewContext.RouteData.Values["controller"]?.ToString() ?? "";
        return current.Equals(controller, StringComparison.OrdinalIgnoreCase) ? activeClass : "";
    }

    public static string NavMobileActive(ViewContext viewContext, string controller, string activeClass = "fw-semibold")
    {
        var current = viewContext.RouteData.Values["controller"]?.ToString() ?? "";
        return current.Equals(controller, StringComparison.OrdinalIgnoreCase) ? activeClass : "";
    }

    public static string SidebarSubtitle(ViewDataDictionary viewData, string fallback = "Recrutamento & Match")
    {
        return viewData["SidebarSubtitle"]?.ToString() ?? fallback;
    }
}
