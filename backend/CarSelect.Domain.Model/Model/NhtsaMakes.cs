public class NhtsaMakes
{
    public int Count { get; set; }
    public string Message { get; set; }
    public string SearchCriteria { get; set; }
    public IEnumerable<CarMakesModel> Results { get; set; } = [];
}