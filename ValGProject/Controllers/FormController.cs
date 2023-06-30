using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using ValGProject.Models;

namespace ValGProject.Controllers
{
    public class FormController : Controller
    {
        private readonly Data.ValGProjectContext db;
        private readonly ILogger<Controller> _logger;

        public FormController(Data.ValGProjectContext context, ILogger<Controller> logger)
        {
            db = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            if (TempData["user"] == null || string.IsNullOrEmpty(TempData["user"].ToString()))
            {
                return RedirectToAction("Login", "Account", new { message = "fail" });
            }

            try
            {
                List<Topic> topics = await db.Topics.ToListAsync();
                return View(topics);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return View(new List<Topic>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (TempData["user"] == null || string.IsNullOrEmpty(TempData["user"].ToString()))
            {
                return RedirectToAction("Login", "Account", new { message = "fail" });
            }

            Topic topic = new()
            {
                Creator = Utility.Decode(TempData["user"].ToString())
            };
            return View(topic);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Topic topic)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    topic.CreatonDate = DateTime.Now;
                    db.Topics.Add(topic);
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                return View(topic);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return View(topic);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (TempData["user"] == null || string.IsNullOrEmpty(TempData["user"].ToString()))
            {
                return RedirectToAction("Login", "Account", new { message = "fail" });
            }

            Topic topic = await db.Topics.FindAsync(id);

            if(topic != null && (Utility.Decode(TempData["user"].ToString()) != topic.Creator))
            {
                return RedirectToAction("Index");
            }

            return View(topic);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Topic topic)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(topic).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return View(topic);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (TempData["user"] == null || string.IsNullOrEmpty(TempData["user"].ToString()))
            {
                return RedirectToAction("Login", "Account", new {message="fail"});
            }

            Topic topic = await db.Topics.FindAsync(id);

            if (topic != null && (Utility.Decode(TempData["user"].ToString()) != topic.Creator))
            {
                return RedirectToAction("Index");
            }

            ViewBag.message = @"<script type='text/javascript' language='javascript'>alert(""You're about to delete a topic. Please review it befor doing so."")</script>";
            return View(topic);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Topic topic)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Topics.Remove(topic);
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return View(topic);
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            if (TempData["user"] == null || string.IsNullOrEmpty(TempData["user"].ToString()))
            {
                return RedirectToAction("Login", "Account", new { message = "fail" });
            }
            return View();
        }

        [HttpPost]
        public IActionResult LogOut(Topic topic)
        {
            TempData["user"] = null;

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult DeleteUser()
        {
            if (TempData["user"] == null || string.IsNullOrEmpty(TempData["user"].ToString()))
            {
                return RedirectToAction("Login", "Account", new { message = "fail" });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(Topic topic)
        {
            try
            {
                User user = await db.Users.FirstOrDefaultAsync(user => user.UserName == Utility.Decode(TempData["user"].ToString()));

                if(user != null)
                {
                    db.Users.Remove(user);
                    await db.SaveChangesAsync();

                    TempData["user"] = null;
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return View();
        }
    }
}
