using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entity;

namespace WebApi.Service
{
    public interface IStudentService
    {
        List<Student> GetAllData();
        Student GetStudentById(Guid id);
        Student GetStudentByName(string name);
        Task<bool> InsertStudent(Student obj);
        Task<bool> UpdateEmployee(Student obj);
        Task<bool> DeleteEmployee(Guid id);
    }
}
