using GraphQL;
using GraphQL.Types;
using GraphQLProject.Interfaces;
using GraphQLProject.Models;
using GraphQLProject.Type;

namespace GraphQLProject.Mutation
{
    public class ReservationMutation : ObjectGraphType
    {
        public ReservationMutation(IReservationRepository reservationRepository)
        {
            // Add reservation
            Field<ReservationType>("addReservation")
            .Arguments(new QueryArguments(
                new QueryArgument<NonNullGraphType<ReservationInputType>> { Name = "reservation" }
            ))
            .ResolveAsync(async context =>
            {
                var reservationInput = context.GetArgument<Reservation>("reservation");
                return await reservationRepository.AddReservation(reservationInput);
            });
            // Update reservation
            Field<ReservationType>("updateReservation")
            .Arguments(new QueryArguments(
                new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                new QueryArgument<NonNullGraphType<ReservationInputType>> { Name = "reservation" }
            ))
            .ResolveAsync(async context =>
            {
                var id = context.GetArgument<int>("id");
                var reservationInput = context.GetArgument<Reservation>("reservation");
                return await reservationRepository.UpdateReservation(reservationInput);
            });
            // Delete reservation
            Field<ReservationType>("deleteReservation")
            .Description("Deletes a reservation and returns the deleted record.")
            .Arguments(new QueryArguments(
                new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }
            ))
            .ResolveAsync(async context =>
            {
                var id = context.GetArgument<int>("id");

                // Ensure you capture the result from the repository
                var deletedReservation = await reservationRepository.DeleteReservation(id);

                if (deletedReservation == null)
                {
                    // Providing a specific error message helps the frontend 
                    // identify if they tried to delete something that doesn't exist.
                    context.Errors.Add(new ExecutionError($"Reservation with ID {id} not found."));
                    return null;
                }

                return deletedReservation;
            });
        }
    }
}
