using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductCatalogAPI.Data.Repositories;
using ProductCatalogAPI.Models;

namespace ProductCatalogAPI.Business.Services
{
    public class ProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }
        public async Task<IEnumerable<ProductCategory>> GetProductCategoriesAsync()
        {
            return await _productCategoryRepository.GetProductCategoriesAsync();
        }
        public async Task<ProductCategory?> GetProductCategoryByIdAsync(int? id)
        {
            return await _productCategoryRepository.GetProductCategoryByIdAsync(id);
        }
        public async Task AddProductCategoryAsync(ProductCategory productCategory)
        {
            await _productCategoryRepository.AddProductCategoryAsync(productCategory);
        }
        public async Task UpdateProductCategoryAsync(ProductCategory productCategory)
        {
            await _productCategoryRepository.UpdateProductCategoryAsync(productCategory);
        }
        public async Task DeleteProductCategoryAsync(int id)
        {
            await _productCategoryRepository.DeleteProductCategoryAsync(id);
        }
        
    }
}