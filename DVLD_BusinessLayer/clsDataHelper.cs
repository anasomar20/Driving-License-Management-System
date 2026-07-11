using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsDataHelper
    {
        static public string ComputeHash(string Password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] Hashbytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Password));
                return BitConverter.ToString(Hashbytes).Replace("-", "").ToLower();
            }
        }

        //static public bool Comparepasswordwithhashcode(ref string Password, ref string HashCode)
        //{
        //    return HashCode == ConvertPasswordttoHashCode(Password);
        //}
    }
}
