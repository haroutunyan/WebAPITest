using Domain.Categories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Categories
{
    public interface ICategoryManager
    {
        Task Create(CategoryModel model);
        Task Delete(int id);
        Task<CategoryModel> Get(int id);
        Task Update(CategoryModel model);
        Task<List<CategoryModel>> GetAll();
    }
}
