using GraphQL.Types;

namespace GraphQLProject.Type
{
    public class ReservationInputType : InputObjectGraphType
    {
        public ReservationInputType() {
            Name = "ReservationInput";           // GraphQL input type name
            Field<IntGraphType>("id");
            Field<StringGraphType>("customerName");
            Field<StringGraphType>("email");
            Field<StringGraphType>("phoneNumber");
            Field<IntGraphType>("partySize");
            Field<StringGraphType>("specialRequests");
            Field<DateTimeGraphType>("reservationDate");
        }
    }
}
