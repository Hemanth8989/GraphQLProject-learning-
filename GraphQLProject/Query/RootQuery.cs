using GraphQL.Types;

namespace GraphQLProject.Query
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery()
        {
            Name = "RootQuery";

            // We use the specific Query classes as the GraphType for these fields
            Field<MenuQuery>("menu")
                .Resolve(context => new { }); // Returns an empty object to satisfy the level

            Field<CategoryQuery>("category")
                .Resolve(context => new { });

            Field<ReservationQuery>("reservation")
                .Resolve(context => new { });
        }
    }
}