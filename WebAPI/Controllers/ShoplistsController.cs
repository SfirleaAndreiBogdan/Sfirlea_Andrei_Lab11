using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoplistsController : ControllerBase
    {
        private readonly WebAPIContext _context;

        public ShoplistsController(WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/Shoplists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shoplist>>> GetShoplist()
        {
            return await _context.Shoplist.ToListAsync();
        }

        // GET: api/Shoplists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shoplist>> GetShoplist(int id)
        {
            var shoplist = await _context.Shoplist.FindAsync(id);

            if (shoplist == null)
            {
                return NotFound();
            }

            return shoplist;
        }

        // PUT: api/Shoplists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoplist(int id, Shoplist shoplist)
        {
            if (id != shoplist.ID)
            {
                return BadRequest();
            }

            _context.Entry(shoplist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoplistExists(id))
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

        // POST: api/Shoplists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shoplist>> PostShoplist(Shoplist shoplist)
        {
            _context.Shoplist.Add(shoplist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoplist", new { id = shoplist.ID }, shoplist);
        }

        // DELETE: api/Shoplists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoplist(int id)
        {
            var shoplist = await _context.Shoplist.FindAsync(id);
            if (shoplist == null)
            {
                return NotFound();
            }

            _context.Shoplist.Remove(shoplist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShoplistExists(int id)
        {
            return _context.Shoplist.Any(e => e.ID == id);
        }
    }
}
