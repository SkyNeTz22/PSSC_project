using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSSCProject.Models;

namespace PSSCProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsInfoesController : ControllerBase
    {
        private readonly ContextForProducts _context;

        public ProductsInfoesController(ContextForProducts context)
        {
            _context = context;
        }

        // GET: api/ProductsInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductsInfo>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/ProductsInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductsInfo>> GetProductsInfo(decimal id)
        {
            var productsInfo = await _context.Products.FindAsync(id);

            if (productsInfo == null)
            {
                return NotFound();
            }

            return productsInfo;
        }

        // PUT: api/ProductsInfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductsInfo(decimal id, ProductsInfo productsInfo)
        {
            if (id != productsInfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(productsInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsInfoExists(id))
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

        // POST: api/ProductsInfoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductsInfo>> PostProductsInfo(ProductsInfo productsInfo)
        {
            _context.Products.Add(productsInfo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductsInfoExists(productsInfo.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetProductsInfo), new { id = productsInfo.Id }, productsInfo);
        }

        // DELETE: api/ProductsInfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductsInfo(decimal id)
        {
            var productsInfo = await _context.Products.FindAsync(id);
            if (productsInfo == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productsInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductsInfoExists(decimal id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
