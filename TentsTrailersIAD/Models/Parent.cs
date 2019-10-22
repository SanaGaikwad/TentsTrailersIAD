using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TentsTrailersIAD.Models
{
    public class Parent
    {
        public Member Member { get; set; }

        public BulkEmail BulkEmail { get; set; }
        public List<SelectListItem> Members { set; get; }
        public String[] SelectedMembers { set; get; }


    }
}