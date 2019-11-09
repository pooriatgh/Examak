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

        public JsonResult Login(string mobile, string password)
        {
            var db = new ENDEntities();
            var hashPassword = HashHandler.Hash(password);
            var user = db.Tbl_User.Where(p => p.Mobile == mobile && p.Password == hashPassword).FirstOrDefault();
            if (user != null)// exist in database and active
            {
                if (user.IsActive)
                {
                    Session["UserSession"] = user;
                    return Json("ok");
                }
                else
                {
                    return Json("notActive");
                }
            }
            else
            {
                return Json("userOrPasswordIncorrect");
            }
        }
        public JsonResult Signup(string mobile, string password)
        {
            var db = new ENDEntities();
            mobile = mobile.Replace("+", "").Replace("98", "");
            if (mobile.ElementAt(0) != 0)
            {
                mobile = "0" + mobile;
            }
            var userExist = db.Tbl_User.Where(p => p.Mobile == mobile).FirstOrDefault();
            if (userExist == null)// mobile does not exist with 0 or 98 or +98 or any
            {
                var hashPassword = HashHandler.Hash(password);
                var newUser = new Tbl_User
                {
                    Mobile = mobile,
                    Password = hashPassword,
                    ResetPassCode = String.Empty,
                    SignupCode = Guid.NewGuid().ToString().Substring(0, 4),
                    PK_UserId = Guid.NewGuid(),
                    IsActive = false,
                    Name = "Guest",
                    CreateDatetime = DateTime.UtcNow,
                    SignupCodeExpireTime = DateTime.UtcNow.AddDays(1),
                    ResetCodeExpireTime = DateTime.UtcNow,
                    SpeakingAvailable = 1,
                    WritingAvailable = 1

                };
                SMSHandler.SendMessage(mobile, "کد سایت آزمونک" + " " + newUser.SignupCode + " ");
            }
            return Json(null);
        }
        public JsonResult ForgotPassword(string mobile)
        {
            var db = new ENDEntities();
            var userExist = db.Tbl_User.Where(p => p.Mobile == mobile).FirstOrDefault();
            if (userExist != null)
            {
                var code = Guid.NewGuid().ToString().Substring(0, 4);
                userExist.ResetPassCode = code;
                userExist.ResetCodeExpireTime = DateTime.UtcNow.AddDays(1);
                db.SaveChanges();
                SMSHandler.SendMessage(mobile, code + " کد تغییر رمز ");
            }
            return Json(null);
        }
        public JsonResult ConfirmNewPassword(string mobile, string code, string newPassword)
        {
            if (code != String.Empty)
            {
                var db = new ENDEntities();
                var timeNow = DateTime.UtcNow;
                var userExist = db.Tbl_User.Where(p => p.Mobile == mobile &&
                p.ResetPassCode == code && p.ResetCodeExpireTime < timeNow).FirstOrDefault();
                if (userExist != null)
                {
                    userExist.Password = HashHandler.Hash(newPassword);
                    userExist.ResetPassCode = String.Empty;
                    db.SaveChanges();
                    return Json("PasswordChangeOK");
                }
                else
                {
                    return Json("CodeOrTimeIsWrong");
                }
            }
            return Json("CodeEmpty");
        }
        public JsonResult CompleteSignup(string mobile, string code)
        {
            var db = new ENDEntities();
            var timeNow = DateTime.UtcNow;
            var userExist = db.Tbl_User.Where(p => p.Mobile == mobile &&
            p.SignupCode == code && p.SignupCodeExpireTime < timeNow).FirstOrDefault();
            if (code!=String.Empty && userExist!=null )//code ok and time doesnt expired
            {
                userExist.IsActive = true;
                db.SaveChanges();
                return Json("ok");
            }
            return Json("nok");
        }
    }
}