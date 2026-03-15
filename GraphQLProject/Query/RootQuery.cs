using GraphQL.Types;

namespace GraphQLProject.Query
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery()
        {
            Name = "RootQuery";

            // We use the specific Query classes as the GraphType for these fields
            Field<MenuQuery>("MenuQuery")
                .Resolve(context => new { }); // Returns an empty object to satisfy the level

            Field<CategoryQuery>("CategoryQuery")
                .Resolve(context => new { });

            Field<ReservationQuery>("ReservationQuery")
                .Resolve(context => new { });
        }
    }
}