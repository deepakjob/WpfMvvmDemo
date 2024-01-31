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

        public bool Add(StudentDto objNewEmployee)
        {
            bool IsAdded = false;
            //Age must be between 21 and 58
            if (objNewEmployee.Age < 21 || objNewEmployee.Age > 58)
                throw new ArgumentException("Invalid age limit for employee");

            try
            {
                var ObjEmployee = new Student();
                ObjEmployee.Id = objNewEmployee.Id;
                ObjEmployee.Name = objNewEmployee.Name;
                ObjEmployee.Age = objNewEmployee.Age;

                ObjContext.Students.Add(ObjEmployee);
                var NoOfRowsAffected = ObjContext.SaveChanges();
                IsAdded = NoOfRowsAffected > 0;
            }
            catch (SqlException ex)
            {

                throw ex;
            }

            return IsAdded;
        }

        public bool Update(StudentDto objEmployeeToUpdate)
        {
            bool IsUpdated = false;

            try
            {
                var ObjEmployee = ObjContext.Students.Find(objEmployeeToUpdate.Id);
                ObjEmployee.Name = objEmployeeToUpdate.Name;
                ObjEmployee.Age = objEmployeeToUpdate.Age;
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
