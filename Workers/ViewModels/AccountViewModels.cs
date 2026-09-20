using System.ComponentModel.DataAnnotations;

namespace Workers.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "الاسم الكامل مطلوب.")]
    [StringLength(
        150,
        MinimumLength = 2,
        ErrorMessage = "الاسم يجب أن يكون بين حرفين و150 حرفًا.")]
    [Display(Name = "الاسم الكامل")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "البريد الإلكتروني مطلوب.")]
    [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح.")]
    [Display(Name = "البريد الإلكتروني")]
    public string Email { get; set; } = string.Empty;

    [Phone(ErrorMessage = "رقم الهاتف غير صحيح.")]
    [Display(Name = "رقم الهاتف")]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "كلمة المرور مطلوبة.")]
    [StringLength(
        100,
        MinimumLength = 8,
        ErrorMessage = "كلمة المرور يجب ألا تقل عن 8 أحرف.")]
    [DataType(DataType.Password)]
    [Display(Name = "كلمة المرور")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "تأكيد كلمة المرور مطلوب.")]
    [DataType(DataType.Password)]
    [Compare(
        nameof(Password),
        ErrorMessage = "كلمتا المرور غير متطابقتين.")]
    [Display(Name = "تأكيد كلمة المرور")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public string? ReturnUrl { get; set; }
}

public class LoginViewModel
{
    [Required(ErrorMessage = "البريد الإلكتروني مطلوب.")]
    [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح.")]
    [Display(Name = "البريد الإلكتروني")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "كلمة المرور مطلوبة.")]
    [DataType(DataType.Password)]
    [Display(Name = "كلمة المرور")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "تذكرني")]
    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }
}