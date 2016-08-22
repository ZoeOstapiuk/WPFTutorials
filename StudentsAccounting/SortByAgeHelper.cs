using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsAccounting
{
    public class SortByAgeHelper : IComparer<Student>
    {
        public int Compare(Student x, Student y)
        {
            // By default, age sorting means from younger to older
            return x.BirthDate.Date.CompareTo(y.BirthDate.Date) * -1;
        }
    }
}
