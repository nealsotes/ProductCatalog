using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductCatalogAPI.Models;

namespace ProductCatalogAPI.Data.Repositories
{
    public interface IProductCategoryRepository
    {
        Task<IEnumerable<ProductCategory>> GetProductCategoriesAsync();
        Task<ProductCategory?> GetProductCategoryByIdAsync(int? id);
        Task AddProductCategoryAsync(ProductCategory productCategory);
        Task UpdateProductCategoryAsync(ProductCategory productCategory);
        Task DeleteProductCategoryAsync(int id);
    }
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly DataContext _context;
        public ProductCategoryRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddProductCategoryAsync(ProductCategory productCategory)
        {
            _context.ProductCategories.Add(productCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductCategoryAsync(int id)
        {
            var productCategory = await _context.ProductCategories.FindAsync(id);
            _context.ProductCategories.Remove(productCategory?? throw new InvalidOperationException());
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductCategory>> GetProductCategoriesAsync()
        {
            return await _context.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategory?> GetProductCategoryByIdAsync(int? id)
        {
            return await _context.ProductCategories.FindAsync(id);
            
        }

        public async Task UpdateProductCategoryAsync(ProductCategory productCategory)
        {
            _context.ProductCategories.Update(productCategory);
            await _context.SaveChangesAsync();
        }
    }
}