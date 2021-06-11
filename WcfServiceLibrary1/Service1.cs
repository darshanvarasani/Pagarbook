using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=G:\\Documents\\Darshan\\sem-6\\SOC\\WcfServiceLibrary1\\WcfServiceLibrary1\\Database1.mdf;Integrated Security=True;";
        public List<Employee> GetAllData()
        {
            List<Employee> e = new List<Employee>();
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Employee", con);
            SqlDataReader reader = cmd.ExecuteReader();                        
            while(reader.Read())
            {
                Employee emp = new Employee();
                emp.Name = reader["Name"].ToString();
                emp.Email_id = reader["Email"].ToString();
                emp.DOB = (DateTime)reader["DOB"];
                emp.Gender = reader["Gender"].ToString();
                emp.Salary = (double)reader["Salary"];
                emp.Type = reader["Type"].ToString();
                emp.Present = reader["Present"].ToString().ToCharArray();
                e.Add(emp);
            }
            con.Close();
            return e;
        }

        public Employee GetDataUsingId(string email_id)
        {            
            Employee e = new Employee();
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Employee where Email=@email_id", con);
            cmd.Parameters.AddWithValue("@email_id", email_id);
            SqlDataReader reader = cmd.ExecuteReader();            
            if(reader.Read())
            {
                e.Name = reader["Name"].ToString();
                e.Email_id = reader["Email"].ToString();
                e.DOB = (DateTime)reader["DOB"];
                e.Gender = reader["Gender"].ToString();
                e.Salary = (double)reader["Salary"];
                e.Type = reader["Type"].ToString();
                e.Present = reader["Present"].ToString().ToCharArray();
                con.Close();
                return e;
            }
            else
            {
                con.Close();
                return null;
            }
        }

        public List<Employee> GetDataUsingType(string type)
        {
            List<Employee> e = new List<Employee>();
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Employee where Type=@type", con);
            cmd.Parameters.AddWithValue("@type", type);
            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                Employee emp = new Employee();
                emp.Name = reader["Name"].ToString();
                emp.Email_id = reader["Email"].ToString();
                emp.DOB = (DateTime)reader["DOB"];
                emp.Gender = reader["Gender"].ToString();
                emp.Salary = (double)reader["Salary"];
                emp.Type = reader["Type"].ToString();
                emp.Present = reader["Present"].ToString().ToCharArray();
                e.Add(emp);
            }
            con.Close();
            return e;
        }

        public Boolean AddEmployee(Employee e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into Employee(Email,Name,DOB,Gender,Type,Salary,Present) values(@Email,@Name,@DOB,@Gender,@Type,@Salary,@Present)", con);
            cmd.Parameters.AddWithValue("@Email", e.Email_id);
            cmd.Parameters.AddWithValue("@Name", e.Name);
            cmd.Parameters.AddWithValue("@DOB", e.DOB);
            cmd.Parameters.AddWithValue("@Gender", e.Gender);
            cmd.Parameters.AddWithValue("@Type", e.Type);
            cmd.Parameters.AddWithValue("@Salary", e.Salary);
            cmd.Parameters.AddWithValue("@Present", e.Present);
            int result = cmd.ExecuteNonQuery();
            con.Close();
            if(result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean EditEmployee(Employee e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("update Employee set Name=@Name,DOB=@DOB,Gender=@Gender,Type=@Type,Salary=@Salary,Present=@Present where Email=@email", con);
            cmd.Parameters.AddWithValue("@email", e.Email_id);
            cmd.Parameters.AddWithValue("@Name", e.Name);
            cmd.Parameters.AddWithValue("@DOB", e.DOB);
            cmd.Parameters.AddWithValue("@Gender", e.Gender);
            cmd.Parameters.AddWithValue("@Type", e.Type);
            cmd.Parameters.AddWithValue("@Salary", e.Salary);
            cmd.Parameters.AddWithValue("@Present", e.Present);
            int result = cmd.ExecuteNonQuery();
            con.Close();
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean DeleteEmployee(string email_id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from Employee where Email=@email_id", con);
            cmd.Parameters.AddWithValue("@email_id", email_id);
            int result = cmd.ExecuteNonQuery();
            con.Close();
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public double GetSalary(string email_id)
        {
            Employee e = GetDataUsingId(email_id);
            double temp = e.Salary;
            int days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            temp = temp / days;
            double salary = 0;
            foreach(char c in e.Present)
            {
                if(c == 'P')
                {
                    salary += temp;
                }
            }
            return salary;
        }
    }
}
