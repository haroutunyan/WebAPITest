using Domain.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    public interface IUserManager
    {
        Task Create(UserModel model);
        Task Update(UserModel model);
        Task Delete(int id);
        Task<UserModel> Get(int id);
        Task<List<UserModel>> GetAll();
    }
}
