using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsAccounting
{
    public class CurrentFileStateInfo
    {
        public string FileName { get; set; }

        public bool IsChangedAfterLastSaving { get; set; } 

        public IStudentRepository StudentsRepo { get; set; } 
    }
}
