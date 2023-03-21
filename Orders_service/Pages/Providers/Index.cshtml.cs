using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Orders_service.Data;
using Orders_service.Models;

namespace Orders_service.Pages.Providers
{
    public class IndexModel : PageModel
    {
        private readonly Orders_service.Data.Orders_serviceContext _context;

        public IndexModel(Orders_service.Data.Orders_serviceContext context)
        {
            _context = context;
        }

        public IList<Provider> Provider { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Provider != null)
            {
                Provider = await _context.Provider.ToListAsync();
            }
        }
    }
}
