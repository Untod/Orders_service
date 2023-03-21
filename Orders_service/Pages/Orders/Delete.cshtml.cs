using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Orders_service.Data;
using Orders_service.Models;

namespace Orders_service.Pages.Orders
{
    public class DeleteModel : PageModel
    {
        private readonly Orders_service.Data.Orders_serviceContext _context;

        public DeleteModel(Orders_service.Data.Orders_serviceContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order.Include(o => o.Provider).FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }
            else 
            {
                Order = order;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }
            var order = await _context.Order.FindAsync(id);

            if (order != null)
            {
                Order = order;
                _context.Order.Remove(Order);
                var itemIds = Order.Items?.Where(i => i.Id != 0)?.Select(i => i.Id).ToList() ?? new List<int>();
                var itemsToRemove = await _context.OrderItem.Where(i => i.OrderId == Order.Id && !itemIds.Contains(i.Id)).ToArrayAsync();
                foreach (var item in itemsToRemove)
                    _context.OrderItem.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
