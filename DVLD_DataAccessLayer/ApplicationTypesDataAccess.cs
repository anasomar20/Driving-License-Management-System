using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public class clsApplicationTypesDataAccess
    {
        public static bool GetTypeInfoByApplicationID(int ApplicationTypeID, ref string Title,ref float Fees )
        {

            bool IsFound = false;

            string query = "SELECT [ApplicationTypeID]     ,[ApplicationTypeTitle]    ,[ApplicationFees] FROM [dbo].[ApplicationTypes]";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    Title = (string)reader["ApplicationTypeTitle"];
                    Fees = Convert.ToSingle( reader["ApplicationFees"]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                IsFound = false;
                clsLogger.Log(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }
        
        public static DataTable GetAllApplicationTypes()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"SELECT * from ApplicationTypes order by ApplicationTypeTitle;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }
        public static int AddNewApplicationType(string Title, float Fees)
        {
            int ApplicationTypeID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"Insert Into(ApplicationTypeTitle,ApplicationFees)
                             VALUES (@ApplicationTypeTitle,@ApplicationFees)
                            SELECT SCOPE_IDINTITY();";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeTitle", Title);
            command.Parameters.AddWithValue("@ApplicationFees", Fees);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (int.TryParse(result.ToString(), out int insertedID))
                {
                    ApplicationTypeID = insertedID;
                }
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return ApplicationTypeID;
        }
        public static bool UpdateApplicationType(int ApplicationTypeID,string Title, float Fees)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"Update ApplicationTypes 
                            Set ApplicationTypeTitle = @ApplicationTypeTitle,
                            ApplicationFees = @ApplicationFees
                            Where ApplicationTypeID = @ApplicationTypeID;";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeTitle", Title);
            command.Parameters.AddWithValue("@ApplicationFees", Fees);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

    }
}
