using InternetProvider.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using System.Reflection;

namespace InternetProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        public AdminController(IConfiguration _configuration) 
        {
              Configuration = _configuration;
        }

        [HttpPost("register")]
        public IActionResult InsertAdmin(Create_Admin adminDto)
        {
            string connectionString = Configuration.GetConnectionString("DefaultString");

            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand("select create_admin(@p_admin_id,@p_admin_password)", connection))
                    {

                        command.CommandType = CommandType.Text;
                        //   command.CommandText = "create_employee(@p_emp_id,@p_first_name,@p_last_name,@p_email,@p_phone,@p_department,@p_position,@p_status,@p_requested_date)";
                        string username = adminDto.Admin_Id;
                        string password = adminDto.EncryptedPassword;
                        var key = "E546C8DF278CD5931069B522E695D4F2";

                        var encryptedPass = PasswordConfig.EncryptString(password, key);
                        command.Parameters.AddWithValue("@p_admin_id", username);
                        command.Parameters.AddWithValue("@p_admin_password",encryptedPass);

                        // command.ExecuteNonQuery();
                        object result = command.ExecuteScalar();
                    }
                }

                return Ok("Admin record inserted successfully.");
            }
            catch (Exception ex)
            {
                // Handle exceptions and return appropriate response
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }




        //======================================= Authenticate ============================
        [HttpPost("login")]
        public IActionResult GetAdminDetails(ReturnAdmin returnAdmin)
        {

            string connectionString =Configuration.GetConnectionString("DefaultString");

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();



                    using (NpgsqlCommand command = new NpgsqlCommand("select is_admin_valid(@p_admin_id,@p_admin_password)", connection))
                    {
                        command.CommandType = CommandType.Text;



                        var username = returnAdmin.Admin_Id;
                        var pass = returnAdmin.DecryptedPassword;



                        command.Parameters.AddWithValue("@p_admin_id", username);
                        command.Parameters.AddWithValue("@p_admin_password", pass);



                        bool result = (bool)command.ExecuteScalar();

                        if (result == true)
                        {
                            return Ok("Admin login authentication successfully.");
                        }
                        else
                        {
                            return NotFound("Unuthorized login credintials");



                        }
                    }



                }



            }
            catch (Exception ex)
            {
                // Handle exceptions and return appropriate response
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

    }
}
