﻿namespace CIAB.Models
{
    public class CyberHealthCheckSignUp
    {
        public string FullName{get; set;}
        public string LastName{get; set;}
        public string JobTitle{get; set;}
        public string CompanyName{get; set;}
        public string InputEmail{get; set;}
        public int ContactNumber { get; set; }
        public bool? RecieveMarketingEmails{get; set;}
        public bool? TermsAndConditions { get; set; }
    }
}