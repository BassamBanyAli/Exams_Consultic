using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exams_Consultic.Models;

namespace Exams_Consultic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderHeadersController : ControllerBase
    {
        private readonly MyDbContext _context;

        public PurchaseOrderHeadersController(MyDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseOrderHeader>>> GetPurchaseOrderHeaders()
        {
            return await _context.PurchaseOrderHeaders.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseOrderHeader>> GetPurchaseOrderHeader(string id)
        {
            var purchaseOrderHeader = await _context.PurchaseOrderHeaders.FindAsync(id);

            if (purchaseOrderHeader == null)
            {
                return NotFound();
            }

            return purchaseOrderHeader;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseOrderHeader(string id, PurchaseOrderHeader purchaseOrderHeader)
        {
            if (id != purchaseOrderHeader.PurchId)
            {
                return BadRequest();
            }

            _context.Entry(purchaseOrderHeader).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseOrderHeaderExists(id))
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


        [HttpPost]
        public async Task<ActionResult<PurchaseOrderHeader>> PostPurchaseOrderHeader(PurchaseOrderHeader purchaseOrderHeader)
        {
            _context.PurchaseOrderHeaders.Add(purchaseOrderHeader);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PurchaseOrderHeaderExists(purchaseOrderHeader.PurchId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPurchaseOrderHeader", new { id = purchaseOrderHeader.PurchId }, purchaseOrderHeader);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseOrderHeader(string id)
        {
            var purchaseOrderHeader = await _context.PurchaseOrderHeaders.FindAsync(id);
            if (purchaseOrderHeader == null)
            {
                return NotFound();
            }

            _context.PurchaseOrderHeaders.Remove(purchaseOrderHeader);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PurchaseOrderHeaderExists(string id)
        {
            return _context.PurchaseOrderHeaders.Any(e => e.PurchId == id);
        }
    }
}
