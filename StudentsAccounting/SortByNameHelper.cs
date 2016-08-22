using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsAccounting
{
    public class SortByNameHelper : IComparer<Student>
    {
        public int Compare(Student x, Student y)
        {
            if (x.LastName != y.LastName)
            {
                return x.LastName.CompareTo(y.LastName);
            }
            return x.FirstName.CompareTo(y.FirstName);
        }
    }
}
