using ValVenalEstimatorApi.Models; 
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ValVenalEstimatorApi.Contracts
{
    public interface IPrefectureRepository
    {
        public Task<Prefecture> AddPrefecture(Prefecture prefecture);
        public Task<Prefecture> GetPrefecture(long id);
        public Task<IEnumerable<Prefecture>> GetAllPrefectures();
        public Task<IActionResult> DeletePrefecture(long id);
        public void LoadDataInDbWithCsvFile(string accessPath);
        public void SaveChange();
        public bool PrefectureExists(long id);
        public void Remove(Prefecture prefecture);
    }
}