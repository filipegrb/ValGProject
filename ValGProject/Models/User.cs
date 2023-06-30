using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ValGProject.Models
{
    public class User
    {
        public int Id { get; set; }

        [DisplayName("User Name:")]
        [Required(ErrorMessage = "User Name is Required")]
        public string UserName { get; set; }

        [DisplayName("Password:")]
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
