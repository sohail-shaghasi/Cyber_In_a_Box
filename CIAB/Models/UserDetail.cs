using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CIAB.Models
{
    public class UserDetail
    {

        public string UserId { get; set; }


        [UIHint("String")]
        public string  FullName { get; set; }

         [UIHint("String")]
        public string UserName { get; set; }

         [UIHint("EmailAddress")]
        public string Email { get; set; }


        [ReadOnly(true)]
        //[UIHint("Password")]
        public string pass { get; set; }

    }
}