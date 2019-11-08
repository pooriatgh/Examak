using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam.Data
{
    public class Tbl_User
    {
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string SignupCode { get; set; }
        public string ResetPassCode { get; set; }
        public bool MobileIsActive { get; set; }
    }
}