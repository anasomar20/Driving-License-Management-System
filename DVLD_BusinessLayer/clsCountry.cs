using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsCountry
    {

        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public clsCountry()
        {
            CountryID = -1;
            CountryName = "";
        }
        private clsCountry(int countryID, string countryName)
        {
            CountryID = countryID;
            CountryName = countryName;
        }
        public static clsCountry Find(int countryID)
        {
            string CountryName = "";

            if (clsCountriesDataAccess.GetCountryInfoByID(countryID, ref CountryName))
            {
                return new clsCountry(countryID,CountryName);
            }
            else
            {
                return null;
            }

        }
        public static clsCountry Find(string CountryName)
        {
            int CountryID = -1;
            if (clsCountriesDataAccess.GetCountryInfoByName(ref CountryID, CountryName))
            {
                return new clsCountry(CountryID,CountryName);
            }
            else
            {
                return null;
            }

        }
        public static DataTable GetAllCountries()
        {
            return clsCountriesDataAccess.GetAllCountries();
        }
       
    }
}
