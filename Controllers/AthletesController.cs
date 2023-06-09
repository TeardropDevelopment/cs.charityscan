﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CharityScanWebApp.Entities;

namespace cs.api.charityscan.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AthletesController : ControllerBase
    {
        private readonly CharityscanDevContext _context;

        public AthletesController(CharityscanDevContext context)
        {
            _context = context;
        }

        #region athlete

        // GET: api/Athletes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Athlete>>> GetAthletes()
        {
          if (_context.Athletes == null)
          {
              return NotFound();
          }
            return await _context.Athletes.ToListAsync();
        }

        // GET: api/Athletes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Athlete>> GetAthlete(int id)
        {
          if (_context.Athletes == null)
          {
              return NotFound();
          }
            var athlete = await _context.Athletes.FindAsync(id);

            if (athlete == null)
            {
                return NotFound();
            }
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return StatusCode(StatusCodes.Status200OK, athlete);
        }

        // PUT: api/Athletes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAthlete(int id, Athlete athlete)
        {
            if (id != athlete.Id)
            {
                return BadRequest();
            }

            _context.Entry(athlete).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AthleteExists(id))
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

        // POST: api/Athletes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Athlete>> PostAthlete(Athlete athlete)
        {
          if (_context.Athletes == null)
          {
              return Problem("Entity set 'CharityscanDevContext.Athletes'  is null.");
          }
            _context.Athletes.Add(athlete);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAthlete", new { id = athlete.Id }, athlete);
        }

        // DELETE: api/Athletes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAthlete(int id)
        {
            if (_context.Athletes == null)
            {
                return NotFound();
            }
            var athlete = await _context.Athletes.FindAsync(id);
            if (athlete == null)
            {
                return NotFound();
            }

            _context.Athletes.Remove(athlete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region code

        [HttpGet("{id}/code")]
        public async Task<ActionResult<Code>> GetAthletesCode(int id)
        {
            var code = await _context.Codes.FirstOrDefaultAsync(x => x.AthleteId == id);

            if(code == null)
                return NotFound();

            return StatusCode(StatusCodes.Status200OK, code);
        }

        #endregion

        private bool AthleteExists(int id)
        {
            return (_context.Athletes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
