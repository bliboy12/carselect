
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using CarSelect.Identity.Data;
using CarSelect.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://pg3alicarselect.z6.web.core.windows.net/")
        .AllowAnyHeader()
        .AllowAnyMethod();
    })
);

builder.Services.AddHttpClient("ReviewsService", client =>
{
    client.BaseAddress = new Uri("https://carselect-reviews-api-d2dvbtgpdcfkhra6.westeurope-01.azurewebsites.net");
});

builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.Authority = "https://carselect-identityserver-evafhmh8eacxbbgd.westeurope-01.azurewebsites.net";
        options.TokenValidationParameters.ValidateAudience = false;

        // // TODO: MUST BE REMOVED BEFORE DEPLOYING
        // // This causes any URL that isn't https to be accepted
        // if (builder.Environment.IsDevelopment())
        // {
        //     options.BackchannelHttpHandler = new HttpClientHandler
        //     {
        //         ServerCertificateCustomValidationCallback =
        //             HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        //     };
        // }
    });


builder.Services.AddAuthorizationBuilder()
    .AddPolicy("ReadPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "carselect.api.read");
    }
)
    .AddPolicy("WritePolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "carselect.api.write");
    }
)
    .AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireRole("admin");
        policy.RequireClaim("scope", "carselect.api.read");
    }
);

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("QuoteCreationLimiter", limiterOptions =>
    {
        // A max of 10 request each minute, this limit is global meaning the following:
        // if User A sends in 9 request and User B sends 1, user B will be the one getting rate limited
        // We could make splits the users based on their IP-adresses but keeping it simple for now
        // TODO: reassess after presentation if need be 
        limiterOptions.PermitLimit = 10;
        limiterOptions.Window = TimeSpan.FromMinutes(1);
        limiterOptions.QueueLimit = 0;
    });
});

// Stripe
builder.Services.Configure<StripeSettings>(
    builder.Configuration.GetSection("Stripe"));

var stripeSettings = builder.Configuration.GetSection("Stripe").Get<StripeSettings>();
StripeConfiguration.ApiKey = stripeSettings!.SecretKey;

// Added this in because I realized that the data is being stored in IdentityServer
// Which in turn makes my User SQL table redunent
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


// Azure SQL Connection
builder.Services.AddDbContext<CarSelectDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));



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

// builder.Services.Configure<ReviewRepositoryOptions>(
//     builder.Configuration.GetSection("ReviewRepositoryOptions")
// );

// builder.Services.Configure<UserRepositoryOptions>(
//     builder.Configuration.GetSection("UserRepositoryOptions")
// );

builder.Services.Configure<BlobStorageRepositoryOptions>(
    builder.Configuration.GetSection("BlobStorageRepositoryOptions")
);

// Cosmos Implementations 
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IListingService, ListingService>();
builder.Services.AddScoped<ICarImageService, CarImageService>();


// Old Implemention (cosmos) - Needs to be removed when SQL Refactor is completed
// builder.Services.AddScoped<IUserService, UserService>();
// builder.Services.AddScoped<IReviewService, ReviewService>();
// builder.Services.AddScoped<IFavoriteService, FavoriteService>();

// The Review Service
builder.Services.AddScoped<IReviewAggregatorService, ReviewAggregatorService>();


// SQL Service Refactor
// builder.Services.AddScoped<IUserSqlService, UserSqlService>();
builder.Services.AddScoped<IFavoriteSqlService, FavoriteSqlService>();
builder.Services.AddScoped<ITransactionSqlService, TransactionSqlService>();

builder.Services.AddHttpClient<ICarService, CarService>(client =>
{
    client.BaseAddress = new Uri("https://vpic.nhtsa.dot.gov/api/vehicles/");
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json")
        );
});


// Cosmos Implementations 
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IListingRepository, ListingRepository>();
builder.Services.AddScoped<ICarImageRepository, CarImageRepository>();

// Old Implemention (cosmos) - Needs to be removed when SQL Refactor is completed
// builder.Services.AddScoped<IUserRepository, UserRepository>();
// builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
// builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();

// SQL Refactoring 
// builder.Services.AddScoped<IUserSqlRepository, UserSqlRepository>();
builder.Services.AddScoped<IFavoriteSqlRepository, FavoriteSqlRepository>();
builder.Services.AddScoped<ITransactionSqlRepository, TransactionSqlRepository>();
builder.Services.AddScoped<IBlobStorageRepository, BlobStorageRepository>();

builder.Services.AddHttpClient();
builder.Services.AddControllers();
// Converts enums to strings and vice versa even ignoring capitilizations
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));


var app = builder.Build();

app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();