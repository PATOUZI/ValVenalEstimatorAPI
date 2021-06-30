using ValVenalEstimatorApi.Models; 
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ValVenalEstimatorApi.Contracts
{
    public interface IPlaceRepository
    {
        public Task<Place> AddPlace(Place place);
        public Task<Place> GetPlace(long id);
        public Task<IEnumerable<Place>> GetAllPlaces();
        public Task DeletePlace(long id);
        public void LoadDataInDbWithCsvFile(string accessPath);
        public Task<IEnumerable<Place>> GetPlacesByPrefectureId(long IdPrefecture);
        public Task<IEnumerable<Place>> GetPlacesByZoneId(long IdPrefecture);
        public Task<IEnumerable<string>> GetPalcesByIdPrefecture(long IdPrefecture);
        public void SaveChange();
        public bool PlaceExists(long id);
        public void Remove(Place place);
    }

}