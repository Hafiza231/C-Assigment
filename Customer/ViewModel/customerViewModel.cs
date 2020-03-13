using Customer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Customer.ViewModel
{
    public class customerViewModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = Validation.name)]
        public string name { get; set; }
        [Required(ErrorMessage = Validation.address1)]
        public string address1 { get; set; }
        [Required(ErrorMessage = Validation.address2)]
        public string address2 { get; set; }
       
        public Nullable<int> cid { get; set; }
        [Required(ErrorMessage = Validation.country)]
        public string country { get; set; }
        [Required(ErrorMessage = Validation.postcode)]
        public Nullable<int> postcode { get; set; }
        [Required(ErrorMessage = Validation.email)]
        public string email { get; set; }
        [Required(ErrorMessage = Validation.mobile)]
        public Nullable<int> mobile { get; set; }
        [Required(ErrorMessage = Validation.birthdate)]
        public Nullable<System.DateTime> birthdate { get; set; }
        public Nullable<bool> active { get; set; }
        public Nullable<System.DateTime> createddate { get; set; }
        public Nullable<System.DateTime> updateddate { get; set; }
        [Required(ErrorMessage = Validation.city)]
        public string city { get; set; }

       
    }
}