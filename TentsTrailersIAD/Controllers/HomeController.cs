using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TentsTrailersIAD;
using TentsTrailersIAD.Models;
using TentsTrailersIAD.Utils;

namespace TentsTrailersIAD.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
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
       
    }
}