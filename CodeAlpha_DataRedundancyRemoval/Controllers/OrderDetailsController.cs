using CodeAlpha_DataRedundancyRemoval.Data;
using CodeAlpha_DataRedundancyRemoval.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CodeAlpha_DataRedundancyRemoval.Controllers
{
    // Manages individual line items (products) attached to an Order.
    // Kept separate from OrdersController to reflect the normalized
    // one-to-many relationship between Order and OrderDetail.
    public class OrderDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderDetails/Create?orderId=5
        public async Task<IActionResult> Create(int? orderId)
        {
            if (orderId == null) return NotFound();

            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return NotFound();

            ViewData["OrderId"] = order.Id;
            ViewData["ProductId"] = new SelectList(_context.Products.OrderBy(p => p.Name), "Id", "Name");
            return View(new OrderDetail { OrderId = order.Id, Quantity = 1 });
        }

        // POST: OrderDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderId,ProductId,Quantity,UnitPrice")] OrderDetail orderDetail)
        {
            if (orderDetail.UnitPrice <= 0)
            {
                var product = await _context.Products.FindAsync(orderDetail.ProductId);
                if (product != null) orderDetail.UnitPrice = product.Price;
            }

            if (ModelState.IsValid)
            {
                _context.Add(orderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Orders", new { id = orderDetail.OrderId });
            }

            ViewData["OrderId"] = orderDetail.OrderId;
            ViewData["ProductId"] = new SelectList(_context.Products.OrderBy(p => p.Name), "Id", "Name", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null) return NotFound();

            ViewData["ProductId"] = new SelectList(_context.Products.OrderBy(p => p.Name), "Id", "Name", orderDetail.ProductId);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderId,ProductId,Quantity,UnitPrice")] OrderDetail orderDetail)
        {
            if (id != orderDetail.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.OrderDetails.Any(e => e.Id == orderDetail.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction("Details", "Orders", new { id = orderDetail.OrderId });
            }

            ViewData["ProductId"] = new SelectList(_context.Products.OrderBy(p => p.Name), "Id", "Name", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var orderDetail = await _context.OrderDetails
                .Include(od => od.Product)
                .Include(od => od.Order)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (orderDetail == null) return NotFound();

            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            int orderId = orderDetail?.OrderId ?? 0;

            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "Orders", new { id = orderId });
        }
    }
}
