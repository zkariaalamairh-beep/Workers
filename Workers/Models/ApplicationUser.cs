using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace Workers.Models;

public class ApplicationUser : IdentityUser
{
    [Required, MaxLength(150)]
    public string FullName { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? ProfileImageUrl { get; set; }

    public int? CountryId { get; set; }

    public int? CityId { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTimeOffset CreatedAtUtc { get; set; } = DateTimeOffset.UtcNow;

    public Country? Country { get; set; }

    public City? City { get; set; }

    public ProviderProfile? ProviderProfile { get; set; }

    public ICollection<ServiceRequest> CustomerRequests { get; set; }
        = new List<ServiceRequest>();
}