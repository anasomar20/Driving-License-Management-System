using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Project.Global_Classes
{
    public class clsLogger
    {
        private static string sourceName = "DVLDApp";

        public clsLogger()
        {
            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, "Application");
            }
        }

        public static void Log(string message, EventLogEntryType entryType = EventLogEntryType.Error) 
        {

            EventLog.WriteEntry(sourceName, message,entryType);

        }

        private static string FormatErrorMessage(Exception ex)
        {

            // this is an example
            /*
                --- Exception Log ---
                Timestamp: 10/31/2024 2:45:32 PM
                Message: File not found.
                Inner Exception: File path was null.
                Stack Trace: at MyApp.Program.Main() in C:\Project\MyApp\Program.cs:line 24
                Source: MyApp
                -----------------------
             */

            string message =

                 $"--- Exception Log ---\n" +
                 $"Timestamp: {DateTime.Now}\n" +
                 $"Message: {ex.Message}\n" +
                 $"Inner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "N/A")}\n" +
                 $"Stack Trace: {ex.StackTrace}\n" +
                 $"Source: {ex.Source}\n" +
                 $"-----------------------";

            return message;
        }
    }
}
