using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearth.Data;

namespace Hearth.Models.Interfaces
{
    public interface ITableFunctions<T>
    {
        Task Add(HearthDbContext dbContext, T item);
        List<T> GetAll(HearthDbContext dbContext);
        Task Remove(HearthDbContext dbContext, T item);
        Task Update(HearthDbContext dbContext, T item);
    }
}
