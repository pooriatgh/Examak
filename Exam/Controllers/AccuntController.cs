using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam.Controllers
{
    public class AccuntController : Controller
    {
        // GET: Profile
        [HttpPost]
        public JsonResult Login(string mobile,string password)
        {
            return Json(null);
        }
        [HttpPost]
        public JsonResult Signup(string mobile,string password)
        {
            return Json(null);
        }
        [HttpPost]
        public JsonResult CompleteSignup(string mobile,string code)
        {
            if (true)//code ok and time doesnt expired
            {
                RedirectToAction("Index","Profile");
            }
            return Json(null);
        }
    }
}