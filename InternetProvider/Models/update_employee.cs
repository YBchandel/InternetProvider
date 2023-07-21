using System.ComponentModel.DataAnnotations;

namespace InternetProvider.Models
{
    public class update_employee
    {

        public int id { get; set; }


        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }

        public string Remark { get; set; }


    }
}
