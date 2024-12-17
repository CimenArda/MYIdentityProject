using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            UserViewModel model = new UserViewModel();

            model.Email = user.Email;
            model.NameSurname = user.NameSurname;
            model.UserName = user.UserName;
            model.ImageUrl = user.ImageUrl;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserViewModel model)
        {
          

            // Kullanıcıyı bulun
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                return View(model);
            }

            // Kullanıcı bilgilerini güncelleyin
            user.NameSurname = model.NameSurname;
            user.Email = model.Email;
            user.UserName = model.UserName;
            user.ImageUrl = model.ImageUrl;
          
            // Şifreyi isteğe bağlı olarak güncelleyin
            if (!string.IsNullOrEmpty(model.Password))
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
            }

            // Kullanıcıyı güncelleyin
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            // Hata durumunda mesaj ekleyin
            ModelState.AddModelError("", "Kullanıcı güncellenirken bir hata oluştu.");
            return View(model);
        }


    }
}
