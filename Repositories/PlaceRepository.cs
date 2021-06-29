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
        readonly IPrefectureRepository _iPrefectureRepository;

        public PlaceRepository(ValVenalEstimatorDbContext context, IPrefectureRepository iPrefRepository)
        {  
            _valVenalEstDbContext = context; 
            _iPrefectureRepository = iPrefRepository;  
        } 
        public async Task<Place> AddPlace(Place place)
        {
            var existingPrefecture = _iPrefectureRepository.PrefectureExists(place.PrefectureId);
            if (existingPrefecture == true)
            {
                _valVenalEstDbContext.Add(place);
                await _valVenalEstDbContext.SaveChangesAsync();
                return place;
            } 
            else
            {
                return null;
            }        
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
            return await _valVenalEstDbContext.Places.Include(p => p.Prefecture).ToListAsync();
        }                 
        public async Task<IActionResult> DeletePlace(long id)
        {
            var place = await _valVenalEstDbContext.Places.FindAsync(id);

            if (place == null)
            {
                return null;     
            }     
            _valVenalEstDbContext.Places.Remove(place);                                      
            await _valVenalEstDbContext.SaveChangesAsync();
            return null;
        }
        public async Task<ActionResult<IEnumerable<string>>> GetDistrictsByIdPrefecture(long IdPrefecture)
        {
            return await _valVenalEstDbContext.Places
                            .Where(o => o.PrefectureId == IdPrefecture)
                            .Select(o => o.District)
                            .Distinct()
                            .OrderBy(o => o)
                            .ToListAsync();
        }
        public async Task<Place> GetPlaceByIdPrefectureAndDistrict(long IdPrefecture, string dist)
        {
            return await _valVenalEstDbContext.Places
                                        .Where(place => place.PrefectureId == IdPrefecture && place.District == dist)
                                        .FirstOrDefaultAsync();    
        }
        public async Task<ActionResult<IEnumerable<Place>>> GetPlacesByIdPrefecture(long IdPrefecture)
        {
             return await _valVenalEstDbContext.Places
                            .Where(o => o.PrefectureId == IdPrefecture)
                            .ToListAsync();
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
                    place.District = p.District;
                    place.PricePerMeterSquare = p.PricePerMeterSquare;   
                    place.PrefectureId = p.PrefectureId;
                    _valVenalEstDbContext.Add<Place>(place);               
                    await _valVenalEstDbContext.SaveChangesAsync();
                    //AddPlace(place);
                }
            }
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