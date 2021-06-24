using ValVenalEstimatorApi.Contracts;
using ValVenalEstimatorApi.Data;
using System.Threading.Tasks;
using ValVenalEstimatorApi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ValVenalEstimatorApi.Repositories
{
    public class PrefectureRepository : IPrefectureRepository
    {
        readonly ValVenalEstimatorDbContext _valVenalEstDbContext;              
        public PrefectureRepository(ValVenalEstimatorDbContext context)
        {  
            _valVenalEstDbContext = context;   
        } 
        public async Task<Prefecture> AddPrefecture(Prefecture prefecture)
        {
            _valVenalEstDbContext.Add(prefecture);
            await _valVenalEstDbContext.SaveChangesAsync();
            return prefecture;
        }
        public async Task<ActionResult<Prefecture>> GetPrefecture(long id)
        {
            var prefecture = await _valVenalEstDbContext.Prefectures.FindAsync(id);

            if (prefecture == null)
            {
                return null;
            }
            return prefecture;
        }

        public async Task<ActionResult<IEnumerable<Prefecture>>> GetAllPrefectures()
        {
            return await _valVenalEstDbContext.Prefectures.ToListAsync();
        }

        public async Task<IActionResult> DeletePrefecture(long id)
        {
            var prefecture = await _valVenalEstDbContext.Prefectures.FindAsync(id);

            if (prefecture == null)
            {
                return null;     
            }     
            _valVenalEstDbContext.Prefectures.Remove(prefecture);                                      
            await _valVenalEstDbContext.SaveChangesAsync();
            return null;
        }

        public async void SaveChange()
        {
            await _valVenalEstDbContext.SaveChangesAsync();
        }

        public bool PrefectureExists(long id) => _valVenalEstDbContext.Prefectures.Any(p => p.Id == id);
        
        public void Remove(Prefecture prefecture)
        {
            _valVenalEstDbContext.Prefectures.Remove(prefecture);   
        }
    }
}