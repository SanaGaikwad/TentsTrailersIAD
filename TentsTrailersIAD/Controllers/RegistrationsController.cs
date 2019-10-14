using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TentsTrailersIAD.Models;
using TentsTrailersIAD.Utils;

namespace TentsTrailersIAD.Controllers
{
    public class RegistrationsController : Controller
    {
        private TentsTrailersIADEntities db = new TentsTrailersIADEntities();

        // GET: Registrations
        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId();
            var registrations = db.Registrations.Where(r => r.Member.UserId == currentUserId).Include(r => r.Booking).Include(r => r.Member);
            return View(registrations.ToList());
        }

        public ActionResult MyBookings()
        {
            string currentUserId = User.Identity.GetUserId();
            var registrations = db.Registrations.Where(r => r.Member.UserId == currentUserId).Include(r => r.Booking).Include(r => r.Member);
            return View(registrations.ToList());
        }

        // GET: Registrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // GET: Registrations/Create
        public ActionResult Create()
        {
         
            string currentUserId = User.Identity.GetUserId();
            var fullName = db.Members.Where(m => m.UserId == currentUserId)
               .Select(a => new SelectListItem
               {
                   Value = a.MemberId.ToString(),
                   Text = a.FirstName + " " + a.LastName
               });
            ViewBag.MemberId = new SelectList(fullName, "Value", "Text");

            if (Session["BookId"] != null)
            {
                //Converting your session variable value to integer
                int convertKey = Convert.ToInt32(Session["BookId"]);
                var bookings = db.Bookings.Where(c => c.BookingId == convertKey)
                  .Select(b => new SelectListItem
                  {
                      Value = b.BookingId.ToString(),
                      Text = "Booking id is: " + b.BookingId + " booked on " + b.BookingDate + "from" + b.BookingStartDate + " to" + b.BookingEnddate
                  });
                ViewBag.BookingId = new SelectList(bookings, "Value", "Text");
                return View();
            }
            else
            {
                return View();
            }

           
            
          
            

           
           
            //ViewBag.BookingId = new SelectList(db.Bookings, "BookingId", "FirstName");
            //ViewBag.MemberId = new SelectList(db.Members, "MemberId", "Email");
            //return View();
        }

        // POST: Registrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MemberId,BookingId")] Registration registration)
        {
            bool bl = (db.Registrations.Any(a => a.MemberId == registration.MemberId && a.BookingId == registration.BookingId));
            if (!bl)
            {
                if (ModelState.IsValid)
                {

                    db.Registrations.Add(registration);
                    db.SaveChanges();
                    try
                    {
                        string BookingDetails = db.Bookings.Where(m => m.BookingId == registration.BookingId).ToList().Single().BookingId.ToString();
                        string status = db.Bookings.Where(m => m.BookingId == registration.BookingId).ToList().Single().BookingStatus.ToString();
                        string Bookingdate = db.Bookings.Where(m => m.BookingId == registration.BookingId).ToList().Single().BookingDate.ToShortDateString();
                        string toEmail = db.Members.Where(m => m.MemberId == registration.MemberId).ToList().Single().Email.ToString();
                        string subject = "Booking Confirmation: Tents&Trailers";
                        string contents = "Your Booking has been confirmed: " + BookingDetails + " Status " + status + "on" + Bookingdate;

                        EmailSender mail = new EmailSender();
                        mail.Send("gaikwadsana@gmail.com",toEmail, subject, contents);
                        Session["Message"] = "Your booking details have been sent to registered email address!";
                      
                        return RedirectToAction("Index", "Home");
                    }
                    catch
                    {
                        return View();
                    }

                }

            }
            else
            {
                ViewBag.Result = "Booking Error: You've already been enrolled";
            }
            string currentUserId = User.Identity.GetUserId();
            var fullName = db.Members.Where(m => m.UserId == currentUserId)
               .Select(a => new SelectListItem
               {
                   Value = a.MemberId.ToString(),
                   Text = a.FirstName + " " + a.LastName
               });
            ViewBag.MemberId = new SelectList(fullName, "Value", "Text");

            var bookings = db.Bookings
               .Select(b => new SelectListItem
               {
                   Value = b.BookingId.ToString(),
                   Text = "Booking id is: " + b.BookingId + " booked on " + b.BookingDate + "from" + b.BookingStartDate + " to" + b.BookingEnddate
               });
            ViewBag.BookingId = new SelectList(bookings, "Value", "Text");
            return View(registration);
        }


        // GET: Registrations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookingId = new SelectList(db.Bookings, "BookingId", "BookingStatus", registration.BookingId);
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "FirstName", registration.MemberId);
            return View(registration);
        }

        // POST: Registrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,MemberId,BookingId")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookingId = new SelectList(db.Bookings, "BookingId", "BookingStatus", registration.BookingId);
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "FirstName", registration.MemberId);
            return View(registration);
        }

        // GET: Registrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // POST: Registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Registration registration = db.Registrations.Find(id);
            db.Registrations.Remove(registration);
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
        public JsonResult GetBookings()
        {
            var events = db.Bookings.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
