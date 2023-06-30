using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValGProject.Models;

namespace ValGProject.Controllers
{
    public class FormController : Controller
    {
        private readonly Data.ValGProjectContext db;

        public FormController(Data.ValGProjectContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index(string userName)
        {
            List<Topic> topics = await db.Topics.ToListAsync();
            if (!string.IsNullOrEmpty(userName))
            {
                TempData["user"] = Utility.Decode(userName);
            }
            return View(topics);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Topic topic = new()
            {
                Creator = TempData["user"] as string
            };
            return View(topic);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Topic topic)
        {
            if(ModelState.IsValid)
            {
                topic.CreatonDate = DateTime.Now;
                db.Topics.Add(topic);
                await db.SaveChangesAsync();

                return RedirectToAction("Index", new {userName = Utility.Encode(topic.Creator)});
            }

            return View(topic);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, string username)
        {
            
            Topic topic = await db.Topics.FindAsync(id);

            if(topic != null && (Utility.Decode(username) != topic.Creator))
            {
                return RedirectToAction("Index", new {userName = Utility.Encode(username) });
            }

            return View(topic);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Topic topic)
        {
            if(ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index", new {userName = Utility.Encode(topic.Creator)});
            }

            return View(topic);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, string username)
        {

            Topic topic = await db.Topics.FindAsync(id);

            if (topic != null && (Utility.Decode(username) != topic.Creator))
            {
                return RedirectToAction("Index", new { userName = Utility.Encode(username) });
            }

            return View(topic);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Topics.Remove(topic);
                await db.SaveChangesAsync();

                return RedirectToAction("Index", new { userName = Utility.Encode(topic.Creator) });
            }

            return View(topic);
        }
    }
}
