using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace DVLD_Project.Global_Classes
{
    internal class clsGlobal
    {
        public static clsUser CurrentUser;

        public static bool RemmemberUsernameAndPassword(string Username,string Password)
        {
            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\Data";
            try
            {
                Registry.SetValue(KeyPath, "Username", Username,RegistryValueKind.String);
                Registry.SetValue(KeyPath, "Password", Password,RegistryValueKind.String);
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
            return true;

        }

        public static bool GetStoredCredential(ref string Username,ref string Password)
        {
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\Data";

            try
            {
                Username = Registry.GetValue(keyPath,"Username", null) as string;
                Password = Registry.GetValue(keyPath, "Password", null) as string;

                return true;            
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }
    }
}
