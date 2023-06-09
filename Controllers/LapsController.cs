﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CharityScanWebApp.Entities;
using CharityScanWebApp.Data;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Logging;

namespace cs.api.charityscan.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LapsController : ControllerBase
    {
        private readonly CharityscanDevContext _context;

        public LapsController(CharityscanDevContext context)
        {
            _context = context;
        }

        // GET: api/Laps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lap>>> GetLaps()
        {
          if (_context.Laps == null)
          {
              return NotFound();
          }
            return await _context.Laps.ToListAsync();
        }

        // GET: api/Laps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lap>> GetLap(int id)
        {
          if (_context.Laps == null)
          {
              return NotFound();
          }
            var lap = await _context.Laps.FindAsync(id);

            if (lap == null)
            {
                return NotFound();
            }

            return lap;
        }

        // PUT: api/Laps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLap(int id, Lap lap)
        {
            if (id != lap.Id)
            {
                return BadRequest();
            }

            _context.Entry(lap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LapExists(id))
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

        // POST: api/Laps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lap>> PostLap(int event_id, string value, int? starter_nr = null)
        {
          if (_context.Laps == null)
          {
              return Problem("Entity set 'CharityscanDevContext.Laps'  is null.");
          }
            _context.Laps.Add(new Lap
            {
                EventId = event_id,
                StarterNr = starter_nr,
                Value = value
            });
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created);
        }

        // POST: api/Laps/bulk
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("bulk")]
        public async Task<ActionResult> PostLap([FromBody] List<OfflineLap> laps)
        {
            if (_context.Laps == null)
            {
                return Problem("Entity set 'CharityscanDevContext.Laps'  is null.");
            }

            foreach (var lap in laps)
            {
                _context.Laps.Add(new Lap
                {
                    EventId = lap.EventId,
                    StarterNr = lap.StartNr,
                    Value = lap.BarCodeValue ?? "UNKNOWN"
                });
            }

            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created);
        }

        // DELETE: api/Laps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLap(int id)
        {
            if (_context.Laps == null)
            {
                return NotFound();
            }
            var lap = await _context.Laps.FindAsync(id);
            if (lap == null)
            {
                return NotFound();
            }

            _context.Laps.Remove(lap);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LapExists(int id)
        {
            return (_context.Laps?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
