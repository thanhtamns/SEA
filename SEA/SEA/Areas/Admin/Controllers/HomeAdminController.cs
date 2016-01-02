using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEA.CustomSystem;
namespace SEA.Areas.Admin.Controllers
{
    public static class Author
    {
        public static string GetId(string name)
        {
            return new MyBlogEntities().AspNetUsers.SingleOrDefault(x => x.Email == name).Id;
        }

        internal static object GetId()
        {
            throw new NotImplementedException();
        }
    }

    [AuthorizeAdmin]
    public class HomeAdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.Char = SEA.Heplers.CreateDefault.CreateDefaultTitle("Nhạc phim cô dâu tám tuổi");
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}