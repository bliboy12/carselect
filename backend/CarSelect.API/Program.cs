
var builder = WebApplication.CreateBuilder(args);

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

builder.Services.Configure<PurchaseRepositoryOptions>(
    builder.Configuration.GetSection("PurchaseRepositoryOptions")
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

builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IListingService, ListingService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICarImageService, CarImageService>();

builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IListingRepository, ListingRepository>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICarImageRepository, CarImageRepository>();

builder.Services.AddScoped<IBlobStorageRepository, BlobStorageRepository>();

builder.Services.AddHttpClient();
builder.Services.AddControllers();


var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
