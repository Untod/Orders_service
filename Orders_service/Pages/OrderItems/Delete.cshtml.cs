using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Orders_service.Data;
using Orders_service.Models;

namespace Orders_service.Pages.OrderItems
{
    public class DeleteModel : PageModel
    {
        private readonly Orders_service.Data.Orders_serviceContext _context;

        public DeleteModel(Orders_service.Data.Orders_serviceContext context)
        {
            _context = context;
        }

        [BindProperty]
      public OrderItem OrderItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.OrderItem == null)
            {
                return NotFound();
            }

            var orderitem = await _context.OrderItem.FirstOrDefaultAsync(m => m.Id == id);

            if (orderitem == null)
            {
                return NotFound();
            }
            else 
            {
                OrderItem = orderitem;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.OrderItem == null)
            {
                return NotFound();
            }
            var orderitem = await _context.OrderItem.FindAsync(id);

            if (orderitem != null)
            {
                OrderItem = orderitem;
                _context.OrderItem.Remove(OrderItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
