using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using PagedList;
namespace SEA.Areas.Admin.Controllers
{

    public class TopicAdminController : Controller
    {
        MyBlogEntities _db;
        public TopicAdminController()
        {
            _db = new MyBlogEntities();
        }
        // GET: Admin/TopicAdmin
        public ActionResult Index(int page= 1 ,int pageSize = 10)
        {
            return View(_db.Topics.OrderBy(x=>x.Id).ToPagedList<Topic>(page,pageSize) as PagedList<Topic>);
        }

        // GET: Admin/TopicAdmin/Details/5
        public ActionResult Details(int id)
        {
            var topic = _db.Topics.Find(id);
            return View(topic);
        }

        public ActionResult ListTopicByLanguage(string id)
        {
            var t = _db.Topics.Where(x => x.LangId == id).ToList();
            ViewBag.Select = new SelectList(t, "Name", "Name");
            return View(new SelectList(t, "Name", "Name"));
        }

        // GET: Admin/TopicAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TopicAdmin/Create
        [HttpPost]
        public ActionResult Create(Topic model)
        {
            try
            {
                // TODO: Add insert logic here
                if(!ModelState.IsValid)
                {
                    return View(model);
                }
                else
                {
                    if (string.IsNullOrEmpty(model.Title) && !string.IsNullOrEmpty(model.Name))
                    {
                        model.Title = SEA.Heplers.CreateDefault.CreateDefaultTitle(model.Name);
                    }
                    var author = Author.GetId(User.Identity.Name);
                    model.CreateBy = author;
                    model.EditBy = author;
                    model.CreateDate = DateTime.Now;
                    model.EditDate = DateTime.Now;
                    _db.Topics.Add(model);
                    _db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Admin/TopicAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_db.Topics.Find(id));
        }

        // POST: Admin/TopicAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Topic model)
        {
            try
            {
                // TODO: Add update logic here
                if(!ModelState.IsValid)
                {
                    return View(model);
                }
                else
                {
                    var data = _db.Topics.Find(id);
                    model.CreateBy = data.CreateBy;
                    model.CreateDate = data.CreateDate;
                    model.EditBy = Author.GetId(User.Identity.Name);
                    model.EditDate = DateTime.Now;
                    _db.Entry(data).CurrentValues.SetValues(model);
                    _db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Admin/TopicAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_db.Topics.Find(id));
        }

        // POST: Admin/TopicAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Topic model)
        {
            try
            {
                // TODO: Add delete logic here
                var data = _db.Topics.Find(id);
                _db.Topics.Remove(data);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }
    }
}
