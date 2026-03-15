using GraphQL;
using GraphQL.Types;
using GraphQLProject.Interfaces;
using GraphQLProject.Type;

namespace GraphQLProject.Query
{
    public class ReservationQuery : ObjectGraphType
    {
        public ReservationQuery(IReservationRepository reservationRepository)
        {
            // Get all reservations
            Field<ListGraphType<ReservationType>>("reservations")
                .ResolveAsync(async context =>
                {
                    return await reservationRepository.GetAllReservations();
                });

            // Get reservation by id
            Field<ReservationType>("reservation")
                .Arguments(new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" }
                ))
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<int>("id");
                    return await reservationRepository.GetReservation(id);
                });
        }
    }
    
}
