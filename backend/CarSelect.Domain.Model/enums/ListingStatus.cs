using System.ComponentModel;

public enum ListingStatus
{
    [Description("active")]
    Active,
    [Description("sold")]
    Sold,
    [Description("removed")]
    Removed
}