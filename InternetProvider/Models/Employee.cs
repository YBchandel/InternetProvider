using System.ComponentModel.DataAnnotations;

namespace InternetProvider.Models
{
    public class Employee
    {

        [Required(ErrorMessage = "Required")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Enter Valid 8 letter Employee Id")]
        public string Emp_Id{ get; set; }


        [Required(ErrorMessage = "Number Must be 6 degits is required.")]
        public string First_name { get; set; }


        [Required(ErrorMessage = "Last name is required.")]
        public string Last_name { get; set; }


        [Required(ErrorMessage = "Email  is required.")]
        [EmailAddress(ErrorMessage = "Email  is required.")]
        public string Email { get; set; }




        [Required(ErrorMessage = "Department is required.")]
        public string Department { get; set; }


        [Required(ErrorMessage = "Position is required.")]
        public string Position { get; set; }




        [Required(ErrorMessage = "Number Must be 10 degits is required.")]
        //[MinLength(10, ErrorMessage = "Number Must be 10 degits is required.")]
       
        public long Phone { get; set; }

     


    }
}
