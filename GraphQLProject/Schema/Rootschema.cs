using GraphQLProject.Mutation;
using GraphQLProject.Query;

namespace GraphQLProject.Schema
{
    public class Rootschema : GraphQL.Types.Schema
    {
        // Injecting IServiceProvider is the best practice for Scoped GraphQL schemas
        public Rootschema(IServiceProvider provider) : base(provider)
        {
            // Resolve the Query and Mutation from the service provider
            Query = provider.GetRequiredService<RootQuery>();

            //Mutation = provider.GetRequiredService<MenuMutation>();
        }
    }
}
