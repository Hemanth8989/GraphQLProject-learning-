using GraphQLProject.Models;

namespace GraphQLProject.Interfaces
{
    public interface IMenuRepository
    {
        Task<List<Menu>> GetAllMenus();
        Task<Menu> GetMenu(int id);
        Task<Menu> AddMenu(Menu menu);
        Task<Menu> UpdateMenu(int id,Menu menu);
        Task<Menu> DeleteMenu(int id);
    }
}
