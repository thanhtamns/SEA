using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using System.Web.Mvc;
namespace SEA.Heplers
{
    /// <summary>
    /// nơi hỗ trợ khởi tạo các giá trị selectList.
    /// </summary>
    public static class Select
    {
        public static SelectList SelectListTopic(int id = 0,string langId = "")
        {
            var list = new List<Topic>();
            if (langId == "")
            {
                list = new MyBlogEntities().Topics.ToList();
                list.Add(new Topic() { Id = 0, Name = "Not exists" });
            }
            else
            {
                list = new MyBlogEntities().Topics.Where(x => x.LangId == langId).ToList();
            }
            return new SelectList(list, "Id", "Name",id);
        }
        public static SelectList SelectIsActive(bool isActive = true)
        {
            return new SelectList(new[] { new { value = true, text = "Active" }, new { value = false, text = "Deactive" } }, "value", "text", isActive);
        }
        public static SelectList SelectLang(string id = "en")
        {
            var list = new MyBlogEntities().Languages.ToList();
            return new SelectList(list, "Id", "Name", id);
        }
        public static SelectList SelectNameTopic(string langId = "en")
        {
            var list = new MyBlogEntities().Topics.Where(x=>x.LangId == langId).ToList();
            return new SelectList(list, "Name", "Name");
        }
        public static SelectList SelectListPost(int rootId,string langId = "")
        {
            var list = new List<Post>();
            list.Add(new Post() { Id = 0, Name = "Not exists" });
            if (langId == "")
            {
                list.AddRange(new MyBlogEntities().Posts.ToList());
            }
            else
            {
                list.AddRange(new MyBlogEntities().Posts.Where(x => x.LangId != langId).ToList());
            }
            return new SelectList(list, "Id", "Name", rootId);
        }
        
    }
}