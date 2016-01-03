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
    public class FeelBacksAdminController : Controller
    {
        MyBlogEntities _db ;
        public FeelBacksAdminController()
        {
            _db = new MyBlogEntities();
        }
        // GET: Admin/FeelBacksAdmin
        public ActionResult Index(int page=1, int pageSize=10)
        {
            return View(_db.FeelBacks.OrderBy(x => x.Id).ToPagedList<FeelBack>(page, pageSize) as PagedList<FeelBack>);
        }

        // GET: Admin/FeelBacksAdmin/Details/5
        public ActionResult Details(int id)
        {            
            return View(_db.FeelBacks.Find(id));
        }
        
        // GET: Admin/FeelBacksAdmin/Delete/5
        public ActionResult Delete(int id)
        {
           
            return View(_db.FeelBacks.Find(id));
        }

        // POST: Admin/FeelBacksAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FeelBack feelBack = _db.FeelBacks.Find(id);
            _db.FeelBacks.Remove(feelBack);
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
    }
}
