using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public class DetaintedDetainedLicensesDataAccess
    {
        public class clsDetainedLicensesDataAccess
        {
            public static bool GetDetainedLicenseInfoByID(int DetainID, ref int LicenseID, ref DateTime DetainDate,
                ref float FineFees, ref int CreatedByUserID, ref bool IsRelease, ref DateTime ReleaseDate
                , ref int ReleasedByUserID, ref int ReleaseApplicationID)
            {

                bool IsFound = false;

                string query = "Select * from DetainedLicenses Where DetainID=@DetainID";

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@DetainID", DetainID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        IsFound = true;
                        LicenseID = (int)reader["LicenseID"];
                        DetainDate = (DateTime)reader["DetainDate"];
                        ReleaseDate = (DateTime)reader["ReleaseDate"];
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                        IsRelease = (bool)reader["IsRelease"];
                        FineFees = Convert.ToSingle(reader["FineFees"]);
                        CreatedByUserID = (int)reader["CreatedByUserID"];
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];

                        if (reader["ReleaseDate"] != DBNull.Value)
                        {
                            ReleaseDate = (DateTime)reader["ReleaseDate"];
                        }
                        else
                        {
                            ReleaseDate = DateTime.MaxValue;
                        }

                        if (reader["ReleasedByUserID"] == DBNull.Value)

                            ReleasedByUserID = -1;
                        else
                            ReleasedByUserID = (int)reader["ReleasedByUserID"];

                        if (reader["ReleaseApplicationID"] == DBNull.Value)

                            ReleaseApplicationID = -1;
                        else
                            ReleaseApplicationID = (int)reader["ReleaseApplicationID"];

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

            public static bool GetDetaintedLicenseInfoByLicenseID(ref int DetainID, int LicenseID, ref DateTime DetainDate,
                ref float FineFees, ref int CreatedByUserID, ref bool IsRelease, ref DateTime ReleaseDate
                , ref int ReleasedByUserID, ref int ReleaseApplicationID)
            {
                bool IsFound = false;

                string query = "SELECT top 1 * FROM DetainedLicenses WHERE LicenseID = @LicenseID order by DetainID desc";

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@LicenseID", LicenseID);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        IsFound = true;

                        IsFound = true;
                        DetainID = (int)reader["DetainID"];
                        DetainDate = (DateTime)reader["DetainDate"];

                        IsRelease = (bool)reader["IsReleased"];
                        FineFees = Convert.ToSingle(reader["FineFees"]);
                        CreatedByUserID = (int)reader["CreatedByUserID"];

                        if (reader["ReleaseApplicationID"] != DBNull.Value)
                        {
                            ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                        }
                        else
                        {

                        }
                        if (reader["ReleasedByUserID"] != DBNull.Value)
                        {
                            ReleasedByUserID = (int)reader["ReleasedByUserID"];
                        }
                        else
                        {

                        }

                        if (reader["ReleaseDate"] != DBNull.Value)
                        {
                            ReleaseDate = (DateTime)reader["ReleaseDate"];
                        }
                        else
                        {

                        }
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

            public static bool ReleaseLicense(int DetainID, int LicenseID, DateTime DetainDate,
                float FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate
                , int ReleasedByUserID, int ReleaseApplicationID)
            {
                int rowsAffected = 0;

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

                string query = @" UPDATE DetainedLicenses
                               SET LicenseID = @LicenseID
                                  ,DetainDate = @DetainDate
                                  ,CreatedByUserID = @CreatedByUserID
                                  ,ReleaseDate = @ReleaseDate
                                  ,FineFees = @FineFees
                                  ,IsReleased = @IsReleased
                                  ,ReleasedByUserID = @ReleasedByUserID
                                  ,ReleaseApplicationID = @ReleaseApplicationID
                             WHERE DetainID = @DetainID;";


                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@DetainID", DetainID);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                command.Parameters.AddWithValue("@LicenseID", LicenseID);
                command.Parameters.AddWithValue("@DetainDate", DetainDate);
                command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
                command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
                command.Parameters.AddWithValue("@FineFees", FineFees);
                command.Parameters.AddWithValue("@IsReleased", IsReleased);
                command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);



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

            public static bool DeleteDetainedLicense(int DetainID)
            {
                int rowsAffected = 0;

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

                string query = @"Delete from DetainedLicenses Where DetainID = @DetainID";


                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@DetainID", DetainID);

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
            public static int AddNewDetainedLicense(int LicenseID, DateTime DetainDate,
                float FineFees, int CreatedByUserID, bool IsReleased/*, DateTime ReleaseDate
                , int ReleasedByUserID, int ReleaseApplicationID*/)
            {
                int DetainID = -1;

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

                string query = @"INSERT INTO DetainedLicenses
           (LicenseID
           ,DetainDate
           ,FineFees
           ,CreatedByUserID
           ,IsReleased
           )
     VALUES
           (@LicenseID
           ,@DetainDate
           ,@FineFees
           ,@CreatedByUserID
           ,@IsReleased
           );
                            SELECT SCOPE_IDENTITY();";


                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                command.Parameters.AddWithValue("@LicenseID", LicenseID);
                command.Parameters.AddWithValue("@DetainDate", DetainDate);
                //command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
                //command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
                command.Parameters.AddWithValue("@FineFees", FineFees);
                command.Parameters.AddWithValue("@IsReleased", IsReleased);
                //command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (int.TryParse(result.ToString(), out int insertedID))
                    {
                        DetainID = insertedID;
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }

                return DetainID;
            }

            public static DataTable GetAllDetainedLicenses()
            {
                DataTable dt = new DataTable();

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

                string query = @"SELECT detainedLicenses_View order IsReleased,DetainID;";

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

            public static bool ReleaseDetainedLicense(int DetainID,
                 int ReleasedByUserID, int ReleaseApplicationID)
            {

                int rowsAffected = 0;
                SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

                string query = @"UPDATE dbo.DetainedLicenses
                              SET IsReleased = 1, 
                              ReleaseDate = @ReleaseDate, 
                              ReleaseApplicationID = @ReleaseApplicationID,
                              ReleasedByUserID = @ReleasedByUserID
                              WHERE DetainID=@DetainID;";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@DetainID", DetainID);
                command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
                command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
                command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);
                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Error: " + ex.Message);
                    return false;
                }

                finally
                {
                    connection.Close();
                }

                return (rowsAffected > 0);
            }
            public static bool IsLicenseDetained(int LicenseID)
            {
                bool IsFound = false;
                SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

                string query = @"Select found = 1 from DetainedLicenses Where LicenseID=@LicenseID and IsReleased = 0";


                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@LicenseID", LicenseID);

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
           
        }
    }
    }
