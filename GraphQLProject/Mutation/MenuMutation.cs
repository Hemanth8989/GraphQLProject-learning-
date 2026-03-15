using GraphQL;
using GraphQL.Types;
using GraphQLProject.Interfaces;
using GraphQLProject.Models;
using GraphQLProject.Type;

namespace GraphQLProject.Mutation
{
    public class MenuMutation: ObjectGraphType
    {
        public MenuMutation(IMenuRepository menuRepository)
        {
            // Add menu
            Field<MenuType>("addMenu")
            .Arguments(new QueryArguments(
                new QueryArgument<NonNullGraphType<MenuInputType>> { Name = "menu" }
            ))
            .ResolveAsync(async context =>
            {
                var menuInput = context.GetArgument<Menu>("menu");
                return await menuRepository.AddMenu(menuInput);
            });
            // Update menu
            Field<MenuType>("updateMenu")
            .Arguments(new QueryArguments(
                new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                new QueryArgument<NonNullGraphType<MenuInputType>> { Name = "menu" }
            ))
            .ResolveAsync(async context =>
            {
                var id = context.GetArgument<int>("id");
                var menuInput = context.GetArgument<Menu>("menu");
                return await menuRepository.UpdateMenu(id, menuInput);
            });
            // Delete menu
            Field<MenuType>("deleteMenu")
            .Arguments(new QueryArguments(
                new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }
            ))
            .ResolveAsync(async context =>
            {
                var id = context.GetArgument<int>("id");
                return await menuRepository.DeleteMenu(id);
            });

        }
    }
}
