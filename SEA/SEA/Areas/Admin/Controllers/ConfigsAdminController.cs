using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using PagedList;

namespace SEA.Areas.Admin.Controllers
{
    public class ConfigsAdminController : Controller
    {
        MyBlogEntities _db ;
        public ConfigsAdminController()
        {
            _db = new MyBlogEntities();
        }

        // GET: Admin/ConfigsAdmin
        public ActionResult Index(int page=1, int pageSize=10)
        {           
            return View(_db.Configs.OrderBy(x=>x.Id).ToPagedList<Config>(page,pageSize) as PagedList<Config>);
        }

        // GET: Admin/ConfigsAdmin/Details/5
        public ActionResult Details(int id)
        {            
            return View(_db.Configs.Find(id));
        }

        // GET: Admin/ConfigsAdmin/Create
        public ActionResult Create()
        {            
            return View();
        }

     
        [HttpPost]
       
        public ActionResult Create(int id, Config model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(model);
                }
                else
                {
                    if(string.IsNullOrEmpty(model.Title)&&!string.IsNullOrEmpty(model.Name))
                    {
                        model.Title = SEA.Heplers.CreateDefault.CreateDefaultTitle(model.Name);
                    }
                    var author = Author.GetId(User.Identity.Name);
                    model.CreateBy = author;
                    model.EditBy = author;
                    model.CreateDate = DateTime.Now;
                    model.EditDate = DateTime.Now;
                    _db.Configs.Add(model);
                    _db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Admin/ConfigsAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_db.Configs.Find(id));
        }

      
        [HttpPost]       
        public ActionResult Edit(int id, Config model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(model);
                }
                else
                {
                    if(String.IsNullOrEmpty(model.Title)&&!String.IsNullOrEmpty(model.Name))
                    {
                        model.Title = SEA.Heplers.CreateDefault.CreateDefaultTitle(model.Name);
                    }
                    var author = Author.GetId(User.Identity.Name);
                    model.CreateBy = author;
                    model.CreateDate = DateTime.Now;
                    model.EditBy = author;
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

        // GET: Admin/ConfigsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Config config = _db.Configs.Find(id);
            if (config == null)
            {
                return HttpNotFound();
            }
            return View(config);
        }

        // POST: Admin/ConfigsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Config config = _db.Configs.Find(id);
            _db.Configs.Remove(config);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        public object data { get; set; }
    }
}
