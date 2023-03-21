using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Orders_service.Data;
using Orders_service.Models;

namespace Orders_service.Pages.OrderItems
{
    public class CreateModel : PageModel
    {
        private readonly Orders_service.Data.Orders_serviceContext _context;

        public CreateModel(Orders_service.Data.Orders_serviceContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["OrderId"] = new SelectList(_context.Order, "Id", "Number");
            return Page();
        }

        [BindProperty]
        public OrderItem OrderItem { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.OrderItem.Add(OrderItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
