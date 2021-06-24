using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ValVenalEstimatorApi.Models;
using ValVenalEstimatorApi.Contracts;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ValVenalEstimatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrefecturesController : ControllerBase
    {
        private readonly IPrefectureRepository _iPrefectureRepository;
        
        public PrefecturesController(IPrefectureRepository iPrefectureRepository)
        {
            _iPrefectureRepository = iPrefectureRepository;
        }   

        [HttpPost]
        public async Task<ActionResult<Prefecture>> AddPrefecture(Prefecture prefecture)
        {
            await _iPrefectureRepository.AddPrefecture(prefecture);
            return CreatedAtAction(
                nameof(AddPrefecture),
                new { id = prefecture.Id },
                prefecture
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Prefecture>> GetPrefecture(long id)
        {
            return await _iPrefectureRepository.GetPrefecture(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prefecture>>> GetAllPrefectures()
        {
            return await _iPrefectureRepository.GetAllPrefectures();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrefecture(long id, Prefecture prefecture)
        {
            if (id != prefecture.Id)
            {
                return BadRequest();
            }

            var p = await _iPrefectureRepository.GetPrefecture(id);
            if (p == null)
            {
                return NotFound();
            }

            p.Value.Name = prefecture.Name;

            try
            {
                _iPrefectureRepository.SaveChange(); 
            }
            catch (DbUpdateConcurrencyException) when (!_iPrefectureRepository.PrefectureExists(id))
            {
                return NotFound();
            }

            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrefecture(long id)
        {
            var prefecture = await _iPrefectureRepository.GetPrefecture(id);

            if (prefecture == null)
            {
                return NotFound();    
            }     
            _iPrefectureRepository.Remove(prefecture.Value);           
            _iPrefectureRepository.SaveChange();                                   
            return StatusCode(202);          
        }
    }
}