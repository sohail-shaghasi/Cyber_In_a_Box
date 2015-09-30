using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace CIAB.Models
{
    public class EmailFrom
    {

        public EmailFrom()
        {

        }



        [Required, Display(Name="Input Your Name ")]
        public string inputName { get; set; }

        [Required, Display(Name="Input Your Email Address "), EmailAddress]
        public string inputEmail { get; set; }

        public string Subject { get; set; }

        public string optProduct { get; set; }

        [Required]
        public string Message { get; set; }


    }
}