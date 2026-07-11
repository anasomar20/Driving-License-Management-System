using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer;

namespace DVLD_BusinessLayer
{
    public class clsApplication
    {
       
        public enum enMode { AddNew = 0 ,Update = 1}
        public enMode Mode = enMode.AddNew;
        public enum enApplicationType { NewDrivingLicense = 1,RenewDrivingLicense = 1 ,
        ReplaceLostDrivingLicense = 3,ReplaceDamagedDrivngLicense = 4,ReleaseDetainedDrivingLicense = 5
                ,NewInternationalLicense= 6,RetakeTest=7};
        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };

        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public clsPerson PersonInfo { get; set; }
        public string ApplicantFullName
        {
            get
            {
                return clsPerson.Find(ApplicantPersonID).FullName();
            }
        }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public clsApplicationTypes ApplicationTypeInfo { get; set; }
        public enApplicationStatus ApplicationStatus { get; set; }
        public string StatusTest
        {
            get 
            {
                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "UnKnown";
                }
            }
        }
        public DateTime LastStatusDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo { set; get; }

        public clsApplication()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationTypeID = -1;
            this.ApplicationDate = DateTime.Now;
            this.LastStatusDate = DateTime.Now;
            this.ApplicationStatus = enApplicationStatus.New;
            this.PaidFees = -1;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }
        private clsApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate,int ApplicationTypeID
            ,enApplicationStatus ApplicationStatus, DateTime LastStatusDate, float PaidFees,int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.PersonInfo = clsPerson.Find(ApplicantPersonID);
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeInfo = clsApplicationTypes.Find(ApplicationTypeID); 
            this.LastStatusDate = LastStatusDate;
            this.ApplicationStatus = ApplicationStatus;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            Mode = enMode.Update;
        }

        public static clsApplication FindBaseApplication(int ID)
        {
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            int ApplicantPersonID = -1,CreatedByUserID = -1 , ApplicationTypeID = -1;
            byte ApplicationStatusValue = 1;
            float PaidFees = -1;
            if (clsApplicationsDataAccess.GetApplicationInfoByApplicationID(ID, ref  ApplicantPersonID, ref  ApplicationDate
            ,ref ApplicationTypeID ,ref ApplicationStatusValue, ref LastStatusDate, ref  PaidFees, ref  CreatedByUserID))
            {
                return new clsApplication(ID, ApplicantPersonID,  ApplicationDate
            ,ApplicationTypeID, (enApplicationStatus)ApplicationStatusValue, LastStatusDate,  PaidFees,  CreatedByUserID);
            }
            else
            {
                return null;
            }
        }
        
        private bool _UpdateApplication()
        {
            return clsApplicationsDataAccess.UpdateApplication(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate,
                this.ApplicationTypeID,((byte)this.ApplicationStatus),this.LastStatusDate,this.PaidFees,this.CreatedByUserID);
        }
        private bool _UpdateApplicationStatus()
        {
            return clsApplicationsDataAccess.UpdateStatus(this.ApplicationID, ((byte)this.ApplicationStatus));
        }
        public bool Cancle()
        {
            return clsApplicationsDataAccess.UpdateStatus(this.ApplicationID,2);
        }
        public bool SetComplete()
        {
            return clsApplicationsDataAccess.UpdateStatus(this.ApplicationID,3);
        }
        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationsDataAccess.AddNewApplication(this.ApplicantPersonID, this.ApplicationDate,
                this.ApplicationTypeID, ((byte)this.ApplicationStatus), this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
            return (this.ApplicationID != -1);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateApplication();
            }

            return false;
        }

        public  bool Delete()
        {
            return clsApplicationsDataAccess.DeleteApplication(this.ApplicationID);
        }
        public static DataTable GetAllApplications()
        {
            return clsApplicationsDataAccess.GetAllApplications();
        }
        public static bool IsApplicationExist(int ApplicationID)
        {
            return clsApplicationsDataAccess.IsApplicationExist(ApplicationID);
        }
        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return clsApplicationsDataAccess.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }

        public bool DoesPersonHaveActiveApplication(int ApplicationTypeID)
        {
            return DoesPersonHaveActiveApplication(this.ApplicantPersonID, ApplicationTypeID);
        }

        public static int GetActiveApplicationID(int PersonID, clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationsDataAccess.GetActiveApplicationID(PersonID, (int)ApplicationTypeID);
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, clsApplication.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationsDataAccess.GetActiveApplicationIDForLicenseClass(PersonID, (int)ApplicationTypeID, LicenseClassID);
        }

        public int GetActiveApplicationID(clsApplication.enApplicationType ApplicationTypeID)
        {
            return GetActiveApplicationID(this.ApplicantPersonID, ApplicationTypeID);
        }


    }
}
