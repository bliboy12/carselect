using System.ComponentModel;
using Newtonsoft.Json;

public enum DriveTypeContract
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