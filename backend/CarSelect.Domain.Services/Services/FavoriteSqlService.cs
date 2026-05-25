public class FavoriteSqlService : IFavoriteSqlService
{
    private readonly IFavoriteSqlRepository _repo;
    private readonly IListingService _listingService;
    private readonly ICarService _carService;

    public FavoriteSqlService(IFavoriteSqlRepository repo, IListingService listingService, ICarService carService)
    {
        _repo = repo;
        _listingService = listingService;
        _carService = carService;
    }

    public async Task<FavoriteModel> CreateFavoriteAsync(Guid userId, Guid listingId)
    {
        FavoriteDataModelSQL newFavorite = new FavoriteDataModelSQL
        {
            UserId = userId,
            ListingId = listingId
        };

        ListingModel getListing = await _listingService.GetListingByIdAsync(listingId);
        FavoriteDataModelSQL result = await _repo.CreateFavoriteAsync(newFavorite);

        FavoriteModel createdFavorite = FavoriteSqlMapper.MapToDomein(result);
        createdFavorite.Listing = getListing;

        return createdFavorite;
    }

    public async Task<IEnumerable<FavoriteModel>> GetAllFavoritesByUserIdAsync(Guid userId)
    {
        IEnumerable<FavoriteDataModelSQL> response = await _repo.GetAllFavoritesByUserIdAsync(userId);
        List<FavoriteModel> favorites = new();

        foreach (FavoriteDataModelSQL favorite in response)
        {
            FavoriteModel newFavorite = FavoriteSqlMapper.MapToDomein(favorite);
            newFavorite.Listing = await _listingService.GetListingByIdAsync(newFavorite.ListingId);
            newFavorite.Listing.Car = await _carService.GetCarByIdAsync(newFavorite.Listing.CarId);
            favorites.Add(newFavorite);
        }

        return favorites;
    }

    public async Task<bool> IsFavoritedAsync(Guid userId, Guid listingId)
    {
        return await _repo.IsFavoritedAsync(userId, listingId);
    }

    public async Task DeleteFavoriteByListingIdAsync(Guid userId, Guid listingId)
    {
        await _repo.DeleteFavoriteByListingIdAsync(userId, listingId);
    }
}