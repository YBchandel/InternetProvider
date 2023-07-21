using System.ComponentModel.DataAnnotations;

namespace InternetProvider.Models
{
    public class Create_Admin
    {
        [Required(ErrorMessage = "Admin Id is Required")]
        public string Admin_Id { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string EncryptedPassword { get; set; }
    }
}
