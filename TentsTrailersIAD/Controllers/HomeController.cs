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

            return View(new BulkEmail());

        }

        [HttpPost]
        public ActionResult ContactInfo(BulkEmail objModelMail, HttpPostedFileBase fileUploader)
        {
            if (ModelState.IsValid)
            {
                
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.Subject = objModelMail.Subject;
                message.Body = objModelMail.Body;
                var from = "gaikwadsana@gmail.com";
                message.From = new MailAddress(from);
                //db connection
                SqlCommand cmd = null;
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                string queryString = @"SELECT EMAIL FROM MEMBER WHERE EMAIL = EMAIL";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command =
                        new SqlCommand(queryString, connection);
                    connection.Open();
                    cmd = new SqlCommand(queryString);
                    cmd.Connection = connection;
                    if (objModelMail.Upload != null)
                    {
                        string fileName = Path.GetFileName(objModelMail.Upload.FileName);
                        message.Attachments.Add(new System.Net.Mail.Attachment(objModelMail.Upload.InputStream, fileName));
                    }
                    message.IsBodyHtml = false;

                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential networkCredential = new NetworkCredential(from, "s@n@_2210_");
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = networkCredential;
                    smtp.Port = 587;

                    SqlDataReader reader = cmd.ExecuteReader();

                    // Call Read before accessing data.
                    while (reader.Read())
                    {

                        var to = new MailAddress(reader["EMAIL"].ToString());
                        message.To.Add(to);
                    }
                        
                        smtp.Send(message);


                        // Call Close when done reading.
                        reader.Close();


                        ViewBag.Message = "Sent";
                       
                    
                    return View("ContactInfo", objModelMail);
                }

            

            }
            else
            {
                return View();
            }
        }

    }
}