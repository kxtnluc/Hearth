using Hearth.Core.Data;
using Hearth.Core.Models.Finance;
using Hearth.Services.Abstract;
using Hearth.Services.DTOs.Finance.Account;
using Hearth.Services.DTOs.Finance.Bank;
using Hearth.Services.Filters;
using Hearth.Services.Filters.Finance;
using Hearth.Services.Interfaces.Finance;
using Hearth.Services.Mapping.Finance;
using Hearth.Services.Utility;
using Microsoft.EntityFrameworkCore;

namespace Hearth.Services.Services.Finance
{
    public class BankService : ASqliteTableService<Bank, BankDTO, SqliteTableFilter>, IBankService
    {
        public BankService(HearthDbContext context) : base(context) { }
        #region Abstract Class Setup
        protected override DbSet<Bank> DbSet => _context.Banks;
        protected override BankDTO ToDto(Bank entity) => entity.ToDto();
        protected override Bank ToEntity(BankDTO dto) => dto.ToEntity();
        protected override List<BankDTO> ToDtoList(List<Bank> entities) => entities.ToDtoList();
        protected override void ApplyUpdate(BankDTO dto, Bank entity) => dto.ApplyUpdate(entity);
        protected override async void ValidatePayload(BankDTO payload)
        {
            if (payload == null) throw new HearthInvalidPayloadException(nameof(payload));
            if (payload.Access_Token == null || payload.Item_Id == null || payload.UserId == null) throw new HearthInvalidPayloadException();
            if (await this.Exists(payload.Id)) throw new HearthRecordAlreadyExistsException();

            return;
        }
        #endregion

        #region Filter
        public override List<BankDTO> Filter(List<BankDTO> banks, SqliteTableFilter filter)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Model Specific Functions
        public async Task<BankDTO?> GetByItemId(string itemid)
        {
            var bank = await _context.Banks
                .SingleOrDefaultAsync(b => b.Item_Id == itemid);

            if (bank == null)
            {
                throw new HearthRecordNotFoundException(itemid);
            }

            return bank.ToDto();
        }

        public async Task<List<BankDTO>> GetByUserId(int userId)
        {
            var banks = await _context.Banks
                .Where(b => b.UserId == userId)
                .ToListAsync();

            if (banks == null)
            {
                throw new HearthRecordNotFoundException("Bank(s) not found under user: " + userId);
            }

            return ToDtoList(banks);
        }

        public async Task<List<string>> GetItemIdsByUserId(int userId)
        {
            var bankItemIds = await _context.Banks
                .Where(b => b.UserId == userId)
                .Select(b => b.Item_Id)
                .ToListAsync();

            if (bankItemIds.Count == 0) throw new HearthRecordNotFoundException("No item ids found under user: " + userId);

            return bankItemIds;
        }
        #endregion
    }
}
