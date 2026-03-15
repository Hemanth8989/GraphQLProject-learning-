using GraphQL.Types;
using GraphQLProject.Query;

namespace GraphQLProject.Mutation
{
    public class RootMutation : ObjectGraphType
    {
        public RootMutation()
        {
            Name = "RootMutation";
            Field<MenuMutation>("MenuMutation").Resolve(context => new { });
            Field<CategoryMutation>("CategoryMutation").Resolve(context => new { });
            Field<ReservationMutation>("ReservationMutation").Resolve(context => new { });
        }
    }
}
