using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam.Controllers
{
    public class AccuntController : Controller
    {
       
        public JsonResult Login(string mobile,string password)
        {
            return Json("ok");
        }
        public JsonResult Signup(string mobile,string password)
        {
            return Json(null);
        }
        public JsonResult ForgotPassword(string mobile)
        {
            return Json(null);
        }
        public JsonResult ConfirmNewPassword(string mobile,string code ,string newPassword){
            return Json(null);
        }
        public JsonResult CompleteSignup(string mobile,string code)
        {
            if (true)//code ok and time doesnt expired
            {
                return Json("ok");
            }
            return Json("nok");
        }
    }
}