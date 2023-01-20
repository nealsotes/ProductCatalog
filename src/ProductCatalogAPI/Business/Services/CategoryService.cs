using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductCatalogAPI.Data.Repositories;
using ProductCatalogAPI.Models;

namespace ProductCatalogAPI.Business.Services
{
 
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsnyc();
        }
        public async Task<Category?> GetCategoryByIdAsync(int? id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }
        public async Task AddCategoryAsync(Category category)
        {
            await _categoryRepository.AddAsync(category);
        }
        public async Task UpdateCategoryAsync(Category category)
        {
            await _categoryRepository.UpdateAsync(category);
        }
        public async Task DeleteCategoryAsync(Category category)
        {
            await _categoryRepository.DeleteAsync(category);
        }
        //implement CategoryExistsAsync method
        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id) != null;
        }


    }
}