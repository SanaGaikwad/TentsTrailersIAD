using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TentsTrailersIAD;
using TentsTrailersIAD.Models;
using TentsTrailersIAD.Utils;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.AspNet.Identity;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace TentsTrailersIAD.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private TentsTrailersIADEntities db = new TentsTrailersIADEntities();
        
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            return View(db.Ratings.ToList());
        }

        public ActionResult SiteMap()
        {
           var campid = db.Campsites.OrderByDescending(p => p.CampId).First().CampId;
            string currlocation = db.Campsites.Where(c => c.CampId == campid).ToList().Single().Location.ToString();
            ViewBag.locate = currlocation;
            return View();
        }
        // Weather information from Accuweather API
        public ActionResult APIView()
        {


            return View();
        }

        // Contact us method
        public ActionResult Contact()
        {
            return View(new BulkEmail());
        }
        [HttpPost]
        public ActionResult Contact(BulkEmail model, HttpPostedFileBase fileUploader)
        {
            if (ModelState.IsValid)
            {
                //var currentUser = User.Identity.GetUserId();
                //String email = db.AspNetUsers.Where(m => m.Id == currentUser).ToList().Single().Email.ToString();
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.Subject = model.Subject;
                message.Body = model.Body;
                var from = model.From;

                message.To.Add(new MailAddress("gaikwadsana@gmail.com"));
                if (model.Upload != null)
                {
                    string fileName = Path.GetFileName(model.Upload.FileName);
                    message.Attachments.Add(new System.Net.Mail.Attachment(model.Upload.InputStream, fileName));
                }
                message.IsBodyHtml = false;

                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential networkCredential = new NetworkCredential("gaikwadsana@gmail.com", "s@n@_2210_");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = networkCredential;
                message.From = new MailAddress(from);
                smtp.Port = 587;
                smtp.Send(message);
                ViewBag.Result = "Thank You! We will get back to you shortly.";

                ModelState.Clear();

                return View(new BulkEmail());
            }
            else
            {
                return View();
            }

        }
        //GET
        // Contact us method for Administrator
        public ActionResult ContactInfo()
        {
            var vm = new Parent();
            vm.Members = db.Members
                             .Select(a => new SelectListItem()
                             {
                                 Value = a.Email,
                                 Text = a.Email
                             })
                             .ToList();
            return View(vm);
            

        }

        // Sending emails to members
        [HttpPost]
        public ActionResult ContactInfo(Parent objModelMail, HttpPostedFileBase fileUploader)
        {
            if (ModelState.IsValid)
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.Subject = objModelMail.BulkEmail.Subject;
                message.Body = objModelMail.BulkEmail.Body;
                var from = "gaikwadsana@gmail.com";
                foreach (var email in objModelMail.SelectedMembers)
                {
                   
                    message.To.Add(email);

                }

                // message.To.Add(new MailAddress("gaikwadsana@gmail.com"));
                if (objModelMail.BulkEmail.Upload != null)
                {
                    string fileName = Path.GetFileName(objModelMail.BulkEmail.Upload.FileName);
                    message.Attachments.Add(new System.Net.Mail.Attachment(objModelMail.BulkEmail.Upload.InputStream, fileName));
                }
                message.IsBodyHtml = false;
               

                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential networkCredential = new NetworkCredential("gaikwadsana@gmail.com", "s@n@_2210_");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = networkCredential;
                message.From = new MailAddress(from);
                smtp.Port = 587;
                smtp.Send(message);
                ViewBag.Result = "Thank You! We will get back to you shortly.";

                ModelState.Clear();
                var vm = new Parent();
                vm.Members = db.Members
                                 .Select(a => new SelectListItem()
                                 {
                                     Value = a.Email,
                                     Text = a.Email
                                 })
                                 .ToList();
                return View(vm);
               

            }
            else
            {
                return View();
            }
        }

    }
}