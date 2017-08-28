using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week7.Repository.Entity;
using week7.Repository.Infrastructure;

namespace week7.Repository.Repository
{
    public class EmployeeRepository : RepositoryBase<Emp2>, IEmployeeRepository
    {
        public EmployeeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
    public interface IEmployeeRepository : IRepository<Emp2>
    {

    }
}
