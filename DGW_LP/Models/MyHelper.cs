using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DGW_LP.Models
{
    public class MyHelper
    {
        public static string GetSocialAvatar()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            return user.Avatar;
        }

        public static string getVideoPath()
        {
            return HttpContext.Current.Server.MapPath("/");
        }
    }
}