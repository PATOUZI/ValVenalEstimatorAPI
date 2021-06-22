using System.Threading.Tasks;
using ValVenalEstimatorApi.Models;
using ValVenalEstimatorApi.ViewModels;
using ValVenalEstimatorApi.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using System.IO;
using System.Globalization;
using ValVenalEstimatorApi.Contracts;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ValVenalEstimatorApi.Repositories             
{
    public class PlaceRepository : IPlaceRepository 
    {
        readonly ValVenalEstimatorDbContext _valVenalEstDbContext;        
        public PlaceRepository(ValVenalEstimatorDbContext context)
        {  
            _valVenalEstDbContext = context;   
        } 
        public async Task<Place> AddPlace(Place place)
        {
            _valVenalEstDbContext.Add(place);
            await _valVenalEstDbContext.SaveChangesAsync();
            return place;
        }
        public async Task<ActionResult<Place>> GetPlace(long id)
        {
            var place = await _valVenalEstDbContext.Places.FindAsync(id);

            if (place == null)
            {
                return null;
            }
            return place;
        }
        public async Task<ActionResult<IEnumerable<Place>>> GetAllPlaces()
        {
            return await _valVenalEstDbContext.Places.ToListAsync();
        } 
        public async Task<ActionResult<IEnumerable<Place>>> GetPlacesByPrefecture(string prefecture)
        {
            return await _valVenalEstDbContext.Places
                            .Where(o => o.Prefecture == prefecture)
                            .ToListAsync();
        }   
        public async Task<ActionResult<IEnumerable<string>>> GetDistrictByPrefecture(string prefecture)
        {
            return await _valVenalEstDbContext.Places
                            .Where(o => o.Prefecture == prefecture)
                            .Select(o => o.District)
                            .Distinct()
                            .OrderBy(o => o)
                            .ToListAsync();
        } 
        public async Task<ActionResult<IEnumerable<string>>> GetPrefectures()
        {
            return await _valVenalEstDbContext.Places
                            .Select(o => o.Prefecture)
                            .Distinct()
                            .OrderBy(o => o)
                            .ToListAsync();
        }               
        public async Task<IActionResult> DeletePlace(long id)
        {
            var place = await _valVenalEstDbContext.Places.FindAsync(id);

            if (place == null)
            {
                //return NotFound();
                return null;     
            }
     
            _valVenalEstDbContext.Places.Remove(place);                                      
            await _valVenalEstDbContext.SaveChangesAsync();

            //return NoContent();
            return null;
        }

        public async void LoadDataInDbWithCsvFile(string accessPath)
        {
            using (var reader = new StreamReader(accessPath))   
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<PlaceDTO>();
                foreach (var p in records)
                {
                    Place place = new Place();
                    place.Prefecture = p.Prefecture;
                    place.District = p.District;
                    place.PricePerMeterSquare = p.PricePerMeterSquare;   
                    _valVenalEstDbContext.Add<Place>(place);               
                    await _valVenalEstDbContext.SaveChangesAsync();
                    //AddPlace(place);
                }
            }
        }
        public async Task<Place> GetPlaceByPrefectureAndDistrict(string pref, string dist)
        {
            return await _valVenalEstDbContext.Places
                                        .Where(place => place.Prefecture == pref  && place.District == dist)
                                        .FirstOrDefaultAsync();    
        }         
        public async void SaveChange()
        {
            await _valVenalEstDbContext.SaveChangesAsync();
        }
        public bool PlaceExists(long id) =>
            _valVenalEstDbContext.Places.Any(p => p.Id == id);
        
        public void Remove(Place place)
        {
            _valVenalEstDbContext.Places.Remove(place);   
        }
    }
}                             