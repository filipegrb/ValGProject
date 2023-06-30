using Microsoft.AspNetCore.Mvc;
using ValGProject.Models;

namespace ValGProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly Data.ValGProjectContext db;
        private readonly ILogger<Controller> _logger;

        public AccountController(Data.ValGProjectContext context, ILogger<Controller> logger)
        {
            db = context;
            _logger = logger;   
        }

        [HttpGet]
        public IActionResult Login(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                ModelState.AddModelError("General", "User was not logged in.");
            }
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
                    string userName = Utility.Encode(user.UserName);
                    TempData["user"] = userName;
                    return RedirectToAction("Index", "Form");
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
            try
            {
                if (ModelState.IsValid)
                {
                    if (!db.Users.Any(x => x.UserName == user.UserName))
                    {
                        db.Users.Add(user);
                        await db.SaveChangesAsync();

                        string userName = Utility.Encode(user.UserName);
                        TempData["user"] = userName;
                        return RedirectToAction("Index", "Form");
                    }
                    else
                    {
                        ModelState.AddModelError("General", "User Name already exists.");
                        return View(user);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            
            return View(user);
        }
    }
}
