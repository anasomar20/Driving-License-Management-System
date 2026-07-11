using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsTestAppointment
    {
        private enum enMode { Update = 0, AddNew = 1 }
        private enMode Mode = enMode.AddNew;
        public int TestAppointmentID { get; set; }
        public clsTestType.enTestType TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }
        public clsApplication RetakeTestAppInfo{ get; set; }

        public int TestID
        {
            get
            {
                return _GetTestID();
            }
        }
        public clsTestAppointment()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = clsTestType.enTestType.VisionTest;
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.IsLocked = false;
            this.RetakeTestApplicationID = -1;
            Mode = enMode.AddNew;
        }
        private clsTestAppointment(int TestAppointmentID, clsTestType.enTestType TestTypeID, int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked,int RetakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            this.RetakeTestAppInfo = clsApplication.FindBaseApplication(RetakeTestApplicationID);
            Mode = enMode.Update;
        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int TestTypeID = -1, LocalDrivingLicenseApplicationID = -1, CreatedByUserID = -1, RetakeTestApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            float PaidFees = 0;
            bool IsLocked = false;
            if (clsTestAppointmentsDataAccess.GetTypeInfoByTestAppointmentID(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID,
            ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked,ref RetakeTestApplicationID))
            {
                return new clsTestAppointment(TestAppointmentID,(clsTestType.enTestType)TestTypeID, LocalDrivingLicenseApplicationID,
                     AppointmentDate, PaidFees, CreatedByUserID, IsLocked,RetakeTestApplicationID);
            }
            else
            {
                return null;
            }
        }

        public static clsTestAppointment GetLastTestAppointment(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            int TestAppointmentID = -1,CreatedByUserID = -1, RetakeTestApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            float PaidFees = 0;
            bool IsLocked = false;
            if (clsTestAppointmentsDataAccess.GetLastTestAppointment(LocalDrivingLicenseApplicationID,(int)TestTypeID
            ,ref TestAppointmentID,ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))
            {
                return new clsTestAppointment(TestAppointmentID, (clsTestType.enTestType)TestTypeID, LocalDrivingLicenseApplicationID,
                     AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            }
            else
            {
                return null;
            }
        }

        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentsDataAccess.UpdateAppointmentTest(this.TestAppointmentID, (int)this.TestTypeID, this.LocalDrivingLicenseApplicationID,
                     this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked,this.RetakeTestApplicationID);
        }

        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentsDataAccess.AddNewAppointmentTest((int)this.TestTypeID, this.LocalDrivingLicenseApplicationID,
                     this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked,this.RetakeTestApplicationID);
            return (this.TestAppointmentID != -1);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateTestAppointment();
                default:
                    break;
            }

            return false;
        }

        public static bool DeleteTestAppointment(int TestAppointmentID)
        {
            return clsTestAppointmentsDataAccess.DeleteAppointmentTest(TestAppointmentID);
        }
        public static DataTable GetAllTestAppointments()
        {
            return clsTestAppointmentsDataAccess.GetAllTestAppointments();
        }
        public DataTable GetApplicationTestAppointmentsPerTestType(clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentsDataAccess.GetApplicationTestAppointmentPerTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);

        }
        public static DataTable GetApplicationTestAppointmentPerTestType(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            return clsTestAppointmentsDataAccess.GetApplicationTestAppointmentPerTestType(LocalDrivingLicenseApplicationID,TestTypeID);
        }
        public static bool IsTestAppointmentExist(int TestAppointmentID)
        {
            return clsTestAppointmentsDataAccess.IsTestExistByTestAppointmentID(TestAppointmentID);
        }
        public static bool IsPersonHasTestAppointmentExistAndActive(int TestAppointmentID,int TestTypeID)
        {
            return clsTestAppointmentsDataAccess.IsPersonHasTestAppointmentExistAndActive(TestAppointmentID,TestTypeID);
        }
        public static int CountNumberOfTrialsForPerson(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestAppointmentsDataAccess.CountNumberOfTrialsForPerson(LocalDrivingLicenseApplicationID, TestTypeID);
        }
        private int _GetTestID()
        {
            return clsTestAppointmentsDataAccess.GetTestID(TestAppointmentID);
        }

    }
}
