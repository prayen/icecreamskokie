using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkokieIceCream.Entity;
using SkokieIceCream.Infrastructure;

namespace SkokieIceCream.Repository
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }

    public interface IStudentRepository : IRepository<Student>
    {
    }
}
