using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace projekt1.net.Models
{
    public class JobApplication
    {
        public int Id { get; set; }
        public int OfferId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Contact Phone")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Contact Email")]
        public string EmailAddress { get; set; }
        public bool ContactAgreement { get; set; }
        [Display(Name = "Description")]
        [MinLength(5)]
        public string Description { get; set; }        
        

        public string CvUrl { get; set; }
        //[NotMapped]
        //[Required(ErrorMessage = "You mus provide CV")]
        //public IFormFile File { get; set; }
    }
}
