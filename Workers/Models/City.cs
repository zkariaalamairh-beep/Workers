using System.ComponentModel.DataAnnotations;

namespace Workers.Models;

public class City
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string NameAr { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string NameEn { get; set; } = string.Empty;

    public int CountryId { get; set; }

    public bool IsActive { get; set; } = true;

    public Country Country { get; set; } = null!;

    public ICollection<ApplicationUser> Users { get; set; }
        = new List<ApplicationUser>();
}