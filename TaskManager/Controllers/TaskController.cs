using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Data;
using Microsoft.EntityFrameworkCore;
using NinjaNye.SearchExtensions;

namespace TaskManager.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskManagerDbcontext context;

        public TaskController(TaskManagerDbcontext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View(context.Tasks.ToList());
        }
        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }
        public IActionResult ChangeStatus(int id)
        {
            var task = context.Tasks.Find(id);
            if (task == null) return NotFound();
            
            task.IsCompleted = !task.IsCompleted;
            context.SaveChanges();

            return RedirectToAction("Index");

        }
        public IActionResult Delete(int id)
        {
            var task = context.Tasks.Find(id);
            if (task == null) return NotFound();
            context.Tasks.Remove(task);
            context.SaveChanges();

            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Create() 
        { 
            return View();
        }
        [HttpPost]
        public IActionResult Create(TaskModel task)
        {

            if (!ModelState.IsValid) return View("Create");

            context.Tasks.Add(task);
            context.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult SortByName()
        {
            return View("Index", context.Tasks.ToList().OrderBy(task => task.Name));
        }
        public IActionResult SortByDeadline() 
        {
            return View("Index", context.Tasks.ToList().OrderBy(task => task.Deadline));
        }
        public IActionResult SortByPriority()
        {
            return View("Index", context.Tasks.ToList().OrderBy(task => task.Priority));
        }
        public IActionResult SortByStatus()
        {
            return View("Index", context.Tasks.ToList().OrderBy(task => task.IsCompleted));
        }
        public IActionResult Search(string searchstring)
        {
            if (string.IsNullOrEmpty(searchstring))
            { return NotFound(); }
            
            return View("Index", context.Tasks.Search(x => x.Name).Containing(searchstring).ToList());
        } 
        
    }
}
