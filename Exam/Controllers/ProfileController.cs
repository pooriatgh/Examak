using Exam.Data;
using Exam.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam.Controllers
{
    [AuthorizationEN]
    public class ProfileController : Controller
    {
        // GET: Profile

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetTransactionList()
        {
            var db = new ENDEntities();
            var user = (Tbl_User)Session["UserSession"];
            var transList = db.Tbl_Payment.Where(p => p.FK_UserId == user.PK_UserId).Select(p=> new {
                Amount = p.Amount.GetValueOrDefault(0),
                p.IsVerified,
                StatusSend = p.SendError,
                StatusRec = p.RecieveError,
                p.CreateDatetime
            }).OrderByDescending(p=>p.CreateDatetime).ToList();
            return Json(transList);
        }
        
        public JsonResult SendTicket(Guid? replyTicketId)
        {
            return Json(null);
        }
        public JsonResult ShowTicketThread(Guid? ticketRootId)
        {
            return Json(null);
        }

        
        
    }
}