using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customer.ViewModel
{
    public class cityViewModel
    {
        public int cid { get; set; }
        public string name { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}