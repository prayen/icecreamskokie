using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entity;
using WebApi.Infrastructure;
using WebApi.Repository;

namespace WebApi.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork = null;
        public StudentService(IStudentRepository studentRepository,
            IUnitOfWork unitOfWork)
        {
            this._studentRepository = studentRepository;
            this._unitOfWork = unitOfWork;
        }
        public List<Student> GetAllData()
        {
            var obj = _studentRepository.GetAll().ToList();
            return obj;
        }
        public Student GetStudentById(Guid id)
        {
            var obj = _studentRepository.GetById(id);
            return obj;
        }

        public Student GetStudentByName(string name)
        {
            var obj = _studentRepository.GetAll().Where(q => q.FirstName == name || q.LastName == name).FirstOrDefault();
            return obj;
        }

        public async Task<bool> InsertStudent(Student obj)
        {
            try
            {
                obj.Id = Guid.NewGuid();
                obj.CreatedDate = DateTime.Now;
                obj.ModifiedDate = DateTime.Now;
                _studentRepository.Add(obj);
                return await this._unitOfWork.CommitAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateEmployee(Student obj)
        {
            try
            {
                Student objStudent = _studentRepository.GetById(obj.Id);
                objStudent.FirstName = obj.FirstName;
                objStudent.LastName = obj.LastName;
                objStudent.Address = obj.Address;
                objStudent.DOB = obj.DOB;
                objStudent.CreatedDate = DateTime.Now;
                objStudent.ModifiedDate = DateTime.Now;
                _studentRepository.Update(objStudent);
                return await this._unitOfWork.CommitAsync();
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteEmployee(Guid id)
        {
            try
            {
                _studentRepository.Delete(id);
                return await this._unitOfWork.CommitAsync();
            }
            catch
            {
                return false;
            }
        }
    }
}
