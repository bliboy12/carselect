
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

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
