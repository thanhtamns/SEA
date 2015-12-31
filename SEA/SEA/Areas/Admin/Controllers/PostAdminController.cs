using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using PagedList;
namespace SEA.Areas.Admin.Controllers
{
    public class PostAdminController : Controller
    {
        private List<Topic> ListTopicFromScript(string StrScript)
        {
            List<string> model = new List<string>();
            string data = "";
            foreach (var item in StrScript)
            {
                if(item!=',')
                {
                    data += item;
                }
                else
                {
                    model.Add(data);
                    data = "";
                }
            }
            model.Add(data);
            List<Topic> result = new List<Topic>();
            foreach (var item in model)
            {
                var topic = _db.Topics.SingleOrDefault(x => x.Name == item);
                if(topic!=null)
                {
                    result.Add(topic);
                }
            }
            return result;
        }
        MyBlogEntities _db;
        public PostAdminController()
        {
            _db = new MyBlogEntities();
        }
        // GET: Admin/PostAdmin
        public ActionResult Index(int page = 1,int pageSize = 10)
        {
            return View(_db.Posts.OrderBy(x=>x.Id).ToPagedList(page,pageSize) as PagedList<Post>);
        }

        // GET: Admin/PostAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View(_db.Posts.Find(id));
        }

        // GET: Admin/PostAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/PostAdmin/Create
        [HttpPost]
        public ActionResult Create(Post model, string ListTopic)
        {
            try
            {
                List<Topic> list = ListTopicFromScript(ListTopic);
                if(string.IsNullOrEmpty(model.Title)&&!string.IsNullOrEmpty(model.Name))
                {
                    model.Title = SEA.Heplers.CreateDefault.CreateDefaultTitle(model.Name);
                }
                if(false)
                {
                    return View(model);
                }
                else
                {
                    var author = Author.GetId(User.Identity.Name);
                    model.CreateBy = author;
                    model.CreateDate = DateTime.Now;
                    model.EditBy = author;
                    model.EditDate = DateTime.Now;
                    //_db.Posts.Add(model);
                    //_db.SaveChanges();
                    var post = _db.Posts.SingleOrDefault(x => x.Title == "bien-va-tam-vuc-cua-bien");
                    if(post!=null)
                    {
                        foreach (var item in list)
                        {
                            post.Topics.Add(item);
                        }
                    }
                    _db.SaveChanges();
                }
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Admin/PostAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_db.Posts.Find(id));
        }

        // POST: Admin/PostAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Post model)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Admin/PostAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_db.Posts.Find(id));
        }

        // POST: Admin/PostAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Post model)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }
    }
}
