using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        public string pass { get; set; }
    }
}