using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using SEA.Heplers;
namespace SEA.CustomSystem
{
    /// <summary>
    /// kiểm tra quyền dụa trên role của user 
    /// chưa có kiểm tra token. sẽ nâng cấp ở bản tiết theo
    /// </summary>
    public class AuthorizeAdminAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var userName = httpContext.User.Identity.Name;
            var checkUser = new MyBlogEntities().AspNetUsers.SingleOrDefault(x => x.UserName == userName).AspNetRoles.Select(x => x.Id).Contains(Constant.ADMIN_ID);
            if (checkUser)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Response.Redirect("~/Admin");
        }
    }
}