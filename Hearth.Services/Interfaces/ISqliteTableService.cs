using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Interfaces
{
    public interface ISqliteTableService<TDto>
    {
        Task<TDto?> GetById(int id);
        Task<List<TDto>> GetAll();
        Task<TDto> Create(TDto payload);
        Task Update(TDto payload);
        Task Delete(int id);
    }
}
