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
    public class UserRole
    {
        public int RoleID { get; set; }
        public int UserID { get; set; }

    }
}