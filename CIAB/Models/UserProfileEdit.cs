using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CIAB.Models
{
    public class UserProfileEdit
    {
        public int UserID { get; set; }
        
        public string FullName { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        [StringLength(150)]
        public string email { get; set; }

        public string Website { get; set; }

        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }


        //------------------------------------------------------------------------

        


    }
}