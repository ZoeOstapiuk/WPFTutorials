using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsAccounting
{
    public class SortByRatingHelper : IComparer<Student>
    {
        public int Compare(Student x, Student y)
        {
            if (x.RatingGrade != "N/A")
            {
                if (y.RatingGrade != "N/A")
                {
                    return Convert.ToInt32(x.RatingGrade).CompareTo(Convert.ToInt32(y.RatingGrade));
                }
                return -1;
            }
            if (y.RatingGrade != "N/A")
            {
                return 1;
            }
            return 0;
        }
    }
}
