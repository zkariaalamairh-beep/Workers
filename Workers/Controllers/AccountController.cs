using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Workers.Models;
using Workers.ViewModels;

namespace Workers.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Register(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Home");
        }

        return View(new RegisterViewModel
        {
            ReturnUrl = returnUrl
        });
    }

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(
        RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        string email = model.Email.Trim();

        var existingUser =
            await _userManager.FindByEmailAsync(email);

        if (existingUser is not null)
        {
            ModelState.AddModelError(
                nameof(model.Email),
                "البريد الإلكتروني مستخدم مسبقًا.");

            return View(model);
        }

        var user = new ApplicationUser
        {
            FullName = model.FullName.Trim(),
            UserName = email,
            Email = email,
            PhoneNumber = string.IsNullOrWhiteSpace(
                model.PhoneNumber)
                ? null
                : model.PhoneNumber.Trim(),

            IsActive = true,
            CreatedAtUtc = DateTimeOffset.UtcNow
        };

        IdentityResult result =
            await _userManager.CreateAsync(
                user,
                model.Password);

        if (!result.Succeeded)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError(
                    string.Empty,
                    GetIdentityErrorMessage(error));
            }

            return View(model);
        }

        await _signInManager.SignInAsync(
            user,
            isPersistent: false);

        TempData["SuccessMessage"] =
            "تم إنشاء حسابك بنجاح، أهلًا بك.";

        return RedirectToLocal(model.ReturnUrl);
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Home");
        }

        return View(new LoginViewModel
        {
            ReturnUrl = returnUrl
        });
    }

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(
        LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        string email = model.Email.Trim();

        ApplicationUser? user =
            await _userManager.FindByEmailAsync(email);

        if (user is null || !user.IsActive)
        {
            ModelState.AddModelError(
                string.Empty,
                "البريد الإلكتروني أو كلمة المرور غير صحيحة.");

            return View(model);
        }
        var result =
            await _signInManager.PasswordSignInAsync(
                user,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: true);

        if (result.Succeeded)
        {
            return RedirectToLocal(model.ReturnUrl);
        }

        if (result.IsLockedOut)
        {
            ModelState.AddModelError(
                string.Empty,
                "تم إيقاف تسجيل الدخول مؤقتًا بسبب تكرار المحاولات. حاول لاحقًا.");

            return View(model);
        }

        ModelState.AddModelError(
            string.Empty,
            "البريد الإلكتروني أو كلمة المرور غير صحيحة.");

        return View(model);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        TempData["SuccessMessage"] =
            "تم تسجيل الخروج بنجاح.";

        return RedirectToAction("Index", "Home");
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }

    private IActionResult RedirectToLocal(string? returnUrl)
    {
        if (!string.IsNullOrWhiteSpace(returnUrl) &&
            Url.IsLocalUrl(returnUrl))
        {
            return LocalRedirect(returnUrl);
        }

        return RedirectToAction("Index", "Home");
    }

    private static string GetIdentityErrorMessage(
        IdentityError error)
    {
        return error.Code switch
        {
            "DuplicateEmail" =>
                "البريد الإلكتروني مستخدم مسبقًا.",

            "DuplicateUserName" =>
                "البريد الإلكتروني مستخدم مسبقًا.",

            "PasswordTooShort" =>
                "كلمة المرور قصيرة جدًا.",

            "PasswordRequiresDigit" =>
                "يجب أن تحتوي كلمة المرور على رقم واحد على الأقل.",

            "PasswordRequiresNonAlphanumeric" =>
                "يجب أن تحتوي كلمة المرور على رمز.",

            _ => error.Description
        };
    }
}