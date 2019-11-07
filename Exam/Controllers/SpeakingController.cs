using Exam.Models;
using System;
using System.Collections.Generic;
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
        public JsonResult GetProperExam(int level, bool isRandom, string category)
        {
            return Json(null);
        }

        public JsonResult SubmitAnswer(Guid speakingId, string answer)
        {
            return Json(null);
        }

        public JsonResult GetCorrection(Guid speakingId)
        {
            return Json(null);
        }
    }
}