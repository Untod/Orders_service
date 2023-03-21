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
    public class DeleteModel : PageModel
    {
        private readonly Orders_service.Data.Orders_serviceContext _context;

        public DeleteModel(Orders_service.Data.Orders_serviceContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Provider == null)
            {
                return NotFound();
            }
            var provider = await _context.Provider.FindAsync(id);

            if (provider != null)
            {
                Provider = provider;
                _context.Provider.Remove(Provider);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
