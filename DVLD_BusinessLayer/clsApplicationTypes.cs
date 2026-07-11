using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer;

namespace DVLD_BusinessLayer
{
    public class clsApplicationTypes
    {
        public enum enMode { AddNew = 0 ,Update = 1}
        public enMode Mode = enMode.Update;
        public int ApplicationTypeID { get; set; }
        public string Title{ get; set; }
        public float Fees { get; set; }

        public clsApplicationTypes()
        {
            ApplicationTypeID = -1;
            Title = string.Empty;
            Fees = 0;
            Mode = enMode.AddNew;
        } 
        private clsApplicationTypes(int ApplicationTypeID, string Title,float Fees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.Title = Title;
            this.Fees = Fees;
            Mode = enMode.Update;
        }
        public static clsApplicationTypes Find(int ApplicationTypeID)
        {
            string title = "";
            float Fees = 0;
            if (clsApplicationTypesDataAccess.GetTypeInfoByApplicationID(ApplicationTypeID, ref title, ref Fees))
            {
                return new clsApplicationTypes(ApplicationTypeID, title, Fees);
            }
            else
            {
                return null;
            }
        }
     
        private bool _UpdateApplictionType()
        {
            return clsApplicationTypesDataAccess.UpdateApplicationType(this.ApplicationTypeID, this.Title
                , this.Fees);
        }
        private bool _AddNewApplictionType()
        {
           this.ApplicationTypeID = clsApplicationTypesDataAccess.AddNewApplicationType(this.Title
                , this.Fees);
            return (this.ApplicationTypeID != -1);
        }
        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypesDataAccess.GetAllApplicationTypes();
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewApplictionType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateApplictionType();
                
            }
            return true;
        }
    }
}
