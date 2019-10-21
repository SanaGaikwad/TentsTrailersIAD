using Newtonsoft.Json;
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
    public class PrecipitatesController : Controller
    {
        private TentsTrailersIADEntities db = new TentsTrailersIADEntities();

        // GET: Precipitates
        public ActionResult Index()
        {
            return View(db.Precipitates.ToList());
        }

        // GET: Precipitates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Precipitate precipitate = db.Precipitates.Find(id);
            if (precipitate == null)
            {
                return HttpNotFound();
            }
            return View(precipitate);
        }

        // GET: Precipitates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Precipitates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PrecipitateId,MONTHS,Precipitation")] Precipitate precipitate)
        {
            if (ModelState.IsValid)
            {
                db.Precipitates.Add(precipitate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(precipitate);
        }

        // GET: Precipitates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Precipitate precipitate = db.Precipitates.Find(id);
            if (precipitate == null)
            {
                return HttpNotFound();
            }
            return View(precipitate);
        }

        // POST: Precipitates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PrecipitateId,MONTHS,Precipitation")] Precipitate precipitate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(precipitate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(precipitate);
        }

        // GET: Precipitates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Precipitate precipitate = db.Precipitates.Find(id);
            if (precipitate == null)
            {
                return HttpNotFound();
            }
            return View(precipitate);
        }

        // POST: Precipitates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Precipitate precipitate = db.Precipitates.Find(id);
            db.Precipitates.Remove(precipitate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Report()
        {
            try
            {

                ViewBag.DataPoints1 = JsonConvert.SerializeObject(db.Precipitates.ToList(), _jsonSetting);
                ViewBag.DataPoints3 = JsonConvert.SerializeObject(db.PrecipitateMINs.ToList(), _jsonSetting);
                ViewBag.DataPoints2 = JsonConvert.SerializeObject(db.PrecipitateMAXes.ToList(), _jsonSetting);
                return View();
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                return View("Error");
            }
            catch (System.Data.SqlClient.SqlException)
            {
                return View("Error");
            }
        }

            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        

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
