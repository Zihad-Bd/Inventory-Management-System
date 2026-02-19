using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Models
{
    public class BaseMember
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string ServiceType { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string DashBoardPageURL { get; set; }

        public DataTable ValidateMemberAsDataTable(string UserName, string password)
        {
            DataTable dataTable = new DataTable();
            string connstring = ConfigurationManager.ConnectionStrings["connstring"].ToString();
            //string connstring2 = ConfigurationManager.AppSettings["connstringAppSetting"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connstring);
            sqlConnection.Open();
            string CommandText = "Select * FROM Members WHERE Name='" + UserName + "' AND Password='" + password + "'";
            SqlCommand cmd = new SqlCommand(CommandText, sqlConnection);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Clear();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);
            cmd.Dispose();
            sqlConnection.Close();
            return dataTable;
        }

        public List<BaseMember> ValidateMemberAsList(string UserName, string password)
        {
            List<BaseMember> membersList = new List<BaseMember>();
            string connstring = ConfigurationManager.ConnectionStrings["connstring"].ToString();
            //string connstring2 = ConfigurationManager.AppSettings["connstringAppSetting"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connstring);
            sqlConnection.Open();
            string CommandText = "Select * FROM Members";
            SqlCommand cmd = new SqlCommand(CommandText, sqlConnection);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Clear();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    BaseMember memberObj = new BaseMember();
                    memberObj.Id = Convert.ToInt32(reader["Id"].ToString());
                    memberObj.Name = reader["Name"].ToString();
                    memberObj.Age = reader["Age"] != DBNull.Value ? Convert.ToInt32(reader["Age"].ToString()) : 0;
                    memberObj.ServiceType = reader["ServiceType"] != DBNull.Value ? reader["Age"].ToString() : "";
                    memberObj.Password = reader["Password"] != DBNull.Value ? reader["Password"].ToString() : "";
                    memberObj.Role = reader["Role"] != DBNull.Value ? reader["Role"].ToString() : "";
                    memberObj.DashBoardPageURL = reader["DashBoardPageURL"] != DBNull.Value ? reader["DashBoardPageURL"].ToString() : "";
                    membersList.Add(memberObj);
                }
            }
            cmd.Dispose();
            sqlConnection.Close();
            return membersList;
        }

        public bool UserExist(string userName)
        {
            List<BaseMember> membersList = new List<BaseMember>();
            bool userExist = false;
            string connString = ConfigurationManager.ConnectionStrings["connstring"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);
            sqlConnection.Open();
            string CommandText = "SELECT * FROM Members WHERE Name = @Name";
            SqlCommand cmd = new SqlCommand(CommandText, sqlConnection);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@Name", userName));
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                userExist = true;
            }
            cmd.Dispose();
            sqlConnection.Close();
            return userExist;
        }

        public bool UpdatePassword(string userName, string newPassword)
        {
            bool statusUpdated = false;
            string connString = ConfigurationManager.ConnectionStrings["connstring"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);
            sqlConnection.Open();
            string CommandText = "UPDATE Members SET Password = @Password WHERE Name = @Name";
            SqlCommand cmd = new SqlCommand(CommandText, sqlConnection);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@Password", newPassword));
            cmd.Parameters.Add(new SqlParameter("@Name", userName));
            int returnValue = cmd.ExecuteNonQuery();
            if (returnValue > 0)
            {
                statusUpdated = true;
            }
            cmd.Dispose();
            sqlConnection.Close();
            return statusUpdated;
        }

        public bool SaveUser(string userName, string password)
        {
            bool statusUpdated = false;
            string connString = ConfigurationManager.ConnectionStrings["connstring"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);
            sqlConnection.Open();
            string CommandText = "INSERT INTO Members (Name, Password) VALUES(@Name, @Password)";
            SqlCommand cmd = new SqlCommand(CommandText, sqlConnection);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@Password", password));
            cmd.Parameters.Add(new SqlParameter("@Name", userName));
            int returnValue = cmd.ExecuteNonQuery();
            if (returnValue > 0)
            {
                statusUpdated = true;
            }
            cmd.Dispose();
            sqlConnection.Close();
            return statusUpdated;
        }
    }
}

