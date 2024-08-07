using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMPG323_Project2_38436272.Models;
using Microsoft.AspNetCore.JsonPatch;
using static CMPG323_Project2_38436272.Models.Client;

namespace CMPG323_Project2_38436272.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTelemetriesController : ControllerBase
    {
        private readonly Cmpg323dbContext _context;


        public JobTelemetriesController(Cmpg323dbContext context)
        {
            _context = context;
        }

        // GET: api/JobTelemetries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobTelemetry>>> GetJobTelemetries()
        {
            return await _context.JobTelemetries.ToListAsync();
        }

        // GET: api/JobTelemetries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobTelemetry>> GetJobTelemetry(int id)
        {
            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);

            if (jobTelemetry == null)
            {
                return NotFound();
            }

            return jobTelemetry;
        }

        // PUT: api/JobTelemetries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobTelemetry(int id, JobTelemetry jobTelemetry)
        {
            if (id != jobTelemetry.Id)
            {
                return BadRequest();
            }

            _context.Entry(jobTelemetry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobTelemetryExists(id))
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

        // POST: api/JobTelemetries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobTelemetry>> PostJobTelemetry(JobTelemetry jobTelemetry)
        {
            _context.JobTelemetries.Add(jobTelemetry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobTelemetry", new { id = jobTelemetry.Id }, jobTelemetry);
        }

        //CODE FOR PATCH
        //PATCH: api/JobTelemetries
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchJobTelemetry(int id, [FromBody] JsonPatchDocument<JobTelemetry> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);
            if (jobTelemetry == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(jobTelemetry, (error) => ModelState.AddModelError(error.AffectedObject?.ToString() ?? string.Empty, error.ErrorMessage));

            // Manually validate the model state after applying the patch
            if (!TryValidateModel(jobTelemetry))
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobTelemetryExists(id))
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

        // DELETE: api/JobTelemetries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobTelemetry(int id)
        {
            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);
            if (jobTelemetry == null)
            {
                return NotFound();
            }

            _context.JobTelemetries.Remove(jobTelemetry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //GetSavingsMethod by project
        // GET: api/JobTelemetries/GetSavingsByProject/{projectId}
        [HttpGet("GetSavingsByProject/{projectId}")]
        public async Task<ActionResult<SavingsResult>> GetSavingsByProject(int projectId, DateTime startDate, DateTime endDate)
        {
            var telemetries = await _context.JobTelemetries
                .Where(t => t.ProjectId == projectId && t.Date >= startDate && t.Date <= endDate)
                .ToListAsync();

            var totalTimeSaved = telemetries.Sum(t => t.TimeSaved);
            var totalCostSaved = telemetries.Sum(t => t.CostSaved);

            var result = new SavingsResult
            {
                TotalTimeSaved = totalTimeSaved,
                TotalCostSaved = totalCostSaved
            };

            return Ok(result);
        }

        //GetSavingsMethod by Client
        // GET: api/JobTelemetries/GetSavingsByClient/{clientId}
        [HttpGet("GetSavingsByClient/{clientId}")]
        public async Task<ActionResult<SavingsResult>> GetSavingsByClient(int clientId, DateTime startDate, DateTime endDate)
        {
            var telemetries = await _context.JobTelemetries
                .Where(t => t.ClientId == clientId && t.Date >= startDate && t.Date <= endDate)
                .ToListAsync();

            var totalTimeSaved = telemetries.Sum(t => t.TimeSaved);
            var totalCostSaved = telemetries.Sum(t => t.CostSaved);

            var result = new SavingsResult
            {
                TotalTimeSaved = totalTimeSaved,
                TotalCostSaved = totalCostSaved
            };

            return Ok(result);
        }



        //PRIVATE METHOD TO CHECK IF TELEMETRY EXISTS
        private bool JobTelemetryExists(int id)
        {
            return _context.JobTelemetries.Any(e => e.Id == id);
        }

    }
}
