using Exam.Data;
using Exam.Models;
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
            var hashPassword = HashHandler.Hash(password);
            if (true)// exist in database and active
            {
                Session["UserSession"] = new Tbl_User { Mobile = mobile, Password = String.Empty };
            }
            return Json("ok");
        }
        public JsonResult Signup(string mobile,string password)
        {
            if (false)// mobile does not exist with 0 or 98 or +98 or any
            {
                var hashPassword = HashHandler.Hash(password);
                var newUser = new Tbl_User
                {
                    Mobile = mobile,
                    Password = hashPassword,
                    MobileIsActive = false,
                    ResetPassCode = String.Empty,
                    SignupCode = Guid.NewGuid().ToString().Substring(0, 4)
                };
                SMSHandler.SendMessage(mobile, "کد سایت آزمونک" + " " + newUser.SignupCode + " ");
            }
            return Json(null);
        }
        public JsonResult ForgotPassword(string mobile)
        {
            SMSHandler.SendMessage(mobile,Guid.NewGuid().ToString().Substring(0, 4));
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