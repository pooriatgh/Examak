using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Filters;

namespace Exam.Models
{
    public class AuthorizationEN : System.Web.Mvc.ActionFilterAttribute, IAuthenticationFilter
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            //var user = filterContext.RequestContext.HttpContext.Session["UserSession"];
            //if (user == null)
            //{
            //    filterContext.Result = new System.Web.Mvc.RedirectResult("~/Home/Index?KickOut=True");
            //}
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {

        }

    }
}