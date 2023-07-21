using System.ComponentModel.DataAnnotations;

namespace InternetProvider.Models
{
    public class Get_id
    {
        
        [StringLength(8)]
        public string Emp_Id { get; set; }


      
        public string First_name { get; set; }


        [Required(ErrorMessage = "Last name is required.")]
        public string Last_name { get; set; }
        public string status { get; set; }
        public DateTime? requested_date { get; set; }
        public DateTime? action_date { get; set; }

        public string Remark { get; set; }



    }
}
