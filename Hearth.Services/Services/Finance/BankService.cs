using Hearth.Core.Data;
using Hearth.Core.Models.Finance;
using Hearth.Services.Abstract;
using Hearth.Services.DTOs.Finance.Account;
using Hearth.Services.DTOs.Finance.Bank;
using Hearth.Services.Interfaces.Finance;
using Hearth.Services.Mapping.Finance;
using Microsoft.EntityFrameworkCore;
using Hearth.Services.Utility;

namespace Hearth.Services.Services.Finance
{
    public class BankService : ASqliteTableService<Bank, BankDTO>, IBankService
    {
        public BankService(HearthDbContext context) : base(context) { }
        #region Abstract Class Setup
        protected override DbSet<Bank> DbSet => _context.Banks;
        protected override BankDTO ToDto(Bank entity) => entity.ToDto();
        protected override Bank ToEntity(BankDTO dto) => dto.ToEntity();
        protected override void ApplyUpdate(BankDTO dto, Bank entity) => dto.ApplyUpdate(entity);
        protected override async void ValidatePayload(BankDTO payload)
        {
            if (payload == null) throw new HearthInvalidPayloadException(nameof(payload));
            if (payload.Access_Token == null || payload.Item_Id == null || payload.UserId == null) throw new HearthInvalidPayloadException();
            if (await this.Exists(payload.Id)) throw new HearthRecordAlreadyExistsException();

            return;
        }
        #endregion

        #region Model Specific Functions
        public async Task<BankDTO?> GetByItemId(string itemid)
        {
            var bank = await _context.Banks
                .FirstOrDefaultAsync(b => b.Item_Id == itemid);

            if (bank == null)
            {
                throw new HearthRecordNotFoundException(itemid);
            }

            return bank.ToDto();
        }
        #endregion
    }
}
