using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentsAccounting;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            StudentRepository repo = new StudentRepository();
            repo.AddStudent(new Student(new DateTime(1998, 3, 30), new string[]
            {
                "5", "5", "5", "5", "5", "5"
            }, "Zoe", "Ostapyuk"));

            repo.AddStudent(new Student(new DateTime(1998, 3, 30), new string[]
            {
                "5", "5", "5", "5", "5", "5"
            }, "Zoe", "Ostapyuk"));

            repo.AddStudent(new Student(new DateTime(1998, 3, 30), new string[]
            {
                "5", "5", "5", "5", "5", "5"
            }, "Roman", "Ostapyuk"));
            repo.SaveXml("doc.xml");
        }
    }
}
