using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsTestType
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.Update;
        public enum enTestType { VisionTest = 1 ,WrittenTest = 2,StreetTest = 3};
        public clsTestType.enTestType ID { get; set;}
        public string Title { get; set; }
        public string Description { get; set; }

        public float Fees { get; set; }

        public clsTestType()
        {
            this.ID =  clsTestType.enTestType.VisionTest;
            Title = string.Empty;
            Description = string.Empty;
            Fees = 0;
            Mode = enMode.AddNew;
        }
        private clsTestType(clsTestType.enTestType ID, string Title,string Description, float Fees)
        {
            this.ID = ID;
            this.Title = Title;
            this.Description = Description;
            this.Fees = Fees;
            Mode = enMode.Update;
        }
        public static clsTestType Find(clsTestType.enTestType ID)
        {
            string title = "",Description = "";
            float Fees = 0;
            if (clsTestTypesDataAccess.GetTypeInfoByTestTypeID((int)ID, ref title, ref Description,ref Fees))
            {
                return new clsTestType(ID, title,Description, Fees);
            }
            else
            {
                return null;
            }
        }
        private bool _UpdateTestType()
        {
            return clsTestTypesDataAccess.UpdateTestType((int)this.ID, this.Title,this.Description
                , this.Fees);
        }
        private bool _AddNewTestType()
        {
            this.ID =(clsTestType.enTestType) clsTestTypesDataAccess.AddNewTestType(this.Title, this.Description
                , this.Fees);
            return (this.Title != "");
        }
        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesDataAccess.GetAllTestTypes();
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateTestType();
                
            }
            return true;
        }
    }
}
