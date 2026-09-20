using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace Workers.Models;

public class ServiceRequest
{
    public long Id { get; set; }

    [Required]
    public string CustomerId { get; set; } = string.Empty;

    [Required]
    public string ProviderId { get; set; } = string.Empty;

    public int? ProviderServiceId { get; set; }

    [Required, MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    public DateTimeOffset? RequestedForUtc { get; set; }

    [MaxLength(500)]
    public string? AddressText { get; set; }

    public Point? ServiceLocation { get; set; }

    public decimal? CustomerBudget { get; set; }

    public decimal? AgreedPrice { get; set; }

    public ServiceRequestStatus Status { get; set; }
        = ServiceRequestStatus.Pending;

    [MaxLength(500)]
    public string? CancellationReason { get; set; }

    public DateTimeOffset CreatedAtUtc { get; set; }
        = DateTimeOffset.UtcNow;

    public DateTimeOffset? UpdatedAtUtc { get; set; }

    public ApplicationUser Customer { get; set; } = null!;

    public ProviderProfile Provider { get; set; } = null!;

    public ProviderService? ProviderService { get; set; }

    public Review? Review { get; set; }
}