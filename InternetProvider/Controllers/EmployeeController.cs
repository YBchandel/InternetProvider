using InternetProvider.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using System.Net.NetworkInformation;

namespace InternetProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IConfiguration Configuration;   /// ******** constructure for connection String
        public EmployeeController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

       // private readonly string connectionString; = "User ID=postgres;Password=Yash#20234;Host=localhost;Port=5432;Database=internet_provider;";
        [HttpPost]
        public IActionResult InsertEmployee(Employee employee)
        {
            string connectionString = Configuration.GetConnectionString("DefaultString");
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();



                    using (NpgsqlCommand command = new NpgsqlCommand("select create_employee(@p_emp_id,@p_first_name,@p_last_name,@p_email,@p_department,@p_position,@p_phone);", connection))
                    {



                        command.CommandType = CommandType.Text;
                        //   command.CommandText = "create_employee(@p_emp_id,@p_first_name,@p_last_name,@p_email,@p_phone,@p_department,@p_position,@p_status,@p_requested_date)";
                        command.Parameters.AddWithValue("@p_emp_id", employee.Emp_Id);
                        command.Parameters.AddWithValue("@p_first_name", employee.First_name);
                        command.Parameters.AddWithValue("@p_last_name", employee.Last_name);
                        command.Parameters.AddWithValue("@p_email", employee.Email);
                        
                        command.Parameters.AddWithValue("@p_department", employee.Department);
                        command.Parameters.AddWithValue("@p_position", employee.Position);
                        command.Parameters.AddWithValue("@p_phone", employee.Phone);
                       


                        command.ExecuteNonQuery();
                    }
                }



                return Ok("Employee record inserted successfully.");
            }
            catch (Exception ex)
            {
                // Handle exceptions and return appropriate response
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        //*************************************** Update *******************************
        // public update_employee


        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] update_employee employee)
        {
            string connectionString = Configuration.GetConnectionString("DefaultString");
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    //using (NpgsqlCommand command = new NpgsqlCommand("select update_status(@p_id,@p_status)", connection))
                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT update_employee_status(@p_id, @p_status, @p_remark);", connection))
                    {
                        command.CommandType = CommandType.Text;

                        command.Parameters.AddWithValue("@p_id", id);
                        command.Parameters.AddWithValue("@p_status", employee.Status);
                        command.Parameters.AddWithValue("@p_remark", employee.Remark);


                        object rowsAffected = command.ExecuteScalar();

                        if (rowsAffected != null)
                        {
                            return Ok($"Employee with ID {id} has been updated successfully.");
                        }
                        else
                        {
                            return NotFound($"Employee with ID {id} not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        //*************************************** GET *******************************
        //

        [HttpGet]
        public IActionResult GetEmployees()
        {
            string connectionString = Configuration.GetConnectionString("DefaultString");
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();



                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM get_employees()", connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            List<GetEmployee> employees = new List<GetEmployee>();



                            while (reader.Read())
                            {
                                GetEmployee emp = new GetEmployee
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    Emp_Id = reader["emp_id"].ToString(),
                                    First_name = reader["first_name"].ToString(),
                                    Last_name = reader["last_name"].ToString(),
                                    Email = reader["email"].ToString(),
                                   
                                    Department = reader["department"].ToString(),
                                    Position = reader["p_position"].ToString(),
                                    Status = reader["p_status"].ToString(),
                                    //requested_date = reader["p_requested_date"].ToString(),
                                    requested_date = reader["p_requested_date"] != DBNull.Value ? Convert.ToDateTime(reader["p_requested_date"]) : (DateTime?)null,
                                    action_date = reader["p_approval_date"] != DBNull.Value ? Convert.ToDateTime(reader["p_approval_date"]) : (DateTime?)null,
                                    //action_date = Convert.ToDateTime(reader["p_approval_date"]),
                                    //Phone = reader["phone"].ToString(),
                                    Phone = Convert.ToInt64(reader["phone"]),
                                    Remark = reader["remark"].ToString() 



                                };



                                employees.Add(emp);
                            }

                            employees = employees.OrderBy(e => e.Status == "Approved" ? 0 : e.Status == "pending" ? 1 : 2).ToList();

                            return Ok(employees);
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
        //********************************************** LOGIN **********************************************
        //[HttpPut("login")]
        //public IActionResult Admin(admin employee)
        //{
      //  string connectionString = Configuration.GetConnectionString("DefaultString");
        //    try
        //    {
        //        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        //        {
        //            connection.Open();

        //            //using (NpgsqlCommand command = new NpgsqlCommand("select update_status(@p_id,@p_status)", connection))
        //            using (NpgsqlCommand command = new NpgsqlCommand("select * from admin where admin_id = @admin_id and admin_password = @admin_password;", connection))
        //            {
        //                command.CommandType = CommandType.Text;

        //                command.Parameters.AddWithValue("@admin_id", employee.Admin_id);
        //                command.Parameters.AddWithValue("@admin_password", employee.Admin_Password);


        //                object rowsAffected = command.ExecuteScalar();

        //                if (rowsAffected != null)
        //                {
        //                    return Ok($"Admin LOGIN Sucissfull.");
        //                }
        //                else
        //                {
        //                    return NotFound($"Admin not found.");
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred: {ex.Message}");
        //    }
        //}
        //************************************** get **************************************************
        [HttpGet("{emp_id}")]
        public IActionResult GetEmployee(string emp_id)
        {
            string connectionString = Configuration.GetConnectionString("DefaultString");
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * from get_employee_id(@p_emp_id)", connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@p_emp_id", emp_id);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Get_id employee = new Get_id
                                {
                                    Emp_Id = reader["employee_id"].ToString(),
                                    First_name = reader["p_first_name"].ToString(),
                                    Last_name = reader["p_last_name"].ToString(),
                                    status = reader["p_status"].ToString(),
                                    //requested_date = Convert.ToDateTime(reader["p_requested_date"]),
                                    //action_date = Convert.ToDateTime(reader["p_approval_date"]),
                                    requested_date = reader["p_requested_date"] != DBNull.Value ? Convert.ToDateTime(reader["p_requested_date"]) : (DateTime?)null,
                                    action_date = reader["p_approval_date"] != DBNull.Value ? Convert.ToDateTime(reader["p_approval_date"]) : (DateTime?)null,

                                    Remark = reader["p_remark"].ToString(),
                                    

                                };

                                return Ok(employee);
                            }
                            else
                            {
                                return NotFound($"Employee with ID {emp_id} not found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        //*******************************************************************************************************
        //================================================ Get Employee By Status =================================



        [HttpGet]
        [Route("GetByStatus/{empStatus}")]
        public IActionResult GetEmployeesByStatus(string empStatus)
        {
            string connectionString = Configuration.GetConnectionString("DefaultString");



            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();



                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM get_employees_by_status(@status_value);", connection))
                    {
                        command.CommandType = CommandType.Text;



                        command.Parameters.AddWithValue("@status_value", empStatus);



                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            List<GetEmployee> employees = new List<GetEmployee>();



                            while (reader.Read())
                            {
                               GetEmployee emp = new GetEmployee
                                {
                                   Id = Convert.ToInt32(reader["id"]),
                                   Emp_Id = reader["emp_id"].ToString(),
                                   First_name = reader["first_name"].ToString(),
                                   Last_name = reader["last_name"].ToString(),
                                   Email = reader["email"].ToString(),

                                   Department = reader["department"].ToString(),
                                   Position = reader["p_position"].ToString(),
                                   Status = reader["p_status"].ToString(),
                                   //requested_date = reader["p_requested_date"].ToString(),
                                   requested_date = reader["p_requested_date"] != DBNull.Value ? Convert.ToDateTime(reader["p_requested_date"]) : (DateTime?)null,
                                   action_date = reader["p_approval_date"] != DBNull.Value ? Convert.ToDateTime(reader["p_approval_date"]) : (DateTime?)null,
                                   //action_date = Convert.ToDateTime(reader["p_approval_date"]),
                                   //Phone = reader["phone"].ToString(),
                                   Phone = Convert.ToInt64(reader["phone"]),
                                   Remark = reader["remark"].ToString()
                               };



                                employees.Add(emp);



                            }
                            return Ok(employees);
                        }
                    }
                }
            }



            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }



        }

        //************************************************************************************************************



    }






}
//********************************************************************************************



