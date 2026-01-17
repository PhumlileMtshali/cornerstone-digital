using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CornerstoneDigital.Models;
using System.Threading.Tasks;

namespace CornerstoneDigital.ViewComponents
{
    public class UserDisplayNameViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public UserDisplayNameViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var userName = User.Identity.Name;
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

                var displayName = user?.FirstName ?? userName?.Split('@')[0] ?? "User";
                return Content(displayName);
            }

            return Content("User");
        }
    }
}
