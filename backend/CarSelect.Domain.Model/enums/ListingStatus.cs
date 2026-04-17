using System.ComponentModel;

public enum ListingStatus
{
    [Description("active")]
    Active = 1,
    [Description("sold")]
    Sold = 2,
    [Description("removed")]
    Removed = 3
}