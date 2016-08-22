using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StudentsAccounting
{
    public class StudentRepository : IStudentRepository
    {
        private List<Student> studentList;
        public const int ExamCount = 6;

        public StudentRepository()
        {
            studentList = new List<Student>();
        }

        private StudentRepository(List<Student> studList)
        {
            studentList = studList;
        }

        public Student this[int index]
        {
            get
            {
                return studentList[index];
            }
            set
            {
                studentList[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return studentList.Count;
            }
        }

        public void AddStudent(Student newStudent)
        {
            if (newStudent == null)
            {
                throw new ArgumentNullException(nameof(newStudent));
            }

            studentList.Add((Student)newStudent.Clone());
        }

        public void RemoveStudent(int index)
        {
            studentList.Remove(studentList[index]);
        }

        public void Clear()
        {
            studentList.Clear();
        }

        public void SortByAge()
        {
            studentList.Sort(new SortByAgeHelper());
        }

        public void SortByName()
        {
            studentList.Sort(new SortByNameHelper());
        }

        public void SortByRating()
        {
            studentList.Sort(new SortByRatingHelper());
        }

        public IEnumerator<Student> GetEnumerator()
        {
            return studentList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return studentList.GetEnumerator();
        }

        public void LoadXml(string path)
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Student>));
            try
            {
                using (Stream fStream = new FileStream(path, FileMode.Open))
                {
                    studentList = (List<Student>)xmlFormat.Deserialize(fStream);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to write repository to a XML file", ex);
            }
        }

        public void SaveXml(string path)
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Student>));
            try
            {
                using (Stream fstream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    xmlFormat.Serialize(fstream, studentList);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to initialize repository", ex);
            }
        }

        public object Clone()
        {
            List<Student> resultList = (from stud in studentList select (Student)stud.Clone()).ToList();
            return new StudentRepository(resultList);
        }
    }
}
