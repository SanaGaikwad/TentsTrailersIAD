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
          

            return View();
        }

        public ActionResult SiteMap()
        {


            return View();
        }

        public ActionResult Contact()
        {
            return View(new SendEmailViewModel());
        }
             [HttpPost]
        public ActionResult Contact(SendEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;

                    

                    EmailSender es = new EmailSender();
                    es.Send(toEmail, subject, contents);

                    ViewBag.Result = "Thank You! We will get back to you shortly.";

                    ModelState.Clear();

                    return View(new SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        


        }
        public ActionResult ContactInfo()
        {
            return View();
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
                var from = "";
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
                        message.Attachments.Add(new Attachment(objModelMail.Upload.InputStream, fileName));
                    }
                    message.IsBodyHtml = false;

                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential networkCredential = new NetworkCredential(from, "");
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