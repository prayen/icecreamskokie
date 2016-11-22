using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.ApiData;

namespace WebApi.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
        bool TransactionCommit();

        Task<bool> CommitAsync();
    }
}
