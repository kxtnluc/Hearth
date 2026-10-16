using Hearth.Core.Data;
using Hearth.Core.Models.Finance;
using Hearth.Services.Abstract;
using Hearth.Services.CTOs.Finance.Account;
using Hearth.Services.DTOs.Finance.Account;
using Hearth.Services.DTOs.Finance.TransactionCategory;
using Hearth.Services.Filters;
using Hearth.Services.Filters.Finance;
using Hearth.Services.Interfaces.Finance;
using Hearth.Services.Mapping.Finance;
using Hearth.Services.Utility;
using Microsoft.EntityFrameworkCore;

namespace Hearth.Services.Services.Finance
{
    public class TransactionCategoryService : ASqliteTableService<TransactionCategory, TransactionCategoryDTO, SqliteTableFilter>, ITransactionCategoryService
    {
        public TransactionCategoryService(
            HearthDbContext context
        )
        : base(context)
        {}
        #region Abstract Class Setup
        protected override DbSet<TransactionCategory> DbSet => _context.TransactionCategories;
        protected override TransactionCategoryDTO ToDto(TransactionCategory entity) => entity.ToDto();
        protected override TransactionCategory ToEntity(TransactionCategoryDTO dto) => dto.ToEntity();
        protected override List<TransactionCategoryDTO> ToDtoList(List<TransactionCategory> entities) => entities.ToDtoList();
        protected override void ApplyUpdate(TransactionCategoryDTO dto, TransactionCategory entity) => dto.ApplyUpdate(entity);
        protected override void ValidatePayload(TransactionCategoryDTO payload)
        {
            // To implement, simply fill out this function
            throw new NotImplementedException();
        }
        #endregion

        #region Filter
        public override List<TransactionCategoryDTO> Filter(List<TransactionCategoryDTO> __objects, SqliteTableFilter filter)
        {
            // To implement, create a unique Filter object, replace it in this service, and fill out this function
            throw new NotImplementedException();
        }
        #endregion

        #region Model Specific Functions
        public async Task<TransactionCategoryDTO?> GetByIdWithRules(int id)
        {
            var trancat = await _context.TransactionCategories
                .Include(c => c.Rules)
                    .ThenInclude(r => r.RuleConditions)
                .SingleOrDefaultAsync(c => c.Id == id);

            return trancat?.ToDto();
        }

        public async Task<Dictionary<int, string>> GetAllAsDictionary()
        {
            return await _context.TransactionCategories
                .ToDictionaryAsync(cat => cat.Id, cat => cat.Name);
        }
        #endregion
        #region CTO Generation

        #endregion
    }
}