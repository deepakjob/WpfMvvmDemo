using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentWpfApp.Models
{
    public class StudentService
    {
        MvvmDemoDbContext ObjContext;
        public StudentService()
        {
            ObjContext = new MvvmDemoDbContext();
        }
        public List<StudentDto> GetAll()
        {
            List<StudentDto> ObjStudentList = new List<StudentDto>();
            try
            {
                var ObjQuery = from obj in ObjContext.Students
                               select obj;
                foreach (var student in ObjQuery)
                {
                    ObjStudentList.Add(new StudentDto { Id = student.Id, Name = student.Name, Age = student.Age });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjStudentList;
        }

        public bool Add(StudentDto objNewStudent)
        {
            bool IsAdded = false;
            //Age must be between 21 and 58
            if (objNewStudent.Age < 21 || objNewStudent.Age > 58)
                throw new ArgumentException("Invalid age limit for employee");

            try
            {
                var ObjStudent = new Student();
                ObjStudent.Id = objNewStudent.Id;
                ObjStudent.Name = objNewStudent.Name;
                ObjStudent.Age = objNewStudent.Age;

                ObjContext.Students.Add(ObjStudent);
                var NoOfRowsAffected = ObjContext.SaveChanges();
                IsAdded = NoOfRowsAffected > 0;
            }
            catch (SqlException ex)
            {

                throw ex;
            }

            return IsAdded;
        }

        public bool Update(StudentDto objStudentToUpdate)
        {
            bool IsUpdated = false;

            try
            {
                var ObjStudent = ObjContext.Students.Find(objStudentToUpdate.Id);
                ObjStudent.Name = objStudentToUpdate.Name;
                ObjStudent.Age = objStudentToUpdate.Age;
                var NoOfRowsAffected = ObjContext.SaveChanges();
                IsUpdated = NoOfRowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return IsUpdated;
        }

    }
}
