using System;
using System.Data;

using DVLD_DataAccessLayer;
namespace DVLD_BusinessLayer
{
    public class clsPerson
    {
        private enum enMode { Update = 0, AddNew = 1}
        private enMode Mode = enMode.Update;
        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName()
        {
            return FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
        }
        public short Gendor { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int NationalityCountryID { get; set; }
        public clsCountry CountryInfo;
        private string _ImagePath;
        public string ImagePath
        { 
            get { return _ImagePath; }
            set { _ImagePath = value; }     
        }

        public clsPerson()
        {
            this.PersonID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Gendor = 5;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";
            Mode = enMode.AddNew;
        }
        private clsPerson(int PersonID, string NationalNo, string FirstName,
            string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, short Gendor,
            string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.CountryInfo = clsCountry.Find(NationalityCountryID);
            this.ImagePath = ImagePath;
            Mode = enMode.Update;
        }

        public static clsPerson Find(int ID)
        {
            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            int  NationalityCountryID = -1;
            short Gendor = 5;
            DateTime DateOfBirth = DateTime.Now;

            if (clsPeopleDataAccess.GetPersonInfoByID(ID, ref NationalNo, ref FirstName,
                ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor,
                ref Address, ref  Phone, ref Email, ref NationalityCountryID, ref ImagePath))
            {
                return new clsPerson(ID, NationalNo, FirstName,SecondName, ThirdName, LastName, 
                    DateOfBirth, Gendor,Address, Phone, Email, NationalityCountryID, ImagePath);
            }
            else
            {
                return null;
            }
        }

        public static clsPerson Find(string NationalNo)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            int PersonID = -1 , NationalityCountryID = -1;
            short Gendor = 5;
            DateTime DateOfBirth = DateTime.Now;

            if (clsPeopleDataAccess.GetPersonInfoByNationalNo(ref PersonID, NationalNo, ref FirstName,
                ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor,
                ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                    DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            }
            else
            {
                return null;
            }
        }

        private bool _UpdatePerson()
        {
           return clsPeopleDataAccess.UpdatePerson(this.PersonID, this.NationalNo,
                this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor,
                this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
        }

        private bool _AddNewPerson()
        {
           this.PersonID = clsPeopleDataAccess.AddNewPerson(this.NationalNo,
                this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor,
                this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
            return (this.PersonID != -1);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                   
                case enMode.Update:
                    return _UpdatePerson();                   
                default:
                    break;
            }

            return false;
        }
        public static bool DeletePerson(int PersonID)
        {
            return clsPeopleDataAccess.DeletePerson(PersonID);
        }
        public static DataTable GetAllPeople()
        {
            return clsPeopleDataAccess.GetAllPeople();
        }
        public static bool IsPersonExist(int PersonID)
        {
            return clsPeopleDataAccess.IsPersonExist(PersonID);
        }
        public static bool IsPersonExist(string NationalNo)
        {
            return clsPeopleDataAccess.IsPersonExist(NationalNo);
        }
      
    }
}
