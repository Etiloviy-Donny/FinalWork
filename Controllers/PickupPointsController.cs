using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Data;
using ServiceLayer.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PickupPointsController : ControllerBase
    {
        private readonly ExamContext _context;

        public PickupPointsController(ExamContext context)
        {
            _context = context;
        }

        // GET: api/PickupPoints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PickupPoint>>> GetPickupPoints()
        {
            return await _context.PickupPoints.ToListAsync();
        }

        // GET: api/PickupPoints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PickupPoint>> GetPickupPoint(int id)
        {
            var pickupPoint = await _context.PickupPoints.FindAsync(id);

            if (pickupPoint == null)
            {
                return NotFound();
            }

            return pickupPoint;
        }

        // PUT: api/PickupPoints/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPickupPoint(int id, PickupPoint pickupPoint)
        {
            if (id != pickupPoint.OrderPickupPoint)
            {
                return BadRequest();
            }

            _context.Entry(pickupPoint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PickupPointExists(id))
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

        // POST: api/PickupPoints
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PickupPoint>> PostPickupPoint(PickupPoint pickupPoint)
        {
            _context.PickupPoints.Add(pickupPoint);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPickupPoint", new { id = pickupPoint.OrderPickupPoint }, pickupPoint);
        }

        // DELETE: api/PickupPoints/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePickupPoint(int id)
        {
            var pickupPoint = await _context.PickupPoints.FindAsync(id);
            if (pickupPoint == null)
            {
                return NotFound();
            }

            _context.PickupPoints.Remove(pickupPoint);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PickupPointExists(int id)
        {
            return _context.PickupPoints.Any(e => e.OrderPickupPoint == id);
        }
    }
}
