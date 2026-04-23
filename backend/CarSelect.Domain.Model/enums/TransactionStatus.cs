using System.ComponentModel;

public enum TransactionStatus
{
    [Description("successful")]
    Successful,
    [Description("pending")]
    Pending,
    [Description("rejected")]
    Rejected
}