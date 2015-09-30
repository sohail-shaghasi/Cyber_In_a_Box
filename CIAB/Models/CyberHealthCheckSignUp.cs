using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIAB.Models
{
    public class CyberHealthCheckSignUp
    {
        public string fullName{get; set;}
        public string LastName{get; set;}
        public string JobTitle{get; set;}
        public string companyName{get; set;}
        public string inputEmail{get; set;}
        public int contactNumber { get; set; }
        public bool? RecievemarketingEmails{get; set;}
        public bool? iAgree { get; set; }
    }
}