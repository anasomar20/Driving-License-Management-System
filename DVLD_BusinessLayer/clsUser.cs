using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer;

namespace DVLD_BusinessLayer
{
    public class clsUser
    {
        private enum enMode { Update = 0, AddNew = 1 }
        private enMode Mode = enMode.Update;
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo;
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        
        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.Username = "";
            this.Password = "";
            this.IsActive = false;
            
            Mode = enMode.AddNew;
        }
        private clsUser(int UserID,int PersonID, string Username, string Password,
            bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.Username = Username;
            this.Password = Password;
            this.IsActive = IsActive;

            Mode = enMode.Update;
        }

        public static clsUser FindByUserID(int ID)
        {
            string Username = "", Password = "";
            int PersonID = -1;
            bool IsActive = false;
            if (clsUsersDataAccess.GetUserInfoByUserID(ID,ref PersonID, ref Username, ref Password,
                ref IsActive))
            {
                return new clsUser(ID, PersonID,Username,Password,IsActive);
            }
            else
            {
                return null;
            }
        }
        public static clsUser FindByPersonID(int PersonID)
        {
            string Username = "", Password = "";
            int UserID = -1;
            bool IsActive = false;
            if (clsUsersDataAccess.GetUserInfoByPersonID(ref UserID, PersonID, ref Username, ref Password,
                ref IsActive))
            {
                return new clsUser(UserID, PersonID, Username, Password, IsActive);
            }
            else
            {
                return null;
            }
        }
        public static clsUser FindByUsernameAndPassword(string Username,string Password)
        {
            int UserID = -1,PersonID = -1;
            bool IsActive = false;
            if (clsUsersDataAccess.GetUserInfoByUsernameAndPassword(ref UserID,ref PersonID,Username,Password,
                ref IsActive))
            {
                return new clsUser(UserID, PersonID, Username, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        private bool _UpdateUser()
        {
            this.Password = clsDataHelper.ComputeHash(this.Password);
            return clsUsersDataAccess.UpdateUser(this.UserID,this.PersonID,this.Username,
                this.Password,this.IsActive);
        }

        private bool _AddNewUser()
        {
            this.Password = clsDataHelper.ComputeHash(this.Password);
            this.UserID = clsUsersDataAccess.AddNewUser(this.PersonID, this.Username, 
                this.Password,this.IsActive);
            return (this.UserID != -1);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateUser();
                default:
                    break;
            }

            return false;
        }
      
        public static bool DeleteUser(int UserID)
        {
            return clsUsersDataAccess.DeleteUser(UserID);
        }
        public static DataTable GetAllUsers()
        {
            return clsUsersDataAccess.GetAllUsers();
        }
        public static bool IsUserExist(int UserID)
        {
            return clsUsersDataAccess.IsUserExist(UserID);
        }
        public static bool IsUserExistForPersonID(int PersonID)
        {
            return clsUsersDataAccess.IsUserExistForPersonID(PersonID);
        }
        public static bool IsUserExist(string Username)
        {
            return clsUsersDataAccess.IsUserExist(Username);
        }
        
    }
}
