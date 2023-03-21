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
    public class DetailsModel : PageModel
    {
        private readonly Orders_service.Data.Orders_serviceContext _context;

        public DetailsModel(Orders_service.Data.Orders_serviceContext context)
        {
            _context = context;
        }

      public Provider Provider { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Provider == null)
            {
                return NotFound();
            }

            var provider = await _context.Provider.FirstOrDefaultAsync(m => m.Id == id);
            if (provider == null)
            {
                return NotFound();
            }
            else 
            {
                Provider = provider;
            }
            return Page();
        }
    }
}
