using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Orders_service.Data;
using Orders_service.Models;
using Orders_service.Utils;

namespace Orders_service.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly Orders_service.Data.Orders_serviceContext _context;

        [BindProperty]
        public Order Order { get; set; } = default!;

        [BindProperty]
        public string? ErrorMessage { get; set; }

        public CreateModel(Orders_service.Data.Orders_serviceContext context)
        {
            _context = context;
        }

        public override PageResult Page()
        {
            this.SetProviders(_context);
            return base.Page();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostCreate()
        {
            if (!ModelState.IsValid || !AreItemsValid())
            {
                return Page();
            }

            _context.Order.Add(Order);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ErrorMessage = "Невозможно создать такой заказ";
                return Page();
            }

            ErrorMessage = null;
            return RedirectToPage("./Index");
        }

        public IActionResult OnPostAddItem()
        {
            if (Order.Items == null)
                Order.Items = new List<OrderItem>();
            Order.Items.Add(new OrderItem() { Quantity = 0 });
            return Page();
        }

        public IActionResult OnPostRemoveItem(int itemIndex)
        {
            if (Order.Items == null)
                return Page();
            Order.Items.RemoveAt(itemIndex);
            return Page();
        }

        private bool AreItemsValid()
        {
            if (Order.Items == null)
                return true;
            foreach (var item in Order.Items)
            {
                if (item.Name == Order.Number)
                {
                    ErrorMessage = "Название элемента не может совпадать с номером заказа";
                    return false;
                }
            }
            return true;
        }
    }
}
