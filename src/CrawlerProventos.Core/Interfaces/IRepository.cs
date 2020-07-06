using CrawlerProventos.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrawlerProventos.Core.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        Task CreateAsync(IEnumerable<T> entities);

        Task DeleteAllAsync();

        Task<IEnumerable<Provento>> GetAllAsync();
    }
}
