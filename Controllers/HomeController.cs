using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AliMertKelimeEzberleme.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AliMertKelimeEzberleme.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null && user.PreferredLanguageId.HasValue)
                {
                    var cards = await _context.Flashcards
                        .Where(f => f.LanguageId == user.PreferredLanguageId.Value)
                        .ToListAsync();
                    
                    var lang = await _context.Languages.FindAsync(user.PreferredLanguageId.Value);
                    ViewBag.LanguageName = lang?.Name;

                    return View("Dashboard_User", cards);
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
