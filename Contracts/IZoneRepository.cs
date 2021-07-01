using ValVenalEstimatorApi.Models; 
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ValVenalEstimatorApi.Contracts
{
    public interface IZoneRepository
    {
        public Task<Zone> AddZone(Zone Zone);
        public Task<Zone> GetZone(long id);
        public Task<IEnumerable<Zone>> GetAllZones();
        public Task<IActionResult> DeleteZone(long id);
        public void LoadDataInDbWithCsvFile(string accessPath);
        public void SaveChange();
        public bool ZoneExists(long id);
        public void Remove(Zone Zone);
    }
}