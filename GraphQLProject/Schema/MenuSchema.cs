using GraphQLProject.Query;
using GraphQLProject.Mutation;
using Microsoft.Extensions.DependencyInjection; // Required for GetRequiredService

namespace GraphQLProject.Schema
{
    public class MenuSchema : GraphQL.Types.Schema
    {
        // Injecting IServiceProvider is the best practice for Scoped GraphQL schemas
        public MenuSchema(IServiceProvider provider) : base(provider)
        {
            // Resolve the Query and Mutation from the service provider
            Query = provider.GetRequiredService<MenuQuery>();

            Mutation = provider.GetRequiredService<MenuMutation>();
        }
    }
}