using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Exam.Data;

namespace Exam.Admin
{
    public class AdminQuestionController : Controller
    {
        private ENDEntities db = new ENDEntities();

        // GET: AdminQuestion
        public ActionResult Index()
        {
            return View(db.Tbl_Question.ToList());
        }

        // GET: AdminQuestion/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Question tbl_Question = db.Tbl_Question.Find(id);
            if (tbl_Question == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Question);
        }

        // GET: AdminQuestion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminQuestion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_QuestionId,QuestionType,QuestionCategory,QuestionLevel,QuestionTitle,QuestionText,QuestionIsActive,CreateDatetime,QuestionImageUrl1,QuestionImageUrl2,QuestionFileUrl")] Tbl_Question tbl_Question)
        {
            if (ModelState.IsValid)
            {
                tbl_Question.PK_QuestionId = Guid.NewGuid();
                db.Tbl_Question.Add(tbl_Question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Question);
        }

        // GET: AdminQuestion/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Question tbl_Question = db.Tbl_Question.Find(id);
            if (tbl_Question == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Question);
        }

        // POST: AdminQuestion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_QuestionId,QuestionType,QuestionCategory,QuestionLevel,QuestionTitle,QuestionText,QuestionIsActive,CreateDatetime,QuestionImageUrl1,QuestionImageUrl2,QuestionFileUrl")] Tbl_Question tbl_Question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Question);
        }

        // GET: AdminQuestion/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Question tbl_Question = db.Tbl_Question.Find(id);
            if (tbl_Question == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Question);
        }

        // POST: AdminQuestion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Tbl_Question tbl_Question = db.Tbl_Question.Find(id);
            db.Tbl_Question.Remove(tbl_Question);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
