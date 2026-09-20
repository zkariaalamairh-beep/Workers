using System.ComponentModel.DataAnnotations;

namespace Workers.Models;

public class Country
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string NameAr { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string NameEn { get; set; } = string.Empty;

    [Required, MaxLength(2)]
    public string Iso2Code { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public ICollection<City> Cities { get; set; } = new List<City>();

    public ICollection<ApplicationUser> Users { get; set; }
        = new List<ApplicationUser>();
}