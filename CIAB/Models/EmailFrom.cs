using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace CIAB.Models
{
    public class EmailFrom
    {

        [Required(ErrorMessage = "Please input your Name.")]
        public string inputName { get; set; }

        [Required(ErrorMessage = "Please input your Email Address.")]
        //[DataType(DataType.EmailAddress, ErrorMessage = "Please input a valid email address.")]
        [EmailAddress(ErrorMessage = "Please input a valid email address.")]
        public string inputEmail { get; set; }

        [Required(ErrorMessage = "Please input the Subject.")]
        public string Subject { get; set; }

        [Required (ErrorMessage="Please Select a product.")]
        public string optProduct { get; set; }
        [Required(ErrorMessage = "Please input a message.")]
        public string Message { get; set; }


    }
}