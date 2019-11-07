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
    }
}