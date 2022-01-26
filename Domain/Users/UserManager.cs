using Abstraction.Repository;
using AutoMapper;
using Domain.Categories.Models;
using Domain.Users.Models;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    public class UserManager : IUserManager
    {
        readonly IMapper _mapper;
        readonly IUnitOfWork _uow;

        public UserManager(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task Create(UserModel model)
        {
            var user = _mapper.Map<User>(model);
            _uow.Repository<User>().Add(user);
            await _uow.SaveAsync();
        }

        public async Task Delete(int id)
        {
            var user = await _uow.Repository<User>().DbSet.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
                throw new CustomException(Constants.UserNotFound, HttpStatusCode.NotFound);

            _uow.Repository<User>().Delete(user);
            await _uow.SaveAsync();
        }

        public async Task<UserModel> Get(int id)
        {
            var user = await _uow.Repository<User>().DbSet.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
                throw new CustomException(Constants.UserNotFound, HttpStatusCode.NotFound);

            return _mapper.Map<UserModel>(user);
        }

        public async Task<List<UserModel>> GetAll()
        {
            return await _uow.Repository<User>().DbSet
                .Include(u => u.Category)
                .Select(u => _mapper.Map<UserModel>(u)).ToListAsync();
        }

        public async Task Update(UserModel model)
        {
            var user = await _uow.Repository<User>().DbSet.FirstOrDefaultAsync(u => u.Id == model.Id);
            if (user is null)
                throw new CustomException(Constants.UserNotFound, HttpStatusCode.NotFound);

            _mapper.Map(model, user);
            _uow.Repository<User>().Update(user);
            await _uow.SaveAsync();
        }
    }
}
