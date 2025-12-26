using TechnicalTestApi.Security;
using Microsoft.EntityFrameworkCore;
using TechnicalTestApi.Infraestructure;
using TechnicalTestApi.Application.Users.Command;
using TechnicalTestApi.Application.Users.Queries;
using TechnicalTestApi.Application.Addresses.Command;
using TechnicalTestApi.Application.Addresses.Queries;
using TechnicalTestApi.Application.Currencies.Command;
using TechnicalTestApi.Application.Currencies.Queries; 
using TechnicalTestApi.Handlers.Users;
using TechnicalTestApi.Handlers.Addresses;
using TechnicalTestApi.Handlers.Currencies;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using TechnicalTestApi.Domain.Entities;
using TechnicalTestApi.Validators.Addresses;
using TechnicalTestApi.Validators.Users;
using TechnicalTestApi.Validators.Currencies;
using TechnicalTestApi.Dtos;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// EF Core + SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

// Configurar JSON serialization para evitar referencias cíclicas
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program)); // Esto escaneará todos los Profiles

// Registrar handlers y servicios
builder.Services.AddScoped<CreateUserHandler>();
builder.Services.AddScoped<GetUsersHandler>();
builder.Services.AddScoped<GetUserByIdHandler>();
builder.Services.AddScoped<UpdateUserHandler>();
builder.Services.AddScoped<DeleteUserHandler>();
builder.Services.AddScoped<CreateAddressHandler>();
builder.Services.AddScoped<UpdateAddressHandler>();
builder.Services.AddScoped<DeleteAddressHandler>();
builder.Services.AddScoped<GetUserAddressesHandler>();
builder.Services.AddScoped<GetCurrenciesHandler>();
builder.Services.AddScoped<CreateCurrencyHandler>();
builder.Services.AddScoped<ConvertCurrencyHandler>();

// Validadores
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAddressValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCurrencyValidator>();

// Password hasher
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

var app = builder.Build();

app.UseMiddleware<ApiKeyMiddleware>();

// Obtener AutoMapper
var mapper = app.Services.GetRequiredService<IMapper>();

app.MapGet("/", () => "API running");

var api = app.MapGroup("/api/v1").WithTags("API");
var users = api.MapGroup("/users").WithTags("Users");

// CREATE USER
users.MapPost("/", async (
    HttpContext context,
    CreateUserHandler handler,
    IValidator<CreateUserCommand> validator,
    IMapper mapper
) =>
{
    try
    {
        var dto = await context.Request.ReadFromJsonAsync<CreateUserDto>();
        if (dto is null)
            return Results.BadRequest("Invalid request body");

        // Mapear DTO a Command usando AutoMapper
        var cmd = mapper.Map<CreateUserCommand>(dto);

        var result = await validator.ValidateAsync(cmd);
        if (!result.IsValid)
            return Results.ValidationProblem(result.ToDictionary());

        var user = await handler.Handle(cmd);
        return Results.Created($"/api/v1/users/{user.Id}", user);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error creating user: {ex.Message}");
    }
});

// GET ALL USERS
users.MapGet("/", async (
    bool? isActive,
    GetUsersHandler handler
) =>
{
    try
    {
        var users = await handler.Handle(new GetUsersQuery(isActive));
        return Results.Ok(users);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error getting users: {ex.Message}");
    }
});

// GET USER BY ID
users.MapGet("/{id:int}", async (
    int id,
    GetUserByIdHandler handler
) =>
{
    try
    {
        var user = await handler.Handle(new GetUserByIdQuery(id));
        return user is null ? Results.NotFound() : Results.Ok(user);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error getting user: {ex.Message}");
    }
});

// UPDATE USER
users.MapPut("/{id:int}", async (
    int id,
    HttpContext context,
    UpdateUserHandler handler,
    IValidator<UpdateUserCommand> validator,
    IMapper mapper
) =>
{
    try
    {
        var dto = await context.Request.ReadFromJsonAsync<UpdateUserDto>();
        if (dto is null)
            return Results.BadRequest("Invalid request body");
        
        if (id != dto.Id)
            return Results.BadRequest("Id mismatch");

        // Mapear DTO a Command usando AutoMapper
        var cmd = mapper.Map<UpdateUserCommand>(dto);

        var result = await validator.ValidateAsync(cmd);
        if (!result.IsValid)
            return Results.ValidationProblem(result.ToDictionary());

        var updated = await handler.Handle(cmd);
        return updated ? Results.NoContent() : Results.NotFound();
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error updating user: {ex.Message}");
    }
});

// DELETE USER
users.MapDelete("/{id:int}", async (
    int id,
    DeleteUserHandler handler
) =>
{
    try
    {
        var deleted = await handler.Handle(new DeleteUserCommand(id));
        return deleted ? Results.NoContent() : Results.NotFound();
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error deleting user: {ex.Message}");
    }
});

/*
* Addresses endpoints
*/
var addressUserGroup = api.MapGroup("/users/{userId:int}/addresses")
                      .WithTags("Addresses");

var addressGroup = api.MapGroup("/addresses")
                      .WithTags("Addresses");

// CREATE ADDRESS FOR USER
addressUserGroup.MapPost("", async (
    int userId,
    HttpContext context,
    CreateAddressHandler handler,
    IValidator<CreateAddressCommand> validator,
    IMapper mapper
) =>
{
    try
    {
        var dto = await context.Request.ReadFromJsonAsync<CreateAddressDto>();
        if (dto is null)
            return Results.BadRequest("Invalid request body");

        // Mapear DTO a Command
        var cmd = mapper.Map<CreateAddressCommand>(dto);
        cmd.UserId = userId; // Asignar el userId de la ruta

        var validation = await validator.ValidateAsync(cmd);
        if (!validation.IsValid)
            return Results.ValidationProblem(validation.ToDictionary());

        var address = await handler.Handle(cmd);
        return address is null
            ? Results.NotFound("User not found")
            : Results.Created($"/addresses/{address.Id}", address);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error creating address: {ex.Message}");
    }
});

// GET ADDRESSES FOR USER
addressUserGroup.MapGet("", async (
    int userId,
    GetUserAddressesHandler handler
) =>
{
    try
    {
        var addresses = await handler.Handle(new GetUserAddressesQuery(userId));
        return Results.Ok(addresses);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error getting addresses: {ex.Message}");
    }
});

// UPDATE ADDRESS
addressGroup.MapPut("/{id:int}", async (
    int id,
    HttpContext context,
    UpdateAddressHandler handler,
    IMapper mapper
) =>
{
    try
    {
        var dto = await context.Request.ReadFromJsonAsync<UpdateAddressDto>();
        if (dto is null)
            return Results.BadRequest("Invalid request body");
        
        if (id != dto.Id)
            return Results.BadRequest();

        // Mapear DTO a Command
        var cmd = mapper.Map<UpdateAddressCommand>(dto);

        var updated = await handler.Handle(cmd);
        return updated ? Results.NoContent() : Results.NotFound();
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error updating address: {ex.Message}");
    }
});

// DELETE ADDRESS
addressGroup.MapDelete("/{id:int}", async (
    int id,
    DeleteAddressHandler handler
) =>
{
    try
    {
        var deleted = await handler.Handle(new DeleteAddressCommand(id));
        return deleted ? Results.NoContent() : Results.NotFound();
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error deleting address: {ex.Message}");
    }
});

/*
* Currency endpoints
*/
var currencyGroup = api.MapGroup("/currencies").WithTags("Currencies");

// 1. Listar monedas
currencyGroup.MapGet("/", async (
    GetCurrenciesHandler handler
) =>
{
    try
    {
        var currencies = await handler.Handle(new GetCurrenciesQuery());
        return Results.Ok(currencies);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error getting currencies: {ex.Message}");
    }
});

// 2. Crear moneda
currencyGroup.MapPost("/", async (
    HttpContext context,
    CreateCurrencyHandler handler,
    IValidator<CreateCurrencyCommand> validator,
    IMapper mapper
) =>
{
    try
    {
        var dto = await context.Request.ReadFromJsonAsync<CreateCurrencyDto>();
        if (dto is null)
            return Results.BadRequest("Invalid request body");

        var cmd = mapper.Map<CreateCurrencyCommand>(dto);

        var result = await validator.ValidateAsync(cmd);
        if (!result.IsValid)
            return Results.ValidationProblem(result.ToDictionary());

        var currency = await handler.Handle(cmd);
        return Results.Created($"/currencies/{currency.Id}", currency);
    }
    catch (InvalidOperationException ex) when (ex.Message.Contains("ya existe"))
    {
        return Results.Conflict(ex.Message);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error creating currency: {ex.Message}");
    }
});

// 3. Conversión de divisas
currencyGroup.MapPost("/convert", async (
    HttpContext context,
    ConvertCurrencyHandler handler,
    IValidator<ConvertCurrencyCommand> validator,
    IMapper mapper
) =>
{
    try
    {
        var dto = await context.Request.ReadFromJsonAsync<CurrencyConversionRequestDto>();
        if (dto is null)
            return Results.BadRequest("Invalid request body");

        var cmd = mapper.Map<ConvertCurrencyCommand>(dto);

        var validation = await validator.ValidateAsync(cmd);
        if (!validation.IsValid)
            return Results.ValidationProblem(validation.ToDictionary());

        var (convertedAmount, fromCurrency, toCurrency) = await handler.Handle(cmd);

        var rate = toCurrency.RateToBase / fromCurrency.RateToBase;

        var response = new CurrencyConversionResponseDto
        {
            FromCurrency = fromCurrency.Code,
            ToCurrency = toCurrency.Code,
            OriginalAmount = cmd.Amount,
            ConvertedAmount = Math.Round(convertedAmount, 2),
            Rate = Math.Round(rate, 6),
            ConversionDate = DateTime.UtcNow
        };

        return Results.Ok(response);
    }
    catch (KeyNotFoundException ex)
    {
        return Results.NotFound(ex.Message);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error converting currency: {ex.Message}");
    }
});

// Inicializar base de datos con datos de ejemplo
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    
    // Agregar algunas monedas de ejemplo si la tabla está vacía
    if (!db.Currencies.Any())
    {
        db.Currencies.AddRange(
            new Currency { Code = "USD", Name = "Dólar Americano", RateToBase = 1.0m },
            new Currency { Code = "EUR", Name = "Euro", RateToBase = 0.85m },
            new Currency { Code = "PYG", Name = "Guaraní Paraguayo", RateToBase = 7300m },
            new Currency { Code = "ARS", Name = "Peso Argentino", RateToBase = 350m },
            new Currency { Code = "BRL", Name = "Real Brasileño", RateToBase = 5.0m }
        );
        await db.SaveChangesAsync();
    }
}

app.Run();