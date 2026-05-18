public class FavoriteService : IFavoriteService
{
    private readonly IFavoriteRepository _repo;
    private readonly IListingService _listingService;
    private readonly ICarService _carService;
    public FavoriteService(IFavoriteRepository favoriteRepository, IListingService listingService, ICarService carService)
    {
        _repo = favoriteRepository;
        _listingService = listingService;
        _carService = carService;
    }
    public async Task<FavoriteModel> CreateFavoriteAsync(Guid userId, Guid listingId)
    {
        FavoriteDataModel newFavorite = new FavoriteDataModel
        {
            Id = $"{userId}_{listingId}",
            UserId = userId.ToString(),
            ListingId = listingId.ToString()
        };
        var result = await _repo.CreateFavoriteAsync(newFavorite);

        var getListing = await _listingService.GetListingByIdAsync(listingId);

        FavoriteModel createdFavorite = FavoriteMapper.MapToDomein(result);
        createdFavorite.Listing = getListing;

        return createdFavorite;
    }

    public async Task<IEnumerable<FavoriteModel>> GetAllFavoritesByUserIdAsync(Guid userId)
    {
        IEnumerable<FavoriteDataModel> response = await _repo.GetAllFavoritesByUserIdAsync(userId.ToString());
        List<FavoriteModel> favorites = new();

        foreach (FavoriteDataModel favorite in response)
        {
            FavoriteModel newFavorite = FavoriteMapper.MapToDomein(favorite);
            newFavorite.Listing = await _listingService.GetListingByIdAsync(newFavorite.ListingId);
            newFavorite.Listing.Car = await _carService.GetCarByIdAsync(newFavorite.Listing.CarId);
            favorites.Add(newFavorite);
        }

        return favorites;
    }

    public async Task<bool> IsFavoritedAsync(Guid userId, Guid listingId)
    {
        return await _repo.IsFavoritedAsync(userId.ToString(), listingId.ToString());
    }

    public async Task DeleteFavoriteByListingIdAsync(Guid userId, Guid listingId)
    {
        await _repo.DeleteFavoriteAsyncByListingIdAsync(userId.ToString(), listingId.ToString());
    }
}