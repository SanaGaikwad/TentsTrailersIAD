using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TentsTrailersIAD.Models;

namespace TentsTrailersIAD.Controllers
{
    public class CampsitesController : Controller
    {
        private TentsTrailersIADEntities db = new TentsTrailersIADEntities();

        // GET: Campsites
        public ActionResult Index()
        {
            return View(db.Campsites.ToList());
        }

        public ActionResult ListView()
        {
            return View(db.Campsites.ToList());
        }

        // GET: Campsites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campsite campsite = db.Campsites.Find(id);
            if (campsite == null)
            {
                return HttpNotFound();
            }
            return View(campsite);
        }

        // GET: Campsites/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Campsites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "CampId,Description,Price,Type,Accomodates,Location")] Campsite campsite)
        {
            if (ModelState.IsValid)
            {
                db.Campsites.Add(campsite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(campsite);
        }

        // GET: Campsites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campsite campsite = db.Campsites.Find(id);
            if (campsite == null)
            {
                return HttpNotFound();
            }
            return View(campsite);
        }

        // POST: Campsites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CampId,Description,Price,Type,Accomodates,Location")] Campsite campsite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(campsite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(campsite);
        }

        // GET: Campsites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campsite campsite = db.Campsites.Find(id);
            if (campsite == null)
            {
                return HttpNotFound();
            }
            return View(campsite);
        }

        // POST: Campsites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Campsite campsite = db.Campsites.Find(id);
            db.Campsites.Remove(campsite);
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
