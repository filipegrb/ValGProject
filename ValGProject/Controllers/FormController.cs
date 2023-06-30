using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;
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

        public IActionResult Index(string userName)
        {
            List<Topic> topics = db.Topics.ToList();
            if (!string.IsNullOrEmpty(userName))
            {
                TempData["user"] = Utility.Decode(userName);
            }
            return View(topics);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Topic topic = new Topic
            {
                Creator = TempData["user"] as string
            };
            return View(topic);
        }

        [HttpPost]
        public IActionResult Create(Topic topic)
        {
            if(ModelState.IsValid)
            {
                topic.CreatonDate = DateTime.Now;
                db.Topics.Add(topic);
                db.SaveChanges();

                return RedirectToAction("Index", new {userName = Utility.Encode(topic.Creator)});
            }

            return View(topic);
        }

        [HttpGet]
        public IActionResult Edit(int id, string username)
        {
            
            Topic topic = db.Topics.Find(id);

            if(topic != null && (Utility.Decode(username) != topic.Creator))
            {
                return RedirectToAction("Index", new {userName = Utility.Encode(username) });
            }

            return View(topic);
        }

        [HttpPost]
        public IActionResult Edit(Topic topic)
        {
            if(ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", new {userName = Utility.Encode(topic.Creator)});
            }

            return View(topic);
        }
    }
}
