using GraphQL;
using GraphQL.Types;
using GraphQLProject.Interfaces;
using GraphQLProject.Models;
using GraphQLProject.Services;
using GraphQLProject.Type;

namespace GraphQLProject.Mutation
{
    public class CategoryMutation : ObjectGraphType
    {
        public CategoryMutation(ICategoryRepository categoryRepository) {
            // Add menu
            Field<CategoryType>("addCategory")
            .Arguments(new QueryArguments(
                new QueryArgument<NonNullGraphType<CategoryInputType>> { Name = "category" }
            ))
            .ResolveAsync(async context =>
            {
                var categoryInput = context.GetArgument<Category>("category");
                return await categoryRepository.AddCategory(categoryInput);
            });
            // Update category
            Field<CategoryType>("updateCategory")
            .Arguments(new QueryArguments(
                new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                new QueryArgument<NonNullGraphType<CategoryInputType>> { Name = "category" }
            ))
            .ResolveAsync(async context =>
            {
                var id = context.GetArgument<int>("id");
                var categoryInput = context.GetArgument<Category>("category");
                return await categoryRepository.UpdateCategory(categoryInput);
            });
            // Delete category
            Field<CategoryType>("deleteCategory")
            .Description("Deletes a category and returns the deleted record.")
            .Arguments(new QueryArguments(
                new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }
            ))
            .ResolveAsync(async context =>
            {
                var id = context.GetArgument<int>("id");

                // Ensure you capture the result from the repository
                var deletedCategory = await categoryRepository.DeleteCategory(id);

                if (deletedCategory == null)
                {
                    // Providing a specific error message helps the frontend 
                    // identify if they tried to delete something that doesn't exist.
                    context.Errors.Add(new ExecutionError($"Category with ID {id} not found."));
                    return null;
                }

                return deletedCategory;
            });
        }
    }
}
