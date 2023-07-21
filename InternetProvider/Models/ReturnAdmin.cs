using System.ComponentModel.DataAnnotations;

namespace InternetProvider.Models
{
    public class ReturnAdmin
    {
       
        public string Admin_Id { get; set; }

      
        public string DecryptedPassword { get; set; }
    }
}
