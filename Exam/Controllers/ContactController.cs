using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Form(string str)
        {
            return View();
        }
    }
}