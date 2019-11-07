using Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public JsonResult GetProperExam(int level,bool isRandom,string category)
        {
            return Json(null);
        }
        public JsonResult SubmitAnswer(Guid writingId, HttpPostedFileBase speakingFile)
        {
            return Json(null);
        }
        public JsonResult GetCorrection(Guid writingId)
        {
            return Json(null);
        }
    }
}