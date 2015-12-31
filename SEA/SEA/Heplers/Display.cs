using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
namespace SEA.Heplers
{
    /// <summary>
    /// nơi hỗ trợ hiển thị một số dữ liệu ra màn hình của hệ thống
    /// </summary>
    public static class Display
    {
        public static string DisplayNameAccount(string id)
        {
            return new MyBlogEntities().AspNetUsers.Find(id).Email;
        }
        public static string DisplayNameTopic(int id)
        {
            
            if(id == 0)
            {
                return "Not exists";
            }
            else
            {
                var topic = new MyBlogEntities().Topics.Find(id);
                return topic.Name;
            }
        }
        public static string DisplayActive(bool isActive)
        {
            return isActive == true ? "Activated" : "Deactivated";
        }
        public static string DisplayLanguage(string id)
        {
            return new MyBlogEntities().Languages.Find(id).Name;
        }
    }
}