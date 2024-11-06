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
    public class CurrenciesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CurrenciesController(MyDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> GetCurrencies()
        {
            return await _context.Currencies.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Currency>> GetCurrency(string id)
        {
            var currency = await _context.Currencies.FindAsync(id);

            if (currency == null)
            {
                return NotFound();
            }

            return currency;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurrency(string id, Currency currency)
        {
            if (id != currency.CurrencyCode)
            {
                return BadRequest();
            }

            _context.Entry(currency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyExists(id))
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
        public async Task<ActionResult<Currency>> PostCurrency(Currency currency)
        {
            _context.Currencies.Add(currency);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CurrencyExists(currency.CurrencyCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCurrency", new { id = currency.CurrencyCode }, currency);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrency(string id)
        {
            var currency = await _context.Currencies.FindAsync(id);
            if (currency == null)
            {
                return NotFound();
            }

            _context.Currencies.Remove(currency);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CurrencyExists(string id)
        {
            return _context.Currencies.Any(e => e.CurrencyCode == id);
        }
    }
}
