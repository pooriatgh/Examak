using Exam.Data;
using Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam.Controllers
{
    [AuthorizationEN]
    public class TicketController : Controller
    {
        public ActionResult Ticket()
        {
            return View();
        }

      
        public JsonResult GetTicketList()
        {
            var db = new ENDEntities();
            var user = (Tbl_User)Session["UserSession"];
            var ticketList = db.Tbl_Ticket.Where(p => p.FK_UserId == user.PK_UserId && p.FK_TicketParent==null)
                .Select(p => new {
                    p.CreateDateTime,
                    p.PK_TicketId,
                    p.TicketSubject,
                    p.TicketText,
                }).OrderByDescending(p => p.CreateDateTime).ToList();
            return Json(ticketList);
        }
        public JsonResult SendReplyTicket(string message,Guid ticketRootId) {
            var db = new ENDEntities();
            var user = (Tbl_User)Session["UserSession"];
            db.Tbl_Ticket.Add(new Tbl_Ticket {
                CreateDateTime = DateTime.UtcNow,
                FK_TicketParent = ticketRootId,
                FK_UserId = user.PK_UserId,
                IsAnswer = true,
                TicketText = message,
                TicketSubject= String.Empty
            });
            db.SaveChanges();
            return Json("ok");
        }
        public JsonResult NewTicket(string message, string subject)
        {
            var db = new ENDEntities();
            var user = (Tbl_User)Session["UserSession"];
            db.Tbl_Ticket.Add(new Tbl_Ticket
            {
                CreateDateTime = DateTime.UtcNow,
                FK_TicketParent = null,
                FK_UserId = user.PK_UserId,
                IsAnswer = true,
                TicketText = message,
                TicketSubject = subject
            });
            db.SaveChanges();
            return Json("ok");
        }
        public JsonResult GetticketSublist(Guid ticketId)
        {
            var db = new ENDEntities();
            var user = (Tbl_User)Session["UserSession"];
            var allTicket = db.Tbl_Ticket
                .Where(p => p.FK_UserId == user.PK_UserId)
                .ToList();
            var answer = new List<Tbl_Ticket>();

            var ticket = allTicket.Where(p => p.FK_TicketParent == ticketId).FirstOrDefault();
            while (ticket!=null) {
                answer.Add(ticket);
                ticket = allTicket.Where(p => p.FK_TicketParent == ticket.PK_TicketId).FirstOrDefault();
            }
            return Json(answer);
        }
    }
}