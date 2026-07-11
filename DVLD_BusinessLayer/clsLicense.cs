using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsLicense
    {
        private enum enMode { Update = 0, AddNew = 1 }
        private enMode Mode = enMode.Update;
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }

        public clsDriver DriverInfo;
        public int LicenseClass { get; set; }
        public clsLicenseClass LicenseClassInfo;    
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees { get; set; }
        public bool IsActive { get; set; }
        public enum enIssueReason { FirstTime = 1,Renew = 2 ,DamagedReplacement = 3,LostReplacement = 4};
        public int CreatedByUserID { get; set; }
        public enIssueReason IssueReason { get; set; }

        public clsDetainLicense DetainedInfo { get; set; }
        public bool IsDetained
        {
            get
            {
                return clsDetainLicense.IsLicenseDetained(this.LicenseID);
            }
        }
        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonTest(IssueReason);
            }
        }
        public clsLicense()
        {
            this.LicenseID = -1;
            this.LicenseClass = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IssueReason = enIssueReason.FirstTime;
            this.PaidFees = 0;
            this.IsActive = true;
            this.Notes = "";
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }
        private clsLicense(int LicenseID, int ApplicationID, int DriverID,
            int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees
           , bool IsActive, enIssueReason IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.LicenseClass = LicenseClass;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IssueReason = IssueReason;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;

            this.DriverInfo = clsDriver.FindByDriverID(this.DriverID);
            this.LicenseClassInfo = clsLicenseClass.Find(this.LicenseClass);
            this.DetainedInfo = clsDetainLicense.FindByLicenseID(this.LicenseID);
                
            Mode = enMode.Update;
        }

        public static clsLicense Find(int ID)
        {
            string Notes = "";
            int ApplicationID = -1, DriverID = -1, LicenseClass = -1, CreatedByUserID = -1;
            byte IssueReason = 1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            bool IsActive = false;
            float PaidFees = 0;

            if (clsLicenseDataAccess.GetLicenseInfoByID(ID, ref ApplicationID, ref DriverID, ref LicenseClass,
                ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new clsLicense(ID, ApplicationID, DriverID, LicenseClass, IssueDate,
                    ExpirationDate, Notes, PaidFees, IsActive,(enIssueReason) IssueReason, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

   
        private bool _UpdateLicense()
        {
            return clsLicenseDataAccess.UpdateLicense(this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate,
                    this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive,(byte) this.IssueReason, this.CreatedByUserID);
        }

        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicenseDataAccess.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate,
                    this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive,(byte) this.IssueReason, this.CreatedByUserID);
            return (this.LicenseID != -1);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateLicense();
                default:
                    break;
            }

            return false;
        }


        
        public static bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return (GetActiveLicenseIDByPersonID(PersonID, LicenseClassID) != -1);
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {

            return clsLicenseDataAccess.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);

        }
        
        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicenseDataAccess.GetDriverLicenses(DriverID);
        }

        public Boolean IsLicenseExpired()
        {
            return (this.ExpirationDate < DateTime.Now);
        }

        public bool DeactivateCurrentLicense()
        {
            return (clsLicenseDataAccess.DeactivateLicense(this.LicenseID));
        }
        public static string GetIssueReasonTest(enIssueReason IssueReason)
        {
            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Replacement For Damaged";
                case enIssueReason.LostReplacement:
                    return "Replacement For Lost";
                default:
                    return "First Time";
            }
        }

        public int Detain(float FineFees,int CreatedByUserID)
        {
            clsDetainLicense detainLicense = new clsDetainLicense();
            detainLicense.LicenseID = this.LicenseID;
            detainLicense.DetainDate = DateTime.Now;
            detainLicense.FineFees = Convert.ToSingle(FineFees);
            detainLicense.CreatedByUserID = CreatedByUserID;

            if (!detainLicense.Save())
            {
                return -1;
            }
            return detainLicense.DetainID;
        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID,ref int ApplicationID)
        {
            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicense;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicense).Fees;
            Application.CreatedByUserID = ReleasedByUserID;

            if (!Application.Save())
            {
                ApplicationID = -1;
                return false;
            }

            ApplicationID = Application.ApplicationID;

            return true;
        }

        public clsLicense RenewLicense(string Notes,int CreatedByUserID)
        {
            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).Fees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;

            int DefaultValidityLenght = this.LicenseClassInfo.DefaultValidityLenght;

            NewLicense.ExpirationDate = DateTime.Now.AddYears(DefaultValidityLenght);
            NewLicense.Notes = Notes;
            NewLicense.PaidFees = this.LicenseClassInfo.ClassFees;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = clsLicense.enIssueReason.Renew;
            NewLicense.CreatedByUserID = CreatedByUserID;

            if (!NewLicense.Save())
            {
                return null;
            }

            DeactivateCurrentLicense();
            return NewLicense;
        }

        public clsLicense Replace(enIssueReason IssueReason , int CreatedByUserID)
        {
            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;

            Application.ApplicationTypeID = (IssueReason == enIssueReason.DamagedReplacement) ?
                (int)clsApplication.enApplicationType.ReplaceDamagedDrivngLicense :
                (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;

            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationTypes.Find(Application.ApplicationTypeID).Fees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = this.ExpirationDate;
            NewLicense.Notes = this.Notes;
            NewLicense.PaidFees = 0;// no fees for the license because it's a replacement.
            NewLicense.IsActive = true;
            NewLicense.IssueReason = IssueReason;
            NewLicense.CreatedByUserID = CreatedByUserID;



            if (!NewLicense.Save())
            {
                return null;
            }

            //we need to deactivate the old License.
            DeactivateCurrentLicense();

            return NewLicense;
        }
    }
}
