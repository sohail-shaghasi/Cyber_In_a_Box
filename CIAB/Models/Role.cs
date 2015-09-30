using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel.DataAnnotations;

namespace CIAB.Models
{
    public class Role
    {
        public Role()
        {

        }

        public int RoleID {get; set;}

        public string RoleName { get; set;}
    }
}