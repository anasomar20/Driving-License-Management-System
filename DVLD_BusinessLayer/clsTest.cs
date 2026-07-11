using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsTest
    {
        private enum enMode { Update = 0, AddNew = 1 }
        private enMode Mode = enMode.AddNew;
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public clsTestAppointment TestAppointmentInfo { set; get; }

        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }
        public bool TestResult { get; set; }

        public clsTest()
        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.Notes = "";
            this.CreatedByUserID = -1;
            this.TestResult = false;

            Mode = enMode.AddNew;
        }
        private clsTest(int TestID, int TestAppointmentID,bool TestResult, string Notes, int CreatedByUserID )
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestAppointmentInfo = clsTestAppointment.Find(TestAppointmentID);
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
            this.TestResult = TestResult;
            Mode = enMode.Update;
        }

        public static clsTest Find(int ID)
        {
            string Notes = "";
            int TestAppointmentID = -1,CreatedByUserID = -1;
            bool TestResult = false;
            if (clsTestsDataAccess.GetTypeInfoByTestID(ID, ref TestAppointmentID,ref TestResult, ref Notes, ref CreatedByUserID))
            {
                return new clsTest(ID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }
        public static clsTest FindLastTestPerPersonAndLicenseClass
            (int PersonID, int LicenseClassID, clsTestType.enTestType TestTypeID)
        {
            int TestID = -1;
            int TestAppointmentID = -1;
            bool TestResult = false; string Notes = ""; int CreatedByUserID = -1;

            if (clsTestsDataAccess.GetLastTestByPersonAndTestTypeAndLicenseClass
                (PersonID, LicenseClassID, (int)TestTypeID, ref TestID,
            ref TestAppointmentID, ref TestResult,
            ref Notes, ref CreatedByUserID))

                return new clsTest(TestID,
                        TestAppointmentID, TestResult,
                        Notes, CreatedByUserID);
            else
                return null;

        }


        private bool _UpdateTest()
        {
            return clsTestsDataAccess.UpdateTest(this.TestID, this.TestAppointmentID, this.TestResult,
                this.Notes, this.CreatedByUserID);
        }

        private bool _AddNewTest()
        {
            this.TestID = clsTestsDataAccess.AddNewTest(this.TestAppointmentID, this.TestResult,
                this.Notes, this.CreatedByUserID);
            return (this.TestID != -1);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateTest();
                default:
                    break;
            }

            return false;
        }      
        public static DataTable GetAllTests()
        {
            return clsTestsDataAccess.GetAllTests();
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestsDataAccess.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            //if total passed test less than 3 it will return false otherwise will return true
            return GetPassedTestCount(LocalDrivingLicenseApplicationID) == 3;
        }
    }
}
