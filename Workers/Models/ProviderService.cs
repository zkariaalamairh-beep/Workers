using System.ComponentModel.DataAnnotations;

namespace Workers.Models;

public class ProviderService
{
    public int Id { get; set; }

    [Required]
    public string ProviderId { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    [Required, MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    public PriceUnit PriceUnit { get; set; }

    public decimal? BasePrice { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTimeOffset CreatedAtUtc { get; set; } = DateTimeOffset.UtcNow;

    public ProviderProfile Provider { get; set; } = null!;

    public ServiceCategory Category { get; set; } = null!;

    public ICollection<ServiceRequest> Requests { get; set; }
        = new List<ServiceRequest>();
}