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
        private List<Topic> ListTopicFromScript(string strScript)
        {
            List<string> model = new List<string>();
            string data = "";
            foreach (var item in strScript)
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

        private bool CheckListTopic(List<string> listOld, string listNew, ref List<Topic> listAdd, ref List<Topic> listRemove)
        {
            List<string> model = new List<string>();
            string data = "";
            foreach (var item in listNew)
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
            bool flag = false;
            foreach (var itemOld in listOld)
            {
                if(listNew.Contains(itemOld))
                {
                    model.Remove(itemOld);
                    listOld.Remove(itemOld);
                }
                else
                {
                    flag = true;
                }
            }
            if (flag == false)
            {
                return flag;
            }
            listAdd = new List<Topic>();
            foreach (var item in model)
            {
                var topic = _db.Topics.SingleOrDefault(x => x.Name == item);
                if (topic != null)
                {
                    listAdd.Add(topic);
                }
            }
            listRemove = new List<Topic>();
            foreach (var item in listOld)
            {
                var topic = _db.Topics.SingleOrDefault(x => x.Name == item);
                if (topic != null)
                {
                    listRemove.Add(topic);
                }
            }
            return flag;

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
                if(!ModelState.IsValid)
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
                    _db.Posts.Add(model);
                    _db.SaveChanges();
                    var post = _db.Posts.SingleOrDefault(x => x.Title == model.Title);
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
            var post = _db.Posts.Find(id);
            ViewBag.ListTopic = post.Topics.Select(x => x.Name).ToList();
            return View(post);
        }

        // POST: Admin/PostAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Post model, string ListTopic)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    var post = _db.Posts.Find(id);
                    ViewBag.ListTopic = post.Topics.Select(x => x.Name).ToList();
                    return View(model);

                }
                else
                {
                    var post = _db.Posts.Find(id);
                    if(model.Title == post.Title &&model.Name!=post.Name)
                    {
                        model.Title = post.Name;
                    }
                    model.CreateBy = post.CreateBy;
                    model.CreateDate = post.CreateDate;
                    model.EditBy = Author.GetId(User.Identity.Name);
                    model.EditDate = DateTime.Now;
                    _db.Entry(post).CurrentValues.SetValues(model);
                    _db.SaveChanges();
                    List<Topic> remove = new List<Topic>();
                    List<Topic> add = new List<Topic>();
                    var check = CheckListTopic(post.Topics.Select(x => x.Name).ToList(), ListTopic,ref add,ref remove);
                    if(check ==true)
                    {
                        foreach (var item in remove)
                        {
                            post.Topics.Remove(item);
                        }
                        foreach (var item in add)
                        {
                            post.Topics.Add(item);
                        }
                        _db.SaveChanges();
                    }
                }
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
            var post = _db.Posts.Find(id);
            return View(post);
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

        public ActionResult EditJoin(int id)
        {
            var post = _db.Posts.Find(id);
            ViewBag.Root = _db.Posts.Find(post.RootId);
            var a = _db.Languages.Where(x => x.Id != post.LangId);
            ViewBag.SelectLang = new SelectList( a,"Id","Name",a.First());
            ViewBag.Lang = a.First();
            var b = _db.Posts.Where(x=>x.LangId == a.FirstOrDefault().Id).ToList();
            b.Add(new Post() { Id = 0, Name = "Not exists" });
            ViewBag.SelectRootId = new SelectList(b, "Id", "Name");
            return View(post);
        }
        public ActionResult ListPostByLang(string id)
        {
            var b = _db.Posts.Where(x => x.LangId == id).ToList();
            b.Add(new Post() { Id = 0, Name = "Not exists" });
            ViewBag.SelectRootId = new SelectList(b, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult EditJoin(int id, int RootId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Posts.Find(id).RootId = RootId;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(_db.Posts.Find(id));
                }
            }
            catch
            {
                return View(_db.Posts.Find(id));
            }
        }

        public string GetIdOfPost(int id, string langId)
        {
            try
            {
                return GetPost.Elemet(_db.Posts.Find(id), langId).Id.ToString();
            }
            catch
            {
                return "Khong co";
            }
        }
    }

    public class GetPost
    {
        public static Post Elemet(Post post,string langId)
        {
            var _db = new MyBlogEntities();
            List<Post> data = new List<Post>();
            Queue<Post> queue = new Queue<Post>();
            queue.Enqueue(post);
            data.Add(post);
            while (queue.Count!=0)
            {
                var a = queue.First();
                var b = _db.Posts.Find(a.RootId);
                var c = _db.Posts.Where(x => x.RootId == a.Id);
                if(b!=null)
                {
                    if (!data.Contains(b))
                    {
                        data.Add(b);
                        queue.Enqueue(b);
                    }
                }
                foreach (var item in c)
                {
                    if (!data.Contains(item))
                    {
                        data.Add(item);
                        queue.Enqueue(item);
                    }
                }
                queue.Dequeue();
            }
            return data.Where(x => x.LangId == langId).First();
        }
    }
}
