using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using Workers.Data;
using Workers.Models;
using Workers.ViewModels;

namespace Workers.Controllers;

public class ProvidersController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProvidersController(ApplicationDbContext context)
    {
        _context = context;
    }

    // عرض جميع مقدمي الخدمات
    [HttpGet]
    public async Task<IActionResult> Index(
        int? categoryId,
        int? cityId,
        ProviderKind? kind,
        string? search)
    {
        var query = _context.ProviderProfiles
            .AsNoTracking()
            .Where(provider =>
                provider.User.IsActive &&
                provider.IsAvailable);

        if (categoryId.HasValue)
        {
            query = query.Where(provider =>
                provider.Services.Any(service =>
                    service.IsActive &&
                    service.CategoryId == categoryId.Value));
        }

        if (cityId.HasValue)
        {
            query = query.Where(provider =>
                provider.User.CityId == cityId.Value);
        }

        if (kind.HasValue)
        {
            query = query.Where(provider =>
                provider.Kind == kind.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            string searchText = search.Trim();

            query = query.Where(provider =>
                provider.User.FullName.Contains(searchText) ||
                (provider.Bio != null &&
                 provider.Bio.Contains(searchText)) ||
                provider.Services.Any(service =>
                    service.IsActive &&
                    (
                        service.Title.Contains(searchText) ||
                        service.Category.NameAr.Contains(searchText)
                    )));
        }

        var providers = await query
            .Select(provider => new ProviderCardViewModel
            {
                UserId = provider.UserId,
                FullName = provider.User.FullName,
                ProfileImageUrl = provider.User.ProfileImageUrl,
                Bio = provider.Bio,

                CityName = provider.User.City != null
                    ? provider.User.City.NameAr
                    : null,

                Kind = provider.Kind,
                ExperienceYears = provider.ExperienceYears,
                IsVerified = provider.IsVerified,

                AverageRating = provider.Requests
                    .Where(request => request.Review != null)
                    .Select(request =>
                        (double?)request.Review!.Rating)
                    .Average() ?? 0,

                ReviewsCount = provider.Requests
                    .Count(request => request.Review != null),

                FeaturedServices = provider.Services
                    .Where(service => service.IsActive)
                    .OrderBy(service => service.Id)
                    .Select(service => service.Title)
                    .Take(3)
                    .ToList()
            })
            .OrderByDescending(provider => provider.IsVerified)
            .ThenByDescending(provider => provider.AverageRating)
            .ToListAsync();

        return View(providers);
    }

    // عرض مقدمي الخدمات الأقرب للمستخدم
    [HttpGet]
    public async Task<IActionResult> Nearby(
        double latitude,
        double longitude,
        double maxDistanceKm = 20,
        int? categoryId = null,
        ProviderKind? kind = null)
    {
        if (latitude is < -90 or > 90)
        {
            return BadRequest("خط العرض غير صحيح.");
        }

        if (longitude is < -180 or > 180)
        {
            return BadRequest("خط الطول غير صحيح.");
        }

        maxDistanceKm = Math.Clamp(maxDistanceKm, 1, 100);

        // X = Longitude
        // Y = Latitude
        var currentLocation = new Point(longitude, latitude)
        {
            SRID = 4326
        };

        var query = _context.ProviderProfiles
            .AsNoTracking()
            .Where(provider =>
                provider.User.IsActive &&
                provider.IsAvailable &&
                provider.WorkLocation != null);

        if (categoryId.HasValue)
        {
            query = query.Where(provider =>
                provider.Services.Any(service =>
                    service.IsActive &&
                    service.CategoryId == categoryId.Value));
        }

        if (kind.HasValue)
        {
            query = query.Where(provider =>
                provider.Kind == kind.Value);
        }

        query = query.Where(provider =>
            provider.WorkLocation!.Distance(currentLocation)
                <= maxDistanceKm * 1000 &&
            provider.WorkLocation.Distance(currentLocation)
                <= provider.ServiceRadiusKm * 1000);

        var providers = await query
            .Select(provider => new ProviderCardViewModel
            {
                UserId = provider.UserId,
                FullName = provider.User.FullName,
                ProfileImageUrl = provider.User.ProfileImageUrl,
                Bio = provider.Bio,

                CityName = provider.User.City != null
                    ? provider.User.City.NameAr
                    : null,

                Kind = provider.Kind,
                ExperienceYears = provider.ExperienceYears,
                IsVerified = provider.IsVerified,

                DistanceKm =
                    provider.WorkLocation!.Distance(currentLocation) / 1000.0,

                AverageRating = provider.Requests
                    .Where(request => request.Review != null)
                    .Select(request =>
                        (double?)request.Review!.Rating)
                    .Average() ?? 0,

                ReviewsCount = provider.Requests
                    .Count(request => request.Review != null),

                FeaturedServices = provider.Services
                    .Where(service => service.IsActive)
                    .OrderBy(service => service.Id)
                    .Select(service => service.Title)
                    .Take(3)
                    .ToList()
            })
            .OrderBy(provider => provider.DistanceKm)
            .ToListAsync();

        ViewBag.MaxDistanceKm = maxDistanceKm;

        return View(providers);
    }

    // صفحة تفاصيل مقدم الخدمة
    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return NotFound();
        }

        var provider = await _context.ProviderProfiles
            .AsNoTracking()
            .Where(item =>
                item.UserId == id &&
                item.User.IsActive)
            .Select(item => new ProviderDetailsViewModel
            {
                UserId = item.UserId,
                FullName = item.User.FullName,
                ProfileImageUrl = item.User.ProfileImageUrl,
                Bio = item.Bio,

                CountryName = item.User.Country != null
                    ? item.User.Country.NameAr
                    : null,

                CityName = item.User.City != null
                    ? item.User.City.NameAr
                    : null,

                Kind = item.Kind,
                ExperienceYears = item.ExperienceYears,
                IsVerified = item.IsVerified,
                IsAvailable = item.IsAvailable,
                ServiceRadiusKm = item.ServiceRadiusKm,

                AverageRating = item.Requests
                    .Where(request => request.Review != null)
                    .Select(request =>
                        (double?)request.Review!.Rating)
                    .Average() ?? 0,

                ReviewsCount = item.Requests
                    .Count(request => request.Review != null),

                Services = item.Services
                    .Where(service => service.IsActive)
                    .Select(service =>
                        new ProviderServiceItemViewModel
                        {
                            Id = service.Id,
                            Title = service.Title,
                            Description = service.Description,
                            CategoryName = service.Category.NameAr,
                            PriceUnit = service.PriceUnit,
                            BasePrice = service.BasePrice
                        })
                    .ToList(),

                Reviews = item.Requests
                    .Where(request => request.Review != null)
                    .OrderByDescending(request =>
                        request.Review!.CreatedAtUtc)
                    .Take(20)
                    .Select(request => new ReviewItemViewModel
                    {
                        Rating = request.Review!.Rating,
                        Comment = request.Review.Comment,
                        CreatedAtUtc = request.Review.CreatedAtUtc,
                        CustomerName = request.Customer.FullName
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();

        if (provider is null)
        {
            return NotFound();
        }

        return View(provider);
    }
}