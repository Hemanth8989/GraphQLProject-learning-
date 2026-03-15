using GraphQLProject.Data;
using GraphQLProject.Interfaces;
using GraphQLProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQLProject.Services
{
    public class MenuRepository : IMenuRepository
    {
        private readonly GraphqlDbContext _dbContext;

        public MenuRepository(GraphqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Menu>> GetAllMenus()
        {
            try
            {
                // Use ToListAsync for non-blocking database reads
                return await _dbContext.Menus.ToListAsync();
            }
            catch (Exception ex)
            {
                // Surface the exception to the console so GraphQL ExposeExceptions can include inner details
                Console.Error.WriteLine($"Error fetching menus: {ex}");
                throw;
            }
        }

        public async Task<Menu?> GetMenu(int id)
        {
            try
            {
                // FindAsync is optimized for looking up primary keys
                return await _dbContext.Menus.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching menu {id}: {ex}");
                throw;
            }
        }

        public async Task<Menu> AddMenu(Menu menu)
        {
            _dbContext.Menus.Add(menu);
            await _dbContext.SaveChangesAsync();
            return menu; // The ID is now populated by SQL Server
        }

        public async Task<Menu?> UpdateMenu(int id, Menu menu)
        {
            var existing = await _dbContext.Menus.FindAsync(id);

            if (existing != null)
            {
                existing.Name = menu.Name;
                existing.Description = menu.Description;
                existing.Price = menu.Price;

                await _dbContext.SaveChangesAsync();
            }

            return existing;
        }

        public async Task<Menu?> DeleteMenu(int id)
        {
            var menu = await _dbContext.Menus.FindAsync(id);

            if (menu != null)
            {
                _dbContext.Menus.Remove(menu);
                await _dbContext.SaveChangesAsync();
            }

            return menu;
        }
    }
}