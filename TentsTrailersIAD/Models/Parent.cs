using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TentsTrailersIAD.Models
{
    public class Parent
    {
        public Member Member { get; set; }

        public BulkEmail BulkEmail { get; set; }
    }
}