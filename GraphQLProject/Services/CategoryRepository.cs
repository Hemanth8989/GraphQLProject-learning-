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

        public async Task DeleteCategory(int id)
        {
            var existing = _dbContext.Categories.Find(id);
            if (existing == null)
                return;

            _dbContext.Categories.Remove(existing);
            _dbContext.SaveChanges();
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
