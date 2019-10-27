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
    public class BookingsController : Controller
    {
        private TentsTrailersIADEntities db = new TentsTrailersIADEntities();

        // GET: Bookings
        public ActionResult Index()
        {
            return View(db.Bookings.ToList());
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }


        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.CampId = new SelectList(db.Campsites, "CampId", "CampId");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //Create a new booking
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        [Authorize]
        public ActionResult Create([Bind(Include = "BookingId,CampId,BookingDate,BookingStartDate,BookingEnddate,BookingStatus")] Booking booking, int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (booking.BookingStartDate < DateTime.Today)
                    {
                        ViewBag.StartDateError = "Start Date cannot be less that today's date";
                    }
                    else if (booking.BookingEnddate < booking.BookingStartDate)
                    {
                        ViewBag.EndDateError = "End date cannot be less that Start date";
                    }
                    else
                    {
                        booking.CampId = id;
     
                        booking.BookingDate = System.DateTime.Now;
                        booking.BookingStatus = "confirmed";
                        db.Bookings.Add(booking);
                        db.SaveChanges();
                        Session["BookId"] = booking.BookingId;
                        return RedirectToAction("Create", "Members");

                    }
                   
                }
                catch
                {
                    return View();
                }
                
            }

            ViewBag.CampId = new SelectList(db.Campsites, "CampId", "CampId", booking.CampId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampId = new SelectList(db.Campsites, "CampId", "CampId", booking.CampId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingId,CampId,BookingDate,BookingStartDate,BookingEnddate,BookingStatus")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CampId = new SelectList(db.Campsites, "CampId", "CampId", booking.CampId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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
