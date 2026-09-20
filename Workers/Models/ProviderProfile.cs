using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace Workers.Models;

public class ProviderProfile
{
    [Key]
    public string UserId { get; set; } = string.Empty;

    public ProviderKind Kind { get; set; }

    [MaxLength(2000)]
    public string? Bio { get; set; }

    [Range(0, 80)]
    public int ExperienceYears { get; set; }

    public bool IsVerified { get; set; }

    public bool IsAvailable { get; set; } = true;

    [Range(1, 500)]
    public double ServiceRadiusKm { get; set; } = 20;

    /*
     * الموقع الذي يعمل منه مقدم الخدمة.
     * X = Longitude
     * Y = Latitude
     */
    public Point? WorkLocation { get; set; }

    public ApplicationUser User { get; set; } = null!;

    public ICollection<ProviderService> Services { get; set; }
        = new List<ProviderService>();

    public ICollection<ServiceRequest> Requests { get; set; }
        = new List<ServiceRequest>();
}