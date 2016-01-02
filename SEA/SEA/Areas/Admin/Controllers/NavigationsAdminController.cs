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
    public class NavigationsAdminController : Controller
    {
        MyBlogEntities _db;
        public NavigationsAdminController()
        {
            _db = new MyBlogEntities();
        }

        // GET: Admin/NavigationsAdmin
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            return View(_db.Navigations.OrderBy(x => x.Id).ToPagedList<Navigation>(page, pageSize) as PagedList<Navigation>);
        }

        // GET: Admin/NavigationsAdmin/Details/5
        public ActionResult Details(int id)
        {
            var navigation = _db.Navigations.Find(id);
            return View(navigation);
        }

        // GET: Admin/NavigationsAdmin/Create
        public ActionResult Create()
        {           
            return View();
        }

       
        [HttpPost]
        public ActionResult Create(Navigation model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
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
                    _db.Navigations.Add(model);
                    _db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Admin/NavigationsAdmin/Edit/5
        public ActionResult Edit(int id)
        {
           
            return View(_db.Navigations.Find(id));
        }

        // POST: Admin/NavigationsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public ActionResult Edit(int id, Navigation model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
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
