using GraphQL.Types;

namespace GraphQLProject.Type
{
    public class MenuInputType : InputObjectGraphType
    {
        public MenuInputType()
        {
            Name = "MenuInput";           // GraphQL input type name
            Field<IntGraphType>("id");
            Field<StringGraphType>("name");
            Field<StringGraphType>("description");
            Field<FloatGraphType>("price");
            Field<IntGraphType>("categoryId");
        }
    }
}
