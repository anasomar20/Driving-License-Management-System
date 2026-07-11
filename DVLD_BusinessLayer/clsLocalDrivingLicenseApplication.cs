using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsLocalDrivingLicenseApplication :clsApplication
    {
        private enum enMode { Update = 0, AddNew = 1 }
        private enMode Mode = enMode.Update;
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int LicenseClassID { get; set; }
         
        public clsLicenseClass LicenseClassInfo;

        public string PersonFullName
        {
            get
            {
                return base.PersonInfo.FullName();
            }
        }
        public clsLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;            
            Mode = enMode.AddNew;
        }
        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, 
            int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID
			, enApplicationStatus ApplicationStatus, DateTime LastStatusDate, float PaidFees,
            int CreatedByUserID,int LicenseClassID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
			this.ApplicantPersonID = ApplicantPersonID;
			this.ApplicationDate = ApplicationDate;
			this.ApplicationTypeID = ApplicationTypeID;
			this.LastStatusDate = LastStatusDate;
			this.ApplicationStatus = ApplicationStatus;
			this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
			this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);
			Mode = enMode.Update;
        }

        public static clsLocalDrivingLicenseApplication FindByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;

			bool IsFound = clsLocalDrivingLicenseApplicationsDataAccess.GetLocalDrivingLicenseApplicationInfoByID(LocalDrivingLicenseApplicationID,ref ApplicationID, ref LicenseClassID);

			if (IsFound)
			{
				clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

				return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID,
			ApplicationID, Application.ApplicantPersonID, Application.ApplicationDate, Application.ApplicationTypeID
			, Application.ApplicationStatus, Application.LastStatusDate, Application.PaidFees,
			Application.CreatedByUserID, LicenseClassID);
			}
			else
            {
                return null;
            }
        }
        public static clsLocalDrivingLicenseApplication FindbyApplication(int ApplicationID)
        {
            int LocalDrivingLicenseApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationsDataAccess.GetLocalDrivingLicenseApplicationInfoByIDByApplicationID(ref LocalDrivingLicenseApplicationID, ApplicationID, ref LicenseClassID);

			if (IsFound)
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID,
			ApplicationID,Application.ApplicantPersonID, Application.ApplicationDate, Application.ApplicationTypeID
			, Application.ApplicationStatus, Application.LastStatusDate,Application.PaidFees,
			Application.CreatedByUserID,LicenseClassID);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationsDataAccess.AddNewDrivingLicenseApplication(this.ApplicationID,this.LicenseClassID);
            return (this.LocalDrivingLicenseApplicationID != -1);
        }

		private bool _UpdateLocalDrivingLicenseApplication()
		{
            return clsLocalDrivingLicenseApplicationsDataAccess.UpdateLocalDrivingLicenseApplication
                (this.LocalDrivingLicenseApplicationID,this.ApplicationID, this.LicenseClassID);		
		}
		public bool Save()
        {
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
                return false;

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
					return _UpdateLocalDrivingLicenseApplication();
			}

            return true;
        }

        public static bool IsPersonHasApplicationInSameLicense(int PersonID, string LicenseClass)
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.IsPersonHasApplicationInSameLicense(PersonID, LicenseClass);
        }
		public bool Delete()
		{
			bool IsLocalDrivingApplicationDeleted = false;
			bool IsBaseApplicationDeleted = false;
			//First we delete the Local Driving License Application
			IsLocalDrivingApplicationDeleted = clsLocalDrivingLicenseApplicationsDataAccess.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID);

			if (!IsLocalDrivingApplicationDeleted)
				return false;
			//Then we delete the base Application
			IsBaseApplicationDeleted = base.Delete();
			return IsBaseApplicationDeleted;

		}
		public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            DataTable dt = clsLocalDrivingLicenseApplicationsDataAccess.GetAllLocalDrivingLicenseApplications();
            return dt;
        }
       
        public bool DoesPassTestType(clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestType);
        }

        public bool DoesAttendTestType(clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (int)TestType);
        }
        public static bool DoesAttendTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.DoesAttendTestType(LocalDrivingLicenseApplicationID, (int)TestType);
        }
        public byte TotalTrialsPerTest(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool IsThereAnActiveScheduledTest(clsTestType.enTestType TestTypeID)
        {

            return clsLocalDrivingLicenseApplicationsDataAccess.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public clsTest GetLastTestPerTestType(clsTestType.enTestType TestTypeID)
        {
            return clsTest.FindLastTestPerPersonAndLicenseClass(this.ApplicantPersonID, this.LicenseClassID, TestTypeID);
        }

        public byte GetPassedTestCount()
        {
            return clsTest.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public bool PassedAllTests()
        {
            return clsTest.PassedAllTests(this.LocalDrivingLicenseApplicationID);
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            //if total passed test less than 3 it will return false otherwise will return true
            return clsTest.PassedAllTests(LocalDrivingLicenseApplicationID);
        }

        public int IssueLicenseForTheFirstTime(string Notes,int CreatedByUserID)
        {
            int DriverID = -1;

            clsDriver Driver = clsDriver.FindByPersonID(this.ApplicantPersonID);

            if (Driver == null)
            {
                Driver = new clsDriver();

                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedByUserID = CreatedByUserID;
                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID = Driver.DriverID;
            }

            clsLicense License = new clsLicense();
            License.ApplicationID = this.ApplicationID;
            License.DriverID = DriverID;
            License.LicenseClass = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLenght);
            License.Notes = Notes;
            License.PaidFees = this.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if (License.Save())
            {
                this.SetComplete();
                return License.LicenseID;
            }
            else
                return -1;
        }

        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID() != -1);
        }

        public int GetActiveLicenseID()
        {
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }
    }
}
