using Workers.Models;

namespace Workers.ViewModels;

public class ProviderCardViewModel
{
    public string UserId { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string? ProfileImageUrl { get; set; }

    public string? Bio { get; set; }

    public string? CityName { get; set; }

    public ProviderKind Kind { get; set; }

    public int ExperienceYears { get; set; }

    public bool IsVerified { get; set; }

    public double AverageRating { get; set; }

    public int ReviewsCount { get; set; }

    public double? DistanceKm { get; set; }

    public List<string> FeaturedServices { get; set; } = new();
}

public class ProviderDetailsViewModel : ProviderCardViewModel
{
    public string? CountryName { get; set; }

    public bool IsAvailable { get; set; }

    public double ServiceRadiusKm { get; set; }

    public List<ProviderServiceItemViewModel> Services { get; set; } = new();

    public List<ReviewItemViewModel> Reviews { get; set; } = new();
}

public class ProviderServiceItemViewModel
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public PriceUnit PriceUnit { get; set; }

    public decimal? BasePrice { get; set; }
}

public class ReviewItemViewModel
{
    public byte Rating { get; set; }

    public string? Comment { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public DateTimeOffset CreatedAtUtc { get; set; }
}