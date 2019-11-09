using Exam.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam.Controllers
{
    public class PaymentController : Controller
    {
        public void BeginPayment(Guid packageId)
        {
            try
            {
                //get packageid and set amount

                PaymentHandler ob = new PaymentHandler();
                string result = ob.pay("1000");
                JsonParameters Parmeters = JsonConvert.DeserializeObject<JsonParameters>(result);
                if (Parmeters.status == 1)
                {
                    Response.Redirect("https://pay.ir/payment/gateway/" + Parmeters.transId);
                }
                else
                {
                    ViewBag["Error"] = "کدخطا : " + Parmeters.errorCode + "<br />" + "پیغام خطا : " + Parmeters.errorMessage;
                }
            }
            catch (Exception)
            {
                ViewBag["Error"] = "خطا در اتصال به درگاه پرداخت";
            }
        }
        public ActionResult Confirm()
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.Form["transId"]))
                {
                    PaymentHandler ob = new PaymentHandler();
                    string result = ob.verify(Request.Form["transId"].ToString());
                    JsonParameters Parmeters = JsonConvert.DeserializeObject<JsonParameters>(result);

                    if (Parmeters.status == 1)
                    {
                        ViewBag["PaymentRresult"] += Request.Form["transId"].ToString();
                        ViewBag["PaymentRresult"] += Parmeters.amount.ToString();
                        ViewBag["PaymentRresult"] = "پرداخت با موفقیت انجام شد.";
                    }
                    else
                    {
                        ViewBag["Error"] = true;
                        ViewBag["Error"] = "کدخطا : " + Parmeters.errorCode + "<br />" + "پیغام خطا : " + Parmeters.errorMessage;
                    }
                }
            }
            catch (Exception)
            {
                ViewBag["Error"] = "متاسفانه پرداخت ناموفق بوده است.";
            }
            return View();
        }
    }
}