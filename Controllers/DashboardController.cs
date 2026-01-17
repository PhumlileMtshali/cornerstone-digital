using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CornerstoneDigital.Models;

namespace CornerstoneDigital.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.Identity?.Name;

            // Get user's cart count
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            var cartItemCount = cart?.CartItems.Count ?? 0;

            // Get total services available
            var totalServices = await _context.Services.CountAsync(s => s.IsActive);

            // Get user's actual name from database
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userId);
            var displayName = user?.FirstName ?? User.Identity?.Name?.Split('@')[0] ?? "User";

            ViewBag.CartItemCount = cartItemCount;
            ViewBag.TotalServices = totalServices;
            ViewBag.UserName = displayName;

            return View();
        }
    }
}
