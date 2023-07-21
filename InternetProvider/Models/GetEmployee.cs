using System.ComponentModel.DataAnnotations;

namespace InternetProvider.Models
{
    public class GetEmployee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Emp id is required.")]
        [StringLength(8)]
        public string Emp_Id { get; set; }


        [Required(ErrorMessage = " required.")]
        public string First_name { get; set; }


        [Required(ErrorMessage = "Last name is required.")]
        public string Last_name { get; set; }


        [Required(ErrorMessage = "Email Id is required.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Number Must be 10 degits is required.")]
        [StringLength(10)]
        public long Phone { get; set; }


        [Required(ErrorMessage = "Department is required.")]
        public string Department { get; set; }


        [Required(ErrorMessage = "Position is required.")]
        public string Position { get; set; }


        public string Remark { get; set; }



        [Required]
        public string Status { get; set; }


        public DateTime? requested_date { get; set; } //---------------
        public DateTime? action_date { get; set; } //----------------


    }
}
