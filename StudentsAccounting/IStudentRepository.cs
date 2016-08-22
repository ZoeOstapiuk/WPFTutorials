using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsAccounting
{
    public interface IStudentRepository : IEnumerable<Student>, ICloneable
    {
        int Count { get; }

        Student this[int index] { get; set; }

        void SortByName();

        void SortByAge();

        void SortByRating();

        void RemoveStudent(int index);

        void Clear();

        void AddStudent(Student newStudent);

        void SaveXml(string path);

        void LoadXml(string opath);
    }
}
