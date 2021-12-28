using System.Linq;
using System.Text.RegularExpressions;

namespace DashboardAPI.Common.Helpers
{
    public static class JustValidation
    {
        public static bool isValidEmail(string email)
        {
            Regex regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            Match match = regex.Match(email);

            return match.Success;
        }

        public static bool isContainSpace(string val)
        {
            return val.Any(char.IsWhiteSpace);
        }
    }
}