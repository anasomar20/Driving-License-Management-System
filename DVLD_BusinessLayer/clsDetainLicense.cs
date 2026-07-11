using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_DataAccessLayer.DetaintedDetainedLicensesDataAccess;

namespace DVLD_BusinessLayer
{
    public class clsDetainLicense
    {
        private enum enMode { Update = 0, AddNew = 1 }
        private enMode Mode = enMode.Update;
        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public float FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo { get; set; }
        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public clsUser ReleasedByUserInfo { get; set; }
        public int ReleaseApplicationID { get; set; }

        public clsDetainLicense()
        {
            this.DetainID = -1;
            this.LicenseID = -1;
            this.ReleaseApplicationID = -1;
            this.ReleasedByUserID = -1;
            this.DetainDate = DateTime.Now;
            this.ReleaseDate = DateTime.Now;
            this.FineFees = 0;
            this.IsReleased = false;
            this.CreatedByUserID = -1;


            Mode = enMode.AddNew;
        }
        private clsDetainLicense(int DetainID, int LicenseID, DateTime DetainDate,
                float FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate
                , int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.ReleaseApplicationID = ReleaseApplicationID;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleasedByUserInfo = ReleasedByUserInfo;
            this.DetainDate = DetainDate;
            this.ReleaseDate = DetainDate;
            this.FineFees = FineFees;
            this.IsReleased = IsReleased;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = CreatedByUserInfo;
            Mode = enMode.Update;
        }

        public static clsDetainLicense FindByDetainID(int DetainID)
        {
            int LicenseID = -1, ReleaseApplicationID = -1, ReleasedByUserID = -1, CreatedByUserID=-1 ;
            DateTime DetainDate = DateTime.Now, ReleaseDate = DateTime.Now;
            float FineFees = 0;
            bool IsRelease = false;

            if (clsDetainedLicensesDataAccess.GetDetainedLicenseInfoByID(DetainID,ref LicenseID, ref DetainDate,
                ref FineFees, ref CreatedByUserID, ref IsRelease, ref ReleaseDate
                , ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clsDetainLicense(DetainID,LicenseID,DetainDate,
                FineFees,CreatedByUserID,IsRelease,ReleaseDate
                ,ReleasedByUserID,ReleaseApplicationID);
            }
            else
            {
                return null;
            }
        }
        public static clsDetainLicense FindByLicenseID(int LicenseID)
        {
            int DetainID = -1, ReleaseApplicationID = -1, ReleasedByUserID = -1, CreatedByUserID = -1;
            DateTime DetainDate = DateTime.Now, ReleaseDate = DateTime.Now;
            float FineFees = 0;
            bool IsRelease = false;

            if (clsDetainedLicensesDataAccess.GetDetaintedLicenseInfoByLicenseID(ref DetainID,LicenseID, ref DetainDate,
                ref FineFees, ref CreatedByUserID, ref IsRelease, ref ReleaseDate
                , ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clsDetainLicense(DetainID, LicenseID, DetainDate,
                FineFees, CreatedByUserID, IsRelease, ReleaseDate
                , ReleasedByUserID, ReleaseApplicationID);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewDetainLicense()
        {
            this.DetainID = clsDetainedLicensesDataAccess.AddNewDetainedLicense(LicenseID, DetainDate,
                FineFees, CreatedByUserID, IsReleased);
            return (this.DetainID != -1);
        }
        private bool _UpdateDetainLicense()
        {
            return clsDetainedLicensesDataAccess.ReleaseLicense(DetainID,LicenseID, DetainDate,
                FineFees, CreatedByUserID, IsReleased,ReleaseDate,ReleasedByUserID,ReleaseApplicationID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateDetainLicense();

            }

            return false;
        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicensesDataAccess.IsLicenseDetained(LicenseID);
        }
        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicensesDataAccess.GetAllDetainedLicenses();
        }
        public bool ReleaseDetainedLicense(int ReleasedByUserID, int ReleaseApplicationID)
        {
            return clsDetainedLicensesDataAccess.ReleaseDetainedLicense(this.DetainID,
                   ReleasedByUserID, ReleaseApplicationID);
        }
    }
}
