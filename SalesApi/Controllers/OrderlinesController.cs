using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models;

namespace SalesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderlinesController : ControllerBase
    {
        private readonly SalesDbContext _context;

        public OrderlinesController(SalesDbContext context)
        {
            _context = context;
        }

        // This is using Linq: 
        private async Task<IActionResult> RecalculateOrderTotal(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            order.Total = (from ol in _context.Orderlines
                           join i in _context.Items
                           on ol.ItemId equals i.Id
                           where ol.OrderId == orderId
                           select new
                           {
                               lineTotal = ol.Quantity * i.Price
                           }).Sum(x => x.lineTotal);
            await _context.SaveChangesAsync();
            return Ok();
        }
        
        //// **THIS IS PRIVATE, so it cannot be called from Postman**
        private async Task<IActionResult> XRecalculateOrderTotal(int orderId)
        {   // read the order to be updated
            var order = await _context.Orders.FindAsync(orderId);
            //check if the order is found
            if(order is null)
            {
                return NotFound();
            }
            // get all the orderlines for the order
            var orderlines = await _context.Orderlines                        
                                                .Include(x => x.Item)
                                                .Include(x => x.OrderId == orderId)
                                                .ToListAsync();
            
            // create a collection to store the product of quantity * price
            /*List<decimal> lineTotals = new List<decimal>();*/
            // and sum the linetotal to get the grand total
            decimal grandTotal = 0m;
            foreach(var ol in orderlines)
            {
                var lineTotal = ol.Quantity * ol.Item.Price;
                grandTotal += lineTotal;  
            }
            // update the order.Total with the grandTotal
            order.Total = grandTotal;
            var changed = await _context.SaveChangesAsync();
            // if change failed throw exception
            if(changed != 1)
            {
                throw new Exception("Recalculate failed!");
            }
            return Ok();
        }

        // GET: api/Orderlines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orderline>>> GetOrderlines()
        {
            return await _context.Orderlines.ToListAsync();
        }

        // GET: api/Orderlines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orderline>> GetOrderline(int id)
        {
            var orderline = await _context.Orderlines.FindAsync(id);

            if (orderline == null)
            {
                return NotFound();
            }

            return orderline;
        }

        // PUT: api/Orderlines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderline(int id, Orderline orderline)
        {
            if (id != orderline.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderline).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                //adding after private prop was added:
                await RecalculateOrderTotal(orderline.OrderId);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderlineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orderlines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Orderline>> PostOrderline(Orderline orderline)
        {
            _context.Orderlines.Add(orderline);
            await _context.SaveChangesAsync();
            await RecalculateOrderTotal(orderline.OrderId);


            return CreatedAtAction("GetOrderline", new { id = orderline.Id }, orderline);
        }
       

        // DELETE: api/Orderlines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderline(int id)
        {
            var orderline = await _context.Orderlines.FindAsync(id);
            if (orderline == null)
            {
                return NotFound();
            }
            var orderId = orderline.OrderId;

            _context.Orderlines.Remove(orderline);
            await _context.SaveChangesAsync();
            await RecalculateOrderTotal(orderId);

            return NoContent();
        }

        private bool OrderlineExists(int id)
        {
            return _context.Orderlines.Any(e => e.Id == id);
        }
    }
}
