using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TentsTrailersIAD.Models
{
    public class BulkEmail
    {

        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
     
        public HttpPostedFileBase Upload { get; set; }
    }
}