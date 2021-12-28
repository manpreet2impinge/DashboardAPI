using System;
using System.Collections.Generic;

namespace DashboardAPI.Common.Helpers
{
    public static class DateTimeUtility
    {
        public static bool isDateTimeStr(string val)
        {
            DateTime dVal;
            if(DateTime.TryParse(val, out dVal))
            {
                return true;
            }
            return false;
        }

        public static string ToDateTimeStr(DateTime dateVal)
        {
            return dateVal.ToString("yyyy-MM-ddTHH:mm:ss.fff");
        }
        
        public static string GetAMPM(this DateTime dateData, Dictionary<string, string> ampmConfig = null)
        {
            string getAMPM = "";

            // Set Default Confir for AM/PM Rule
            if(ampmConfig == null)
            {
                ampmConfig = new Dictionary<string, string>();
                ampmConfig.Add("AMStart", "00:00:00");
                ampmConfig.Add("AMEnd", "12:59:00");
                ampmConfig.Add("PMStart", "13:00:00");
                ampmConfig.Add("PMEnd", "24:00:00");
            }

            TimeSpan dateString = dateData.TimeOfDay;

            if (dateString <= TimeSpan.Parse(ampmConfig["AMEnd"]))
            {
                getAMPM = "AM";
            }
            else if (dateString >= TimeSpan.Parse(ampmConfig["PMStart"])) 
            {
                getAMPM = "PM";
            }

            return getAMPM;
        }

        public static DateTime FirstDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        public static int DaysInMonth(DateTime value)
        {
            return DateTime.DaysInMonth(value.Year, value.Month);
        }

        public static DateTime LastDayOfMonth(DateTime value)
        {
            return new DateTime(value.Year, value.Month, DaysInMonth(value));
        }

        public static bool CheckFirstDayOfMonth(DateTime value)
        {
            return value.ToString("yyyy-MM-dd") == value.FirstDayOfMonth().ToString("yyyy-MM-dd");
        }

        // Get DateTime replace with Year(safe for LeapYear)
        public static DateTime ChangeYear(this DateTime dt, int newYear)
        {
            return dt.AddYears(newYear - dt.Year);
        }
    }
}