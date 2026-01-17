using Microsoft.AspNetCore.Mvc;
using CornerstoneDigital.Models;
using Microsoft.Extensions.Logging;

namespace CornerstoneDigital.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ContactController> _logger;

        public ContactController(ApplicationDbContext context, ILogger<ContactController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new ContactViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var contactMessage = new ContactMessage
                {
                    Name = model.Name,
                    Email = model.Email,
                    Phone = model.Phone,
                    ServiceInterest = model.ServiceInterest,
                    Message = model.Message
                };

                _context.ContactMessages.Add(contactMessage);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Thank you! We'll contact you within 24 hours.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save contact message for {Email}", model?.Email);
                ModelState.AddModelError(string.Empty, "An internal error occurred. Please try again later.");
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Please fill in all required fields." });
            }

            try
            {
                var contactMessage = new ContactMessage
                {
                    Name = model.Name,
                    Email = model.Email,
                    Phone = model.Phone,
                    ServiceInterest = model.ServiceInterest,
                    Message = model.Message
                };

                _context.ContactMessages.Add(contactMessage);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Thank you! We'll contact you within 24 hours." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save contact message for {Email}", model?.Email);
                return StatusCode(500, new { success = false, message = "An internal error occurred. Please try again later." });
            }
        }
    }
}