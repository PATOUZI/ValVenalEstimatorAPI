using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ValVenalEstimatorApi.Models;
using ValVenalEstimatorApi.Contracts;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ValVenalEstimatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZonesController : ControllerBase
    {
        private readonly IZoneRepository _iZoneRepository;
        
        public ZonesController(IZoneRepository iZoneRepository)
        {
            _iZoneRepository = iZoneRepository;
        }   

        [HttpPost]
        public async Task<Zone> AddZone(Zone zone)
        {
            await _iZoneRepository.AddZone(zone);
            /*return CreatedAtAction(
                nameof(AddZone),
                new { id = zone.Id },
                zone
            );*/
            return zone;
        }

        [HttpGet("{id}")]
        public async Task<Zone> GetZone(long id)
        {
            return await _iZoneRepository.GetZone(id);
        }

        [HttpGet]
        public async Task<IEnumerable<Zone>> GetAllZones()
        {
            return await _iZoneRepository.GetAllZones();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateZone(long id, Zone zone)
        {
            if (id != zone.Id)
            {
                return BadRequest();
            }

            var z = await _iZoneRepository.GetZone(id);
            if (z == null)
            {
                return NotFound();
            }

            z.Name = zone.Name;

            try
            {
                _iZoneRepository.SaveChange(); 
            }
            catch (DbUpdateConcurrencyException) when (!_iZoneRepository.ZoneExists(id))
            {
                return NotFound();
            }

            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZone(long id)
        {
            var zone = await _iZoneRepository.GetZone(id);

            if (zone == null)
            {
                return NotFound();    
            }     
            _iZoneRepository.Remove(zone);           
            _iZoneRepository.SaveChange();                                   
            return StatusCode(202);          
        }

        [HttpPost("{accessPath}")]
        public void LoadDataInDbByPost(string accessPath)
        {
            _iZoneRepository.LoadDataInDbWithCsvFile(accessPath);
        } 
    }
}