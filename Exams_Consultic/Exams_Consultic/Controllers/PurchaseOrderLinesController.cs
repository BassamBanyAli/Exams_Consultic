using Exams_Consultic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exams_Consultic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderLinesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public PurchaseOrderLinesController(MyDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseOrderLine>>> GetPurchaseOrderLines()
        {
            return await _context.PurchaseOrderLines.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseOrderLine>> GetPurchaseOrderLine(string id)
        {
            var purchaseOrderLine = await _context.PurchaseOrderLines.FindAsync(id);

            if (purchaseOrderLine == null)
            {
                return NotFound();
            }

            return purchaseOrderLine;
        }


        [HttpPost]
        public async Task<ActionResult<PurchaseOrderLine>> PostPurchaseOrderLine(PurchaseOrderLine purchaseOrderLine)
        {

            if (purchaseOrderLine == null)
            {
                return BadRequest();
            }

            _context.PurchaseOrderLines.Add(purchaseOrderLine);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPurchaseOrderLine), new { id = purchaseOrderLine.PurchId }, purchaseOrderLine);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseOrderLine(string id, PurchaseOrderLine purchaseOrderLine)
        {
            if (id != purchaseOrderLine.PurchId)
            {
                return BadRequest();
            }

            _context.Entry(purchaseOrderLine).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseOrderLineExists(id))
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


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseOrderLine(string id)
        {
            var purchaseOrderLine = await _context.PurchaseOrderLines.FindAsync(id);
            if (purchaseOrderLine == null)
            {
                return NotFound();
            }

            _context.PurchaseOrderLines.Remove(purchaseOrderLine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PurchaseOrderLineExists(string id)
        {
            return _context.PurchaseOrderLines.Any(e => e.PurchId == id);
        }
    }
}

