using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Orders_service.Data;
using Orders_service.Models;

namespace Orders_service.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly Orders_service.Data.Orders_serviceContext _context;

        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime FromDateFilter { get; set; } = DateTime.Today.AddMonths(-1);

        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime ToDateFilter { get; set; } = DateTime.Today;

        [BindProperty]
        public int ProviderId { get; set; }

        public IList<Order> Orders { get; set; } = default!;

        public IndexModel(Orders_service.Data.Orders_serviceContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            SetProviders();
            await GetOrdersFiltered();
        }

        public async Task OnPostAsync()
        {
            SetProviders();
            await GetOrdersFiltered();
        }

        private async Task GetOrdersFiltered()
        {
            if (_context.Order == null)
                return;
            var ordersQuery = from x in _context.Order select x;
            // add filters
                ordersQuery = ordersQuery.Where(x => x.Date >= FromDateFilter);
                ordersQuery = ordersQuery.Where(x => x.Date <= ToDateFilter.AddDays(1).AddSeconds(-1));
            if (ProviderId > 0)
                ordersQuery = ordersQuery.Where(x => x.ProviderId == ProviderId);
            // execute query
            Orders = await ordersQuery.Include(o => o.Provider).ToListAsync();
        }

        private void SetProviders()
        {
            if (_context.Provider == null)
                return;
            var providers = new List<Provider>() { new Provider() { Id = 0, Name = "All" } };
            providers.AddRange(_context.Provider);
            ViewData["Providers"] = new SelectList(providers, nameof(Provider.Id), nameof(Provider.Name));
        }
    }
}
