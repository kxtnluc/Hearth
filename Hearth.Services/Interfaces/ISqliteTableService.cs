using Hearth.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Interfaces
{
    public interface ISqliteTableService<TDto, TFilter>
    {
        Task<TDto?> GetById(int id);
        Task<List<TDto>> GetAll();
        Task<TDto> Create(TDto payload, bool saveChanges = true);
        Task<List<TDto>> CreateRange(List<TDto> payload, bool saveChanges = true);
        Task Update(TDto payload, bool saveChanges = true);
        Task UpdateRange(List<TDto> payloads, bool saveChanges = true);
        Task Delete(int id, bool saveChanges = true);
        Task DeleteRange(int[] ids, bool saveChanges = true);
        Task<__TableDataDTO> GetTableData();
        Task<bool> Exists(int id);
        List<TDto> Filter(List<TDto> payload, TFilter filter);
    }
}
