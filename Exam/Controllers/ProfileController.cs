using Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        [AuthorizationEN]
        public ActionResult Index()
        {
            return View();
        }
    }
}