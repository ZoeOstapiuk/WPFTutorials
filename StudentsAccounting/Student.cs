using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsAccounting
{
    [Serializable]
    public class Student : ICloneable
    {
        private string firstName;
        private string lastName;
        private DateTime birthDate;
        private string[] grades;

        public Student(DateTime bDay, string[] strGrades, string fName = "First Name", string lName = "Last Name")
        {
            if (strGrades == null)
            {
                throw new ArgumentNullException(nameof(grades));
            }

            FirstName = fName;
            LastName = lName;

            BirthDate = bDay;

            this.grades = new string[strGrades.Length];
            Exam1 = strGrades[0];
            Exam2 = strGrades[1];
            Exam3 = strGrades[2];
            Exam4 = strGrades[3];
            Exam5 = strGrades[4];
            Exam6 = strGrades[5];
        }

        public Student()
        {
            this.grades = new string[6]
            {
                "N/A", "N/A", "N/A", "N/A", "N/A", "N/A"
            };
            this.FirstName = "First Name";
            this.LastName = "Last Name";
            this.BirthDate = new DateTime(1900, 1, 1);
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentOutOfRangeException();
                }
                firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentOutOfRangeException();
                }
                lastName = value;
            }
        }

        public DateTime BirthDate
        {
            get
            {
                return birthDate;
            }
            set
            {
                if (value.CompareTo(new DateTime(1900, 1, 1)) < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                birthDate = value;
            }
        }

        public string Exam1
        {
            get
            {
                return grades[0];
            }
            set
            {
                if (!IsGradeCorrect(value))
                {
                    throw new ArgumentOutOfRangeException();
                }
                grades[0] = value;
            }
        }

        public string Exam2
        {
            get
            {
                return grades[1];
            }
            set
            {
                if (!IsGradeCorrect(value))
                {
                    throw new ArgumentOutOfRangeException();
                }
                grades[1] = value;
            }
        }

        public string Exam3
        {
            get
            {
                return grades[2];
            }
            set
            {
                if (!IsGradeCorrect(value))
                {
                    throw new ArgumentOutOfRangeException();
                }
                grades[2] = value;
            }
        }

        public string Exam4
        {
            get
            {
                return grades[3];
            }
            set
            {
                if (!IsGradeCorrect(value))
                {
                    throw new ArgumentOutOfRangeException();
                }
                grades[3] = value;
            }
        }

        public string Exam5
        {
            get
            {
                return grades[4];
            }
            set
            {
                if (!IsGradeCorrect(value))
                {
                    throw new ArgumentOutOfRangeException();
                }
                grades[4] = value;
            }
        }

        public string Exam6
        {
            get
            {
                return grades[5];
            }
            set
            {
                if (!IsGradeCorrect(value))
                {
                    throw new ArgumentOutOfRangeException();
                }
                grades[5] = value;
            }
        }

        public string RatingGrade
        {
            get
            {
                int tryInt;
                if (!Int32.TryParse(Exam1, out tryInt) || !Int32.TryParse(Exam2, out tryInt) ||
                    !Int32.TryParse(Exam3, out tryInt) || !Int32.TryParse(Exam4, out tryInt) || 
                    !Int32.TryParse(Exam5, out tryInt) || !Int32.TryParse(Exam6, out tryInt))
                {
                    return "N/A";
                }
                return ((Int32.Parse(Exam1) + Int32.Parse(Exam2) +
                         Int32.Parse(Exam3) + Int32.Parse(Exam4) +
                         Int32.Parse(Exam5) + Int32.Parse(Exam6)) / 6 ).ToString();
            }
        }

        private bool IsGradeCorrect(string value)
        {
            int tryInt;
            if (value == null || (value != "N/A" && !(Int32.TryParse(value, out tryInt) && tryInt >= 0 && tryInt <= 100)))
            {
                return false;
            }
            return true;
        }

        // ICloneable
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
