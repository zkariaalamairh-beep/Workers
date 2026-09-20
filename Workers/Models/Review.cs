using System.ComponentModel.DataAnnotations;

namespace Workers.Models;

public class Review
{
    public long Id { get; set; }

    public long ServiceRequestId { get; set; }

    [Range(1, 5)]
    public byte Rating { get; set; }

    [MaxLength(1000)]
    public string? Comment { get; set; }

    public DateTimeOffset CreatedAtUtc { get; set; }
        = DateTimeOffset.UtcNow;

    public ServiceRequest ServiceRequest { get; set; } = null!;
}