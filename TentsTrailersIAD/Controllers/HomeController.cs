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
            //ViewBag.message = Session["Message"];
            //Session["Message"] = "";
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

        public ActionResult APIView()
        {


            return View();
        }

        public ActionResult Contact()
        {
            return View(new BulkEmail());
        }
        [HttpPost]
        public ActionResult Contact(BulkEmail model, HttpPostedFileBase fileUploader)
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
        //GET
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
            //ViewBag.Email = new MultiSelectList(db.Members, "Email", "Email");
            //return View();

        }

        //    [HttpPost]
        //    public ActionResult ContactInfo(Parent objModelMail, HttpPostedFileBase fileUploader)
        //    {
        //        if (ModelState.IsValid)
        //        {

        //            MailMessage message = new MailMessage();
        //            SmtpClient smtp = new SmtpClient();
        //            message.Subject = objModelMail.BulkEmail.Subject;
        //            message.Body = objModelMail.BulkEmail.Body;
        //            var from = "gaikwadsana@gmail.com";
        //            message.From = new MailAddress(from);
        //            //db connection
        //            SqlCommand cmd = null;
        //            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //           string queryString = @"SELECT EMAIL FROM MEMBER WHERE EMAIL = EMAIL";

        //            using (SqlConnection connection = new SqlConnection(connectionString))
        //            {
        //                SqlCommand command =
        //                    new SqlCommand(queryString, connection);
        //                connection.Open();
        //                cmd = new SqlCommand(queryString);
        //                cmd.Connection = connection;
        //                if (objModelMail.BulkEmail.Upload != null)
        //                {
        //                    string fileName = Path.GetFileName(objModelMail.BulkEmail.Upload.FileName);
        //                    message.Attachments.Add(new System.Net.Mail.Attachment(objModelMail.BulkEmail.Upload.InputStream, fileName));
        //                }
        //                message.IsBodyHtml = false;

        //                smtp.Host = "smtp.gmail.com";
        //                smtp.EnableSsl = true;
        //                NetworkCredential networkCredential = new NetworkCredential(from, "s@n@_2210_");
        //                smtp.UseDefaultCredentials = false;
        //                smtp.Credentials = networkCredential;
        //                smtp.Port = 587;

        //                SqlDataReader reader = cmd.ExecuteReader();

        //                // Call Read before accessing data.
        //                while (reader.Read())
        //                {

        //                    var to = new MailAddress(reader["EMAIL"].ToString());
        //                    message.To.Add(to);
        //                }

        //                    smtp.Send(message);


        //                    // Call Close when done reading.
        //                    reader.Close();


        //                    ViewBag.Message = "Sent";

        //                ViewBag.Email = new MultiSelectList(db.Members, "Email", "Email");

        //                return View("ContactInfo", objModelMail);
        //            }



        //        }
        //        else
        //        {
        //            return View();
        //        }
        //    }

        //}
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