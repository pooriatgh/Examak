using Exam.Data;
using Exam.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam.Controllers
{
    [AuthorizationEN]
    public class SpeakingController : Controller
    {
        public ActionResult SpeakingPanel()
        {
            return View();
        }
        public JsonResult GetProperExam(int level, bool isRandom, string category,Guid? speakingId)
        {
            var db = new ENDEntities();
            if (!isRandom)
            {
                var speaking = db.Tbl_Question.Where(p => p.QuestionIsActive == true &&
                p.QuestionType == "speaking" && p.PK_QuestionId == speakingId).FirstOrDefault();
                if (speaking != null)
                {
                    return Json(speaking);
                }
                else
                {
                    return Json("nospeaking");
                }
            }
            else
            {
                var random = new Random();
                var speakingList = db.Tbl_Question.Where(p => p.QuestionIsActive == true && p.QuestionType == "speaking").ToList();
                int index = random.Next(speakingList.Count);
                return Json(speakingList[index]);
            }
        }

        public JsonResult SubmitAnswer(Guid speakingId, HttpPostedFileBase speakingFile)
        {
            var db = new ENDEntities();
            var user = (Tbl_User)Session["UserSession"];
            var updateUser = db.Tbl_User.Where(p => p.PK_UserId == user.PK_UserId && p.IsActive == true).FirstOrDefault();
            string physicalpath = "";
            if (user.SpeakingAvailable > 0)
            {
                if (speakingFile.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid().ToString()+".mp3";
                    physicalpath = Path.Combine(Server.MapPath("~/App_Data/SpeakingFiles"), fileName);
                    speakingFile.SaveAs(physicalpath);
                }

                var newANswer = new Tbl_Answer
                {
                    AnswerDatetime = DateTime.UtcNow,
                    FK_QuestionId = speakingId,
                    FK_UserId = user.PK_UserId,
                    PK_AnswerId = Guid.NewGuid(),
                    TeacherAnswerFile = String.Empty,
                    TeacherAnswerText = String.Empty,
                    TeacherReplayDatetime = null,
                    UserAnswerFileUrl = physicalpath,
                    UserAnswerText = String.Empty
                };
                db.Tbl_Answer.Add(newANswer);
                db.SaveChanges();
                updateUser.SpeakingAvailable -= 1;
                db.SaveChanges();
            }
            return Json(null);
        }

        public JsonResult GetCorrectionList(Guid speakingId)
        {
            var db = new ENDEntities();
            var user = (Tbl_User)Session["UserSession"];
            var answerU = db.Tbl_Answer.Where(p => p.FK_QuestionId == speakingId && p.FK_UserId == user.PK_UserId).ToList();
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
            if (updateUser != null)
            {
                return Json(updateUser.SpeakingAvailable);
            }
            return Json("nok");
        }

        public JsonResult GetInProgress()
        {
            var db = new ENDEntities();
            var user = (Tbl_User)Session["UserSession"];
            var answerList = db.Tbl_Answer.Where(p => p.FK_UserId == user.PK_UserId &&
            p.TeacherAnswerText == String.Empty && p.TeacherAnswerFile == String.Empty &&
            p.Tbl_Question.QuestionType=="speaking").ToList();
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
            var answerList = db.Tbl_Answer.Where(p => p.FK_UserId == user.PK_UserId && p.Tbl_Question.QuestionType=="Speaking" &&
            (p.TeacherAnswerText != String.Empty || p.TeacherAnswerFile != String.Empty)).ToList();
            if (answerList != null)
            {
                return Json(answerList.Count);
            }
            return Json("nok");
        }

    }
}