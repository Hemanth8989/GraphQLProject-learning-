using GraphQL;
using GraphQL.Types;
using GraphQLProject.Interfaces;
using GraphQLProject.Type;

namespace GraphQLProject.Query
{
    public class MenuQuery : ObjectGraphType
    {
        public MenuQuery(IMenuRepository menuRepository)
        {
            // Get all menus
            Field<ListGraphType<MenuType>>("menus")
                .ResolveAsync(async context =>
                {
                    return await menuRepository.GetAllMenus();
                });

            // Get menu by id
            Field<MenuType>("menu")
                .Arguments(new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" }
                ))
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<int>("id");
                    return await menuRepository.GetMenu(id);
                });
        }
    }
}