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
    public class IndexModel : PageModel
    {
        private readonly Orders_service.Data.Orders_serviceContext _context;

        public IndexModel(Orders_service.Data.Orders_serviceContext context)
        {
            _context = context;
        }

        public IList<OrderItem> OrderItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.OrderItem != null)
            {
                OrderItem = await _context.OrderItem
                .Include(o => o.Order).ToListAsync();
            }
        }
    }
}
