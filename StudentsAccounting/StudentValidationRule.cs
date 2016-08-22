using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StudentsAccounting
{
    public class StudentValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            // TODO: More strict name validation needed
            string result = value as String;

            if (String.IsNullOrWhiteSpace(result))
            {
                return new ValidationResult(false, "Empty or null string");
            }
            // TODO: Some smarter validation
            else if (result.Contains("/"))
            {
                string[] ymd = result.Split('/');
                int tryInt;

                if (ymd.Length != 3 || !Int32.TryParse(ymd[2], out tryInt) || !Int32.TryParse(ymd[1], out tryInt) || !Int32.TryParse(ymd[0], out tryInt))
                {
                    return new ValidationResult(false, null);
                }

                if (new DateTime(Convert.ToInt32(ymd[2]), Convert.ToInt32(ymd[0]), Convert.ToInt32(ymd[1])).CompareTo(new DateTime(1900, 1, 1)) < 0)
                {
                    return new ValidationResult(false, null);
                }
            }
            return new ValidationResult(true, null);
        }
    }
}
