using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam.Models
{
    public static class SMSHandler
    {
        public static bool SendMessage(string mobile ,string message)
        {
            var sender = "1000596446";
            var receptor = mobile;
            var api = new Kavenegar.KavenegarApi("4E624B41737739685264334447614C436F62496C53673D3D");
            api.Send(sender, receptor, message);
            return true;
        }
    }
}