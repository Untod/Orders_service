using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Orders_service.Data;
using Orders_service.Models;
using Orders_service.Utils;

namespace Orders_service.Pages.Orders
{
    public class EditModel : PageModel
    {
        private readonly Orders_service.Data.Orders_serviceContext _context;

        [BindProperty]
        public Order Order { get; set; } = default!;

        [BindProperty]
        public string? ErrorMessage { get; set; }

        public EditModel(Orders_service.Data.Orders_serviceContext context)
        {
            _context = context;
        }

        public override PageResult Page()
        {
            this.SetProviders(_context);
            return base.Page();
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order =  await _context.Order.Include(o => o.Items).FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            Order = order;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostSave()
        {
            if (!ModelState.IsValid || !AreItemsValid())
            {
                return Page();
            }

            _context.Attach(Order).State = EntityState.Modified;
            var itemIds = Order.Items?.Where(i => i.Id != 0)?.Select(i => i.Id).ToList() ?? new List<int>();
            var itemsToRemove = await _context.OrderItem.Where(i => i.OrderId == Order.Id && !itemIds.Contains(i.Id)).ToArrayAsync();
            foreach (var item in itemsToRemove)
                _context.OrderItem.Remove(item);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(Order.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (DbUpdateException)
            {
                ErrorMessage = "Невозможно сохранить текущие изменения";
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
            var item = Order.Items[itemIndex];
            Order.Items.Remove(item);
            return Page();
        }

        private bool OrderExists(int id)
        {
          return _context.Order.Any(e => e.Id == id);
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
