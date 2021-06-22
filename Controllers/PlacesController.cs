using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ValVenalEstimatorApi.Models;
using ValVenalEstimatorApi.Contracts;
using ValVenalEstimatorApi.ViewModels;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ValVenalEstimatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly IPlaceRepository _iPlaceRepository;
        public PlacesController(IPlaceRepository iPlaceRepository)
        {
            _iPlaceRepository = iPlaceRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Place>> AddPlace(Place place)
        {
            await _iPlaceRepository.AddPlace(place);
            return CreatedAtAction(
                nameof(AddPlace),
                new { id = place.Id },
                place
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Place>> GetPlace(long id)
        {
            return await _iPlaceRepository.GetPlace(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Place>>> GetAllPlaces()
        {
            return await _iPlaceRepository.GetAllPlaces();
        }

        [HttpGet ("prefecture/{prefecture}")]
        public async Task<ActionResult<IEnumerable<Place>>> GetPlacesByPrefecture(string prefecture)
        {
            return await _iPlaceRepository.GetPlacesByPrefecture(prefecture);
        }

        [HttpGet ("district/{prefecture}")]
        public async Task<ActionResult<IEnumerable<string>>> GetDistrictByPrefecture(string prefecture)
        {
           return await _iPlaceRepository.GetDistrictByPrefecture(prefecture);
        }
        
        [HttpGet ("prefecture")]
        public async Task<ActionResult<IEnumerable<string>>> GetPrefectures()
        {
           return await _iPlaceRepository.GetPrefectures();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlace(long id, Place place)
        {
            if (id != place.Id)
            {
                return BadRequest();
            }

            var p = await _iPlaceRepository.GetPlace(id);
            if (p == null)
            {
                return NotFound();
            }

            p.Value.Prefecture = place.Prefecture;
            p.Value.District = place.District;
            p.Value.PricePerMeterSquare = place.PricePerMeterSquare;

            try
            {
                _iPlaceRepository.SaveChange(); 
            }
            catch (DbUpdateConcurrencyException) when (!_iPlaceRepository.PlaceExists(id))
            {
                return NotFound();
            }

            return StatusCode(200);
            //return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlace(long id)
        {
            var place = await _iPlaceRepository.GetPlace(id);

            if (place == null)
            {
                return NotFound();    
            }
     
            _iPlaceRepository.Remove(place.Value);
            //_valVenalEstDbContext.Places.Remove(place);   
            
            _iPlaceRepository.SaveChange();                                   
            //await _valVenalEstDbContext.SaveChangesAsync();

            return StatusCode(202);
            //return NoContent();
            
            //return await _iPlaceRepository.DeletePlace(id);
        }
    
        [HttpPost("{accessPath}", Name = "LoadDataInDbByPost")]
        public void LoadDataInDbByPost(string accessPath)
        {
            _iPlaceRepository.LoadDataInDbWithCsvFile(accessPath);
        }                     

        [HttpPost("LoadDataInDataBase")]      
        public void Load(string accessPath)
        {
            _iPlaceRepository.LoadDataInDbWithCsvFile(accessPath);
        }                                     

        /*[HttpGet("Load/{accessPath}")]
        public void LoadDatas(string accessPath)
        {
            _iPlaceRepository.LoadDataInDbWithCsvFile(accessPath);
        }*/           

        /*[HttpGet("LoadDataInDb")]
        public IActionResult LoadDataInDb(string accessPath)
        {
            _iPlaceRepository.LoadDataInDbWithCsvFile(accessPath);
            return Ok(GetAllPlaces());
        }*/    

        [HttpGet("{pref}/{dist}/{area}", Name = "GetValVenal")]
        public async Task<ActionResult<ValVenalDTO>> GetValVenal(string pref, string dist, int area)
        {
            var localite = await _iPlaceRepository.GetPlaceByPrefectureAndDistrict(pref, dist);
            if (localite == null)
            {
                return NotFound();
            }
            ValVenalDTO venaleValue = new ValVenalDTO();
            venaleValue.ValVenal = localite.PricePerMeterSquare * area;
            return venaleValue;
        }   

    }
}
