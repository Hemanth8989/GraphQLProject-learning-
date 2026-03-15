using System.Linq;
using GraphQLProject.Data;
using GraphQLProject.Interfaces;
using GraphQLProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQLProject.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly GraphqlDbContext _dbContext;

        public CategoryRepository(GraphqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> AddCategory(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> DeleteCategory(int id)
        {
            // 1. Find the category in the database
            var category = await _dbContext.Categories.FindAsync(id);

            if (category == null)
            {
                return null;
            }

            // 2. Remove it from the change tracker
            _dbContext.Categories.Remove(category);

            // 3. Persist the change to SQL Server
            await _dbContext.SaveChangesAsync();

            // 4. Return the deleted object to the GraphQL resolver
            return category;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            // Eager-load related menus to avoid lazy-loading surprises
            return await _dbContext.Categories.Include(c => c.Menus).ToListAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            return _dbContext.Categories
                .Include(c => c.Menus)
                .FirstOrDefault(c => c.Id == id);
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            var existing = _dbContext.Categories.Find(category.Id);
            if (existing == null)
                return null;

            existing.Name = category.Name;
            existing.ImageUrl = category.ImageUrl;

            _dbContext.SaveChanges();
            return existing;
        }
    }
}
