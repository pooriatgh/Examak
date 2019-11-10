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