using GraphQL.Types;

namespace GraphQLProject.Type
{
    public class CategoryInputType : InputObjectGraphType
    {
        public CategoryInputType() {
            Name = "CategoryInput";           // GraphQL input type name
            Field<IntGraphType>("id");
            Field<StringGraphType>("name");
            Field<StringGraphType>("imageUrl");
        }
    }
}
