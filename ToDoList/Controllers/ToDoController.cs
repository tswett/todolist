using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ToDoController : Controller
    {
        private ToDoItemDbContext db = new ToDoItemDbContext();

        // GET: ToDo
        public ActionResult Index()
        {
            return View(db.ToDoItems.ToList());
        }

        // Exposing this method like this doesn't feel like good architecture.
        // I think my next change would be to create a view model class containing this message.
        public static string GetFeasibilityMessage(IEnumerable<ToDoItem> allItems)
        {
            // Get all items yet to be done, in order of deadline.
            IEnumerable<ToDoItem> items = allItems
                .Where(item => item.Status != ToDoItem.ItemStatus.Done)
                .Where(item => item.Deadline.HasValue)
                .OrderBy(item => item.Deadline.Value);

            // Assume that we're starting at the current time...
            DateTime currentTime = DateTime.Now;

            // ...then loop through items, assuming we perform them in order of deadline.
            foreach (ToDoItem item in items)
            {
                currentTime = currentTime.AddDays(item.DaysRequired);

                if (currentTime > item.Deadline.Value)
                {
                    return string.Format(
                        "No, the item \"{0}\" has a deadline of {1}, but will not be completed " +
                            "until {2}.",
                        item.Content,
                        item.Deadline,
                        currentTime);
                }
            }

            return string.Format("Yes, all items will be completed by {0}.", currentTime);
        }

        // GET: ToDo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoItem toDoItem = db.ToDoItems.Find(id);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }
            return View(toDoItem);
        }

        // GET: ToDo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Content,Status,DaysRequired,Deadline")] ToDoItem toDoItem)
        {
            if (ModelState.IsValid)
            {
                db.ToDoItems.Add(toDoItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(toDoItem);
        }

        // GET: ToDo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoItem toDoItem = db.ToDoItems.Find(id);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }
            return View(toDoItem);
        }

        // POST: ToDo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Content,Status,DaysRequired,Deadline")] ToDoItem toDoItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toDoItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toDoItem);
        }

        // GET: ToDo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoItem toDoItem = db.ToDoItems.Find(id);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }
            return View(toDoItem);
        }

        // POST: ToDo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToDoItem toDoItem = db.ToDoItems.Find(id);
            db.ToDoItems.Remove(toDoItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
