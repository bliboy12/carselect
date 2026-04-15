using System.ComponentModel;

public enum DriveType
{
    [Description("4wd")]
    FourWd,
    [Description("fwd")]
    Fwd,
    [Description("rwd")]
    Rwd,
    [Description("awd")]
    Awd,
}