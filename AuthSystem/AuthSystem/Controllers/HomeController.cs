using AuthSystem.Areas.Identity.Data;
using AuthSystem.Data;
using AuthSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AuthSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthDbContext _dbContext;
        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, AuthDbContext dbContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var userId = _userManager.GetUserId(this.User);
            var newProfiles = _dbContext.AddProfiles.Where(profile => profile.UserId == userId).ToList();
            ViewData["UserID"] = _userManager.GetUserId(this.User);
            ViewData["FirstName"] = currentUser.FirstName;
            ViewData["LastName"] = currentUser.LastName;
            ViewData["Email"] = currentUser.Email;
            ViewData["LinkedinProfileUrl"] = currentUser.LinkedinProfileUrl;
            ViewData["InstagramProfileUrl"] = currentUser.InstagramProfileUrl;
            ViewData["FacebookProfileUrl"] = currentUser.FacebookProfileUrl;
            return View(newProfiles);
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

        [HttpPost]
        public async Task<IActionResult> Edit(int editItemId, string profileName, string profileUrl)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null)
            {
                var profileToUpdate = _dbContext.AddProfiles.FirstOrDefault(profile => profile.UserId == currentUser.Id && profile.Id == editItemId);
                if (profileToUpdate != null)
                {
                    profileToUpdate.ProfileName = profileName;
                    profileToUpdate.ProfileUrl = profileUrl;
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(Index), "Home");
                }
            }
            return Content("Something Went Wrong");
        }

        [HttpPost]
        public async Task<IActionResult> Save(string profileName, string profileUrl)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var additionalItem = new AddProfile
                {
                    ProfileName = profileName,
                    ProfileUrl = profileUrl,
                    UserId = await _userManager.GetUserIdAsync(currentUser)
                };
                _dbContext.AddProfiles.Add(additionalItem);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("YourFormView");
            }
        }
      [HttpPost]
        public IActionResult Delete(int id)
        {
            var profileToDelete = _dbContext.AddProfiles.Find(id);

            if (profileToDelete != null)
            {
                _dbContext.AddProfiles.Remove(profileToDelete);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }
    }
}



