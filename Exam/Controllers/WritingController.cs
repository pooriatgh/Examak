using Exam.Data;
using Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Exam.Controllers
{
    [AuthorizationEN]
    public class WritingController : Controller
    {
        // GET: Writing
        
        public ActionResult WritingPanel()
        {
            return View();
        }
        public JsonResult GetProperExam(int level,bool isRandom,string category,Guid? writingId)
        {
            var db = new ENDEntities();
            if (!isRandom)
            {
                var writing = db.Tbl_Question.Where(p => p.QuestionIsActive == true &&
                p.QuestionType == "writing" && p.PK_QuestionId== writingId).FirstOrDefault();
                if (writing != null)
                {
                    return Json(writing);
                }
                else
                {
                    return Json("nowriting");
                }
            }
            else
            {
                var random = new Random();
                var writingList = db.Tbl_Question.Where(p => p.QuestionIsActive == true && p.QuestionType == "writing").ToList();
                int index = random.Next(writingList.Count);
                return Json(writingList[index]);
            }
        }
        public JsonResult SubmitAnswer(Guid writingId, string answer)
        {
            var db = new ENDEntities();
            var user = (Tbl_User)Session["UserSession"];
            var updateUser = db.Tbl_User.Where(p => p.PK_UserId == user.PK_UserId && p.IsActive==true).FirstOrDefault();
            if (user.WritingAvailable>0)
            {
                var newANswer = new Tbl_Answer
                {
                    AnswerDatetime = DateTime.UtcNow,
                    FK_QuestionId = writingId,
                    FK_UserId = user.PK_UserId,
                    PK_AnswerId = Guid.NewGuid(),
                    TeacherAnswerFile = String.Empty,
                    TeacherAnswerText = String.Empty,
                    TeacherReplayDatetime = null,
                    UserAnswerFileUrl = String.Empty,
                    UserAnswerText = answer
                };
                db.Tbl_Answer.Add(newANswer);
                Thread.Sleep(2000);
                db.SaveChanges();
                updateUser.WritingAvailable -= 1;
                db.SaveChanges();
            }

            return Json(null);
        }
        public JsonResult GetCorrectionList(Guid writingId)
        {
            var db = new ENDEntities();
            var user = (Tbl_User)Session["UserSession"];
            var answerU = db.Tbl_Answer.Where(p => p.FK_QuestionId == writingId && p.FK_UserId == user.PK_UserId).ToList();
            if (answerU != null)
            {
                return Json(answerU);
            }
            return Json("nok");
        }

        public JsonResult GetCorrection(Guid answerId)
        {
            var db = new ENDEntities();
            var user = (Tbl_User)Session["UserSession"];
            var answerU = db.Tbl_Answer.Where(p => p.PK_AnswerId == answerId && p.FK_UserId == user.PK_UserId).FirstOrDefault();
            if (answerU != null)
            {
                return Json(answerU);
            }
            return Json("nok");
        }


        public JsonResult GetAvailable()
        {
            var db = new ENDEntities();
            var user = (Tbl_User)Session["UserSession"];
            var updateUser = db.Tbl_User.Where(p => p.PK_UserId == user.PK_UserId && p.IsActive == true).FirstOrDefault();
            if (updateUser!=null)
            {
                return Json(updateUser.WritingAvailable);
            }
            return Json("nok");
        }
        public JsonResult GetInProgress()
        {
            var db = new ENDEntities();
            var user = (Tbl_User)Session["UserSession"];
            var answerList = db.Tbl_Answer.Where(p => p.FK_UserId==user.PK_UserId &&
            p.TeacherAnswerText == String.Empty && p.TeacherAnswerFile==String.Empty).ToList();
            if (answerList != null)
            {
                return Json(answerList.Count);
            }
            return Json("nok");
        }
        public JsonResult GetDone()
        {
            var db = new ENDEntities();
            var user = (Tbl_User)Session["UserSession"];
            var answerList = db.Tbl_Answer.Where(p => p.FK_UserId == user.PK_UserId &&
            (p.TeacherAnswerText != String.Empty || p.TeacherAnswerFile != String.Empty)).ToList();
            if (answerList != null)
            {
                return Json(answerList.Count);
            }
            return Json("nok");
        }
    }
}