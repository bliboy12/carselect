
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod();
    })
);
builder.Services.Configure<CarImageRepositoryOptions>(
    builder.Configuration.GetSection("CarImageRepositoryOptions")
);

builder.Services.Configure<CarRepositoryOptions>(
    builder.Configuration.GetSection("CarRepositoryOptions")
);

builder.Services.Configure<CosmosOptions>(
    builder.Configuration.GetSection("CosmosOptions")
);

builder.Services.Configure<FavoriteRepositoryOptions>(
    builder.Configuration.GetSection("FavoriteRepositoryOptions")
);

builder.Services.Configure<ListingRepositoryOptions>(
    builder.Configuration.GetSection("ListingRepositoryOptions")
);

builder.Services.Configure<TransactionRepositoryOptions>(
    builder.Configuration.GetSection("TransactionRepositoryOptions")
);

builder.Services.Configure<ReviewRepositoryOptions>(
    builder.Configuration.GetSection("ReviewRepositoryOptions")
);

builder.Services.Configure<UserRepositoryOptions>(
    builder.Configuration.GetSection("UserRepositoryOptions")
);

builder.Services.Configure<BlobStorageRepositoryOptions>(
    builder.Configuration.GetSection("BlobStorageRepositoryOptions")
);

// Cosmos Implementations 
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IListingService, ListingService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ICarImageService, CarImageService>();


// Old Implemention (cosmos) - Needs to be removed when SQL Refactor is completed
builder.Services.AddScoped<IUserService, UserService>();
// builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();


// SQL Service Refactor
builder.Services.AddScoped<IUserSqlService, UserSqlService>();
builder.Services.AddScoped<IReviewSqlService, ReviewSqlService>();
builder.Services.AddScoped<IFavoriteSqlService, FavoriteSqlService>();

builder.Services.AddHttpClient<ICarService, CarService>(client =>
{
    client.BaseAddress = new Uri("https://vpic.nhtsa.dot.gov/api/vehicles/");
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json")
        );
});

// Azure SQL Connection
builder.Services.AddDbContext<CarSelectDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

// Cosmos Implementations 
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IListingRepository, ListingRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ICarImageRepository, CarImageRepository>();

// Old Implemention (cosmos) - Needs to be removed when SQL Refactor is completed
builder.Services.AddScoped<IUserRepository, UserRepository>();
// builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();

// SQL Refactoring 
builder.Services.AddScoped<IUserSqlRepository, UserSqlRepository>();
builder.Services.AddScoped<IReviewSqlRepository, ReviewSqlRepository>();
builder.Services.AddScoped<IFavoriteSqlRepository, FavoriteSqlRepository>();

builder.Services.AddScoped<IBlobStorageRepository, BlobStorageRepository>();

builder.Services.AddHttpClient();
builder.Services.AddControllers();
// Converts enums to strings and vice versa even ignoring capitilizations
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();

app.UseRouting();
app.UseCors("AllowFrontend");
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();