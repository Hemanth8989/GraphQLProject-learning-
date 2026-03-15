using GraphQL;
using GraphQL.Types;
using GraphQLProject.Interfaces;
using GraphQLProject.Type;

namespace GraphQLProject.Query
{
    public class CategoryQuery : ObjectGraphType
    {
        public CategoryQuery(ICategoryRepository categoryRepository)
        {
            // Get all categories
            Field<ListGraphType<CategoryType>>("categories")
                .ResolveAsync(async context =>
                {
                    return await categoryRepository.GetAllCategories();
                });

            // Get category by id
            Field<CategoryType>("category")
                .Arguments(new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" }
                ))
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<int>("id");
                    return await categoryRepository.GetCategory(id);
                });
        }
    }
}
