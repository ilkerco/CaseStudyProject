using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.SeedWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        Task<bool> SaveEntitiesAsync();
    }
}
