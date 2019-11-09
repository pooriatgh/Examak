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
    public class PaymentController : Controller
    {
        public void BeginPayment(Guid packageId)
        {
            try
            {
                //get packageid and set amount
                var db = new ENDEntities();
                var user = (Tbl_User)Session["UserSession"];
                decimal amount = 1000;
                var newPayment = new Tbl_Payment
                {
                    PK_PaymentId = Guid.NewGuid(),
                    Amount = amount,
                    CreateDatetime = DateTime.UtcNow,
                    FK_UserId = user.PK_UserId,
                    IsVerified = false,
                    RecieveError = String.Empty,
                    SendError = String.Empty
                };
                db.Tbl_Payment.Add(newPayment);
                db.SaveChanges();
                PaymentHandler ob = new PaymentHandler();
                string result = ob.pay(amount.ToString());
                JsonParameters Parmeters = JsonConvert.DeserializeObject<JsonParameters>(result);
                if (Parmeters.status == 1)
                {
                    newPayment.TransactionId = Parmeters.transId;
                    newPayment.HashTransaction = HashHandler.Hash(Parmeters.transId);
                    db.SaveChanges();
                    Response.Redirect("https://pay.ir/payment/gateway/" + Parmeters.transId);
                }
                else
                {
                    newPayment.RecieveError = "کدخطا : " + Parmeters.errorCode + "<br />" + "پیغام خطا : " + Parmeters.errorMessage;
                    db.SaveChanges();
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
                    string transId = Request.Form["transId"].ToString();
                    var db = new ENDEntities();
                    var timenow = DateTime.UtcNow;
                    var user = (Tbl_User)Session["UserSession"];
                    var payment = db.Tbl_Payment.Where(p => p.FK_UserId == user.PK_UserId &&
                    p.TransactionId == transId && p.IsVerified == false).FirstOrDefault();
                    string result = ob.verify(transId);
                    JsonParameters Parmeters = JsonConvert.DeserializeObject<JsonParameters>(result);
                    if (Parmeters.status == 1)
                    {
                       
                        if (payment!=null)
                        {
                            payment.IsVerified = true;
                            if (payment.Amount == 500000)
                            {
                                payment.Tbl_User.WritingAvailable += 1;
                                payment.Tbl_User.SpeakingAvailable += 1;
                            }else if (payment.Amount == 1000000)
                            {
                                payment.Tbl_User.WritingAvailable += 3;
                                payment.Tbl_User.SpeakingAvailable += 2;
                            }
                            else if (payment.Amount == 2000000)
                            {
                                payment.Tbl_User.WritingAvailable += 6;
                                payment.Tbl_User.SpeakingAvailable += 6;
                            }
                            else if (payment.Amount == 5000000)
                            {
                                payment.Tbl_User.WritingAvailable += 16;
                                payment.Tbl_User.SpeakingAvailable += 16;
                            }
                            else if (payment.Amount == 10000000)
                            {
                                payment.Tbl_User.WritingAvailable += 40;
                                payment.Tbl_User.SpeakingAvailable += 40;
                            }
                            db.SaveChanges();
                        }
                        ViewBag["PaymentRresult"] += Request.Form["transId"].ToString();
                        ViewBag["PaymentRresult"] += Parmeters.amount.ToString();
                        ViewBag["PaymentRresult"] = "پرداخت با موفقیت انجام شد.";
                    }
                    else
                    {
                        payment.RecieveError = "کدخطا : " + Parmeters.errorCode + "<br />" + "پیغام خطا : " + Parmeters.errorMessage;
                        db.SaveChanges();
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