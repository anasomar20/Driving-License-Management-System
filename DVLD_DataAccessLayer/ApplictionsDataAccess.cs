using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public class clsApplicationsDataAccess
    {
        public static bool GetApplicationInfoByApplicationID(int ApplicationID, ref int PersonID, ref DateTime ApplicationDate
            , ref int ApplicationTypeID, ref byte ApplicationStatus, ref DateTime LastStatusDate, ref float PaidFees, ref int UserID)
        {
            bool IsFound = false;

            string query = "Select * from Applications Where ApplicationID=@ApplicationID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    PersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = (byte)reader["ApplicationStatus"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = Convert.ToSingle( reader["PaidFees"]);
                    UserID = (int)reader["CreatedByUserID"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }
        
       
        public static bool UpdateApplication(int ApplicationID, int PersonID, DateTime ApplicationDate
            , int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int UserID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"Update Applications 
                            Set ApplicantPersonID = @PersonID,
                            Applicationstatus = @Applicationstatus,
                            ApplicationDate = @ApplicationDate,
                            ApplicationTypeID = @ApplicationTypeID,
                            LastStatusDate = @LastStatusDate,
                            PaidFees = @PaidFees,
                            CreatedByUserID = @UserID
                            Where ApplicationID = @ApplicationID;";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@UserID", UserID);

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
        public static bool UpdateStatus(int ApplicationID, byte ApplicationStatus)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"Update Applications 
                            Set 
                            Applicationstatus = @Applicationstatus,                          
                            LastStatusDate = @LastStatusDate
                            Where ApplicationID = @ApplicationID;";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);


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

        public static bool DeleteApplication(int ApplicationID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"Delete from Applications Where ApplicationID = @ApplicationID";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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
        public static int AddNewApplication(int PersonID, DateTime ApplicationDate
            , int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int UserID)
        {
            int ApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"INSERT INTO Applications(ApplicantPersonID,ApplicationDate,ApplicationTypeID,ApplicationStatus,LastStatusDate,PaidFees,CreatedbyUserID)
                            Values(@PersonID,@ApplicationDate,@ApplicationTypeID,@ApplicationStatus,@LastStatusDate,@PaidFees,@UserID);
                            SELECT SCOPE_IDENTITY();";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (int.TryParse(result.ToString(), out int insertedID))
                {
                    ApplicationID = insertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return ApplicationID;
        }

        public static DataTable GetAllApplications()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"
SELECT        Applications.ApplicationID, 
(People.FirstName + ' ' + People.SecondName+ ' ' +People.ThirdName+ ' ' +People.LastName) as FullName,
	Applications.ApplicationDate, ApplicationTypes.ApplicationTypeTitle,CASE Applications.ApplicationStatus 
        WHEN 1 THEN 'New' 
        WHEN 2 THEN 'Cancle' 
		WHEN 3 THEN 'Complete'
		 End as Status ,
                         Applications.LastStatusDate, Applications.PaidFees, Applications.CreatedByUserID
FROM            Applications INNER JOIN
                         People ON Applications.ApplicantPersonID = People.PersonID INNER JOIN
                         ApplicationTypes ON Applications.ApplicationTypeID = ApplicationTypes.ApplicationTypeID";

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

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"Select found = 1 from Applications Where ApplicationID=@ApplicationID";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    IsFound = true;
                }
            }
            catch (Exception ex)
            {
                IsFound = true;
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }
        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return (GetActiveApplicationID(PersonID, ApplicationTypeID) != 1);
        }
        public static int GetActiveApplicationID(int PersonID,int ApplicationTypeID)
        {
            int ActiveApplicationID = -1;

            string query = "Select ActiveApplicationID = ApplicationID from Applications Where ApplicantPersonID = @PersonID and ApplicationTypeID = @ApplicationTypeID and ApplicationStatus = 1;";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);


            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (int.TryParse(result.ToString(), out int insertedID))
                {
                    ActiveApplicationID = insertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return ActiveApplicationID;
        }
        public static int GetActiveApplicationIDForLicenseClass(int PersonID,int ApplicationTypeID, int LicenseClassID)
        {
            int ActiveApplicationID = -1;

            string query = @"Select ActiveApplicationID = Applications.ApplicationID 
                from Applications INNER JOIN LocalDrivingLicenseApplications On Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                 Where ApplicantPersonID = @PersonID 
                    and ApplicationTypeID = @ApplicationTypeID
                    and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                    and ApplicationStatus = 1;";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (int.TryParse(result.ToString(), out int insertedID))
                {
                    ActiveApplicationID = insertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return ActiveApplicationID;
        }
    }
}
