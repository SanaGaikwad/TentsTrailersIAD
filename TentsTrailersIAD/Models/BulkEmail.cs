using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TentsTrailersIAD.Models
{
    public class BulkEmail
    {

        public string To { get; set; }

        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress]
        public string From { get; set; }

        public string Subject { get; set; }

        [Required(ErrorMessage ="Please enter message body")]
        [AllowHtml]
        public string Body { get; set; }
     
        
        public HttpPostedFileBase Upload { get; set; }
    }
}