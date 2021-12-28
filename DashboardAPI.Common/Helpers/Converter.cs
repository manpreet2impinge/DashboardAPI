using System;
using System.Reflection;

namespace DashboardAPI.Common.Helpers
{
    public static class Converter
    {
        public static object nullToEmptyStr(object obj)
        {
            // For specific field
            if (obj == null)
            {
                return string.Empty;
            }
            
            // For a object
            foreach (var prop in obj.GetType().GetProperties())
            {
                // Set empty string if value true or Set null for custom object type
                var t = prop.PropertyType;
                if ((t.IsPrimitive || t == typeof(Decimal) || t == typeof(String) || t == typeof(DateTime)) && prop.GetValue(obj) == null)
                {
                    prop.SetValue(obj, "");
                }
            }

            return obj;
        }
    }
}