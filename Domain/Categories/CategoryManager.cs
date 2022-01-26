using Abstraction.Repository;
using AutoMapper;
using Domain.Categories.Models;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Categories
{
    public class CategoryManager : ICategoryManager
    {
        IMapper _mapper;
        IUnitOfWork _uow;

        public CategoryManager(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task Create(CategoryModel model)
        {
            var category = _mapper.Map<Category>(model);
            _uow.Repository<Category>().Add(category);
            await _uow.SaveAsync();
        }

        public async Task Delete(int id)
        {
            var category = await _uow.Repository<Category>().DbSet.FirstOrDefaultAsync(u => u.Id == id);
            if (category is null)
                throw new CustomException(Constants.CategoryNotFound, HttpStatusCode.NotFound);

            _uow.Repository<Category>().Delete(category);
            await _uow.SaveAsync();
        }

        public async Task<CategoryModel> Get(int id)
        {
            var category = await _uow.Repository<Category>().DbSet.FirstOrDefaultAsync(u => u.Id == id);
            if (category is null)
                throw new CustomException(Constants.CategoryNotFound, HttpStatusCode.NotFound);

            return _mapper.Map<CategoryModel>(category);
        }

        public async Task<List<CategoryModel>> GetAll()
        {
            return await _uow.Repository<Category>().DbSet
                .Include(u => u.Users)
                .Select(u => _mapper.Map<CategoryModel>(u)).ToListAsync();
        }

        public async Task Update(CategoryModel model)
        {
            var category = await _uow.Repository<Category>().DbSet.FirstOrDefaultAsync(u => u.Id == model.Id);
            if (category is null)
                throw new CustomException(Constants.CategoryNotFound, HttpStatusCode.NotFound);

            _mapper.Map(model, category);
            _uow.Repository<Category>().Update(category);
            await _uow.SaveAsync();
        }
    }
}
