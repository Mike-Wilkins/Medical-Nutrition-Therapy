using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface IDietRepository
    {
        Task<DietType> Add(DietType diet);
        Task<DietType> Delete(int id);
        DietType Update(DietType diet);
        Task<DietType> GetDiet(int id);
        Task<IEnumerable<DietType>> GetAllDiets();

    }
}
