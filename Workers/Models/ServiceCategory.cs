using System.ComponentModel.DataAnnotations;

namespace Workers.Models;

public class ServiceCategory
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string NameAr { get; set; } = string.Empty;

    [Required, MaxLength(120)]
    public string NameEn { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? IconUrl { get; set; }

    public int? ParentId { get; set; }

    public bool IsActive { get; set; } = true;

    public ServiceCategory? Parent { get; set; }

    public ICollection<ServiceCategory> Children { get; set; }
        = new List<ServiceCategory>();

    public ICollection<ProviderService> ProviderServices { get; set; }
        = new List<ProviderService>();
}