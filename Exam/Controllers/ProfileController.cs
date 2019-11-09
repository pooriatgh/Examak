using Exam.Models;
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
            return Json(null);
        }
        public JsonResult GetTicketList()
        {
            return Json(null);
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