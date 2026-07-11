using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer;

namespace DVLD_BusinessLayer
{
    public class clsLicenseClass
    {
        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string Description { get; set; }
        public byte MinimumAllowedAge { get; set; }
        public byte DefaultValidityLenght { get; set; }
        public float ClassFees { get; set; }
        public clsLicenseClass()
        {
            LicenseClassID = -1;
            ClassName = string.Empty;
            Description = string.Empty;
            MinimumAllowedAge = 0;
            DefaultValidityLenght = 0;
            ClassFees = 0;
        }
        private clsLicenseClass(int LicenseClassID, string className,string Description,byte MiniumumAllowedAge 
            ,byte DefaultValidityLenght, float ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = className;
            this.Description = Description;
            this.MinimumAllowedAge = MiniumumAllowedAge;
            this.DefaultValidityLenght = DefaultValidityLenght;
            this.ClassFees = ClassFees;
        }
        public static clsLicenseClass Find(int LicenseClassID)
        {
            string ClassName = "",Description = "";
            byte MinimumAllowedAge =  0, DefaultValidityLenght = 0;
            float ClassFees = 0;
            if (clsLicenseClassesDataAccess.GetLicenseClassInfoByID(LicenseClassID, ref ClassName, ref Description
                ,ref MinimumAllowedAge,ref DefaultValidityLenght,ref ClassFees))
            {
                return new clsLicenseClass(LicenseClassID, ClassName, Description
                , MinimumAllowedAge,DefaultValidityLenght, ClassFees);
            }
            else
            {
                return null;
            }
        }
        public static clsLicenseClass Find(string ClassName)
        {
            int LicenseClassID = -1;
            string Description = "";
            byte MinimumAllowedAge = 0, DefaultValidityLenght = 0;
            float ClassFees = 0;
            if (clsLicenseClassesDataAccess.GetLicenseClassInfoByClassName(ClassName,ref LicenseClassID, ref Description
                , ref MinimumAllowedAge, ref DefaultValidityLenght, ref ClassFees))
            {
                return new clsLicenseClass(LicenseClassID, ClassName, Description
                , MinimumAllowedAge, DefaultValidityLenght, ClassFees);
            }
            else
            {
                return null;
            }
        }
        public bool UpdateApplictionType()
        {
            return clsLicenseClassesDataAccess.UpdateLicenseClass(this.LicenseClassID, this.ClassName, this.Description
                , this.MinimumAllowedAge, this.DefaultValidityLenght, this.ClassFees);
        }
        public static DataTable GetAllLicenseClass()
        {
            return clsLicenseClassesDataAccess.GetAllLicenseClasses();
        }
    }
}
