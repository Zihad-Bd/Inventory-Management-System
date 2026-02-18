using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class BaseEquipment
    {
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public int Quantity { get; set; }
        public int Stock {  get; set; } 
        public DateTime EntryDate { get; set; }
        public DateTime ReceiveDate { get; set; }

        public List<BaseEquipment> ListEquipment()
        {
            List<BaseEquipment> equipmentList = new List<BaseEquipment>();
            string connString = ConfigurationManager.ConnectionStrings["connstring"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);
            sqlConnection.Open();
            string commandText = "SELECT * FROM Equipments";
            SqlCommand cmd = new SqlCommand(commandText, sqlConnection);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Clear();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    BaseEquipment equipmentObj = new BaseEquipment();
                    equipmentObj.EquipmentId = Convert.ToInt32(reader["EquipmentId"].ToString());
                    equipmentObj.EquipmentName = reader["EquipmentName"].ToString();
                    equipmentObj.Quantity = Convert.ToInt32(reader["Quantity"].ToString());
                    equipmentObj.Stock = Convert.ToInt32(reader["Stock"].ToString());
                    equipmentObj.EntryDate = Convert.ToDateTime(reader["EntryDate"].ToString());
                    equipmentObj.ReceiveDate = Convert.ToDateTime(reader["ReceiveDate"].ToString());
                    equipmentList.Add(equipmentObj);
                }
            }
            return equipmentList;
        }

        public int saveEquipment()
        {
            string connString = ConfigurationManager.ConnectionStrings["connstring"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);
            sqlConnection.Open();
            string commandText = "INSERT INTO Equipments(EquipmentName, Quantity, Stock, EntryDate, ReceiveDate) VALUES(@EquipmentName, @Quantity, @Stock, @EntryDate, @ReceiveDate)";
            SqlCommand cmd = new SqlCommand(commandText, sqlConnection);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@EquipmentName", this.EquipmentName));
            cmd.Parameters.Add(new SqlParameter("@Quantity", this.Quantity));
            cmd.Parameters.Add(new SqlParameter("@Stock", this.Quantity));
            cmd.Parameters.Add(new SqlParameter("@EntryDate", this.EntryDate));
            cmd.Parameters.Add(new SqlParameter("@ReceiveDate", this.ReceiveDate));
            int returnValue = cmd.ExecuteNonQuery();
            cmd.Dispose();
            sqlConnection.Close();
            return returnValue;
        }

        public int updateEquipment()
        {
            string connString = ConfigurationManager.ConnectionStrings["connstring"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connString);
            sqlConnection.Open();
            string commandText = "UPDATE Equipments SET EquipmentName = @EquipmentName, Quantity = @Quantity, ReceiveDate = @ReceiveDate WHERE EquipmentId = @EquipmentId;";
            SqlCommand cmd = new SqlCommand(commandText, sqlConnection);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@EquipmentName", this.EquipmentName));
            cmd.Parameters.Add(new SqlParameter("@Quantity", this.Quantity));
            cmd.Parameters.Add(new SqlParameter("@ReceiveDate", this.ReceiveDate));
            cmd.Parameters.Add(new SqlParameter("@EquipmentId", this.EquipmentId));
            int returnValue = cmd.ExecuteNonQuery();
            cmd.Dispose(); 
            sqlConnection.Close();
            return returnValue;
        }
    }
}