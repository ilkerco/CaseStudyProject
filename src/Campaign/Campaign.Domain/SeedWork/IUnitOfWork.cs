using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Campaign.Domain.SeedWork
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
