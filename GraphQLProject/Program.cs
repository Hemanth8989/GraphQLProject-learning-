using GraphQL;
using GraphQL.Types;
using GraphQLProject.Data;
using GraphQLProject.Interfaces;
using GraphQLProject.Mutation;
using GraphQLProject.Query;
using GraphQLProject.Schema;
using GraphQLProject.Services;
using GraphQLProject.Type;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Database Configuration
// This must be registered before the services that use it
builder.Services.AddDbContext<GraphqlDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GraphqlDbContextConnection")));

// 2. Register Repository (Scoped to match DbContext)
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// 3. Register GraphQL Types
builder.Services.AddScoped<MenuType>();
builder.Services.AddScoped<ReservationType>();
builder.Services.AddScoped<CategoryType>();
builder.Services.AddScoped<MenuInputType>();
builder.Services.AddScoped<CategoryInputType>();
builder.Services.AddScoped<ReservationInputType>();
//querys
builder.Services.AddScoped<MenuQuery>();
builder.Services.AddScoped<CategoryQuery>();
builder.Services.AddScoped<ReservationQuery>();
builder.Services.AddScoped<RootQuery>();
//mutations
builder.Services.AddScoped<MenuMutation>();
builder.Services.AddScoped<CategoryMutation>();
builder.Services.AddScoped<ReservationMutation>();
builder.Services.AddScoped<RootMutation>();
builder.Services.AddScoped<ISchema, Rootschema>();

// 4. GraphQL Engine Configuration
builder.Services.AddGraphQL(options => options
    .AddSchema<Rootschema>()
    .AddSystemTextJson()
    .AddGraphTypes(typeof(Rootschema).Assembly)
    .AddErrorInfoProvider(opt => opt.ExposeExceptionDetails = true)// Automatically registers MenuType, MenuInputType, etc.
);

builder.Services.AddControllers();

// 5. CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowGraphQL", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// ... Middleware (Rest of your code is correct)
app.UseCors("AllowGraphQL");
app.UseHttpsRedirection();
app.UseGraphQL<ISchema>("/graphql");
app.UseGraphQLGraphiQL("/ui/graphql");
app.MapControllers();

app.Run();