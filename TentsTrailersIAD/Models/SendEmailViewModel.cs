using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace TentsTrailersIAD.Models
{
    public class SendEmailViewModel
    {
      
        public string FromEmail { get; set; }

        
        public string ToEmail { get; set; }

        [Required(ErrorMessage = "Please enter a subject.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please enter the contents")]
        public string Contents { get; set; }

        public HttpPostedFileBase Upload { get; set; }


    }
}