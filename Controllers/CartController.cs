using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CornerstoneDigital.Models;
using System.Security.Claims;

namespace CornerstoneDigital.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            var cart = await GetOrCreateCartAsync();
            return View(cart);
        }

        // POST: Cart/AddToCart
        [HttpPost]
        public async Task<IActionResult> AddToCart(int serviceId, string? customizationNotes = null)
        {
            var service = await _context.Services.FindAsync(serviceId);
            if (service == null || !service.IsActive)
            {
                return NotFound();
            }

            var cart = await GetOrCreateCartAsync();

            // Check if item already exists in cart
            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ServiceId == serviceId);
            if (existingItem != null)
            {
                existingItem.Quantity++;
                existingItem.CustomizationNotes = customizationNotes;
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ServiceId = serviceId,
                    Quantity = 1,
                    Price = service.Price,
                    CustomizationNotes = customizationNotes
                };
                _context.CartItems.Add(cartItem);
            }

            cart.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"{service.Name} added to cart!";
            return RedirectToAction(nameof(Index));
        }

        // POST: Cart/UpdateQuantity
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity)
        {
            if (quantity < 1)
            {
                return BadRequest("Quantity must be at least 1");
            }

            var cart = await GetOrCreateCartAsync();
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);

            if (cartItem == null)
            {
                return NotFound();
            }

            cartItem.Quantity = quantity;
            cart.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST: Cart/RemoveFromCart
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var cart = await GetOrCreateCartAsync();
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);

            if (cartItem == null)
            {
                return NotFound();
            }

            _context.CartItems.Remove(cartItem);
            cart.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Item removed from cart";
            return RedirectToAction(nameof(Index));
        }

        // POST: Cart/ClearCart
        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            var cart = await GetOrCreateCartAsync();
            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Cart cleared";
            return RedirectToAction(nameof(Index));
        }

        // GET: Cart/Checkout
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var cart = await GetOrCreateCartAsync();

            if (!cart.CartItems.Any())
            {
                TempData["ErrorMessage"] = "Your cart is empty";
                return RedirectToAction(nameof(Index));
            }

            return View(cart);
        }

        // POST: Cart/ProcessCheckout
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProcessCheckout(string projectDetails)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await GetOrCreateCartAsync();

            if (!cart.CartItems.Any())
            {
                TempData["ErrorMessage"] = "Your cart is empty";
                return RedirectToAction(nameof(Index));
            }

            // Create orders for each cart item
            foreach (var item in cart.CartItems)
            {
                var order = new Order
                {
                    UserId = userId,
                    ServiceId = item.ServiceId,
                    Amount = item.Price * item.Quantity,
                    Status = "Pending",
                    ProjectDetails = projectDetails ?? item.CustomizationNotes,
                    OrderDate = DateTime.Now
                };
                _context.Orders.Add(order);
            }

            // Clear the cart
            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Order placed successfully! We'll contact you shortly.";
            return RedirectToAction("Index", "Home");
        }

        // Helper method to get or create cart
        private async Task<Cart> GetOrCreateCartAsync()
        {
            string sessionId;

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Service)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                if (cart == null)
                {
                    cart = new Cart { UserId = userId };
                    _context.Carts.Add(cart);
                    await _context.SaveChangesAsync();
                }

                return cart;
            }
            else
            {
                // For anonymous users, use session
                sessionId = HttpContext.Session.GetString("CartSessionId");
                if (string.IsNullOrEmpty(sessionId))
                {
                    sessionId = Guid.NewGuid().ToString();
                    HttpContext.Session.SetString("CartSessionId", sessionId);
                }

                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Service)
                    .FirstOrDefaultAsync(c => c.UserId == sessionId);

                if (cart == null)
                {
                    cart = new Cart { UserId = sessionId };
                    _context.Carts.Add(cart);
                    await _context.SaveChangesAsync();
                }

                return cart;
            }
        }

        // API endpoint to get cart count
        [HttpGet]
        public async Task<IActionResult> GetCartCount()
        {
            var cart = await GetOrCreateCartAsync();
            var count = cart.CartItems.Sum(ci => ci.Quantity);
            return Json(new { count });
        }
    }
}
