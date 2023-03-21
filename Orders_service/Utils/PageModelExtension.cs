using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Orders_service.Data;
using Orders_service.Models;

namespace Orders_service.Utils
{
    public static class PageModelExtension
    {
        public static void SetProviders(this PageModel page, Orders_serviceContext context)
        {
            page.ViewData["Providers"] = new SelectList(context.Provider, nameof(Provider.Id), nameof(Provider.Name));
        }
    }
}
