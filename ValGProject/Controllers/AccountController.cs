using Microsoft.AspNetCore.Mvc;
using ValGProject.Models;

namespace ValGProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly Data.ValGProjectContext db;

        public AccountController(Data.ValGProjectContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            if(ModelState.IsValid)
            {
                if(db.Users.Any(x => x.UserName == user.UserName && x.Password == user.Password))
                {
                    string s = Utility.Encode(user.UserName);
                    return RedirectToAction("Index", "Form", new {userName = s});
                }
                else
                {
                    ModelState.AddModelError("General", "User Name And Password combination does not exist.");
                    return View(user);
                }
                
            }  
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Registration(User user)
        {
            if (ModelState.IsValid)
            {
                if (!db.Users.Any(x => x.UserName == user.UserName))
                {
                    db.Users.Add(user);
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index", "Form");
                }
                else
                {
                    ModelState.AddModelError("General", "User Name already exists.");
                    return View(user);
                }
            }
            return View(user);
        }
    }
}
