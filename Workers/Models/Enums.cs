namespace Workers.Models;

public enum ProviderKind
{
    Professional = 1,
    DailyWorker = 2
}

public enum PriceUnit
{
    Fixed = 1,
    Hourly = 2,
    Daily = 3,
    Negotiable = 4
}

public enum ServiceRequestStatus
{
    Pending = 1,
    Accepted = 2,
    InProgress = 3,
    Completed = 4,
    Cancelled = 5,
    Rejected = 6
}