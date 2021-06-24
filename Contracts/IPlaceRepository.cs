using ValVenalEstimatorApi.Models; 
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ValVenalEstimatorApi.Contracts
{
    public interface IPlaceRepository
    {
        public Task <Place> AddPlace(Place place);
        public Task<ActionResult<Place>> GetPlace(long id);
        public Task<ActionResult<IEnumerable<Place>>> GetAllPlaces();
        public Task<IActionResult> DeletePlace(long id);
        public void LoadDataInDbWithCsvFile(string accessPath);
        public Task<Place> GetPlaceByIdPrefectureAndDistrict(long IdPrefecture, string dist);
        public Task<ActionResult<IEnumerable<Place>>> GetPlacesByIdPrefecture(long IdPrefecture);
        public Task<ActionResult<IEnumerable<string>>> GetDistrictsByIdPrefecture(long IdPrefecture);
        public void SaveChange();
        public bool PlaceExists(long id);
        public void Remove(Place place);
    }

}