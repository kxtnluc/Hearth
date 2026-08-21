using Hearth.Services.DTOs.Finance.Account;
using Hearth.Services.DTOs.Finance.Bank;
using Hearth.Services.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Interfaces.Finance
{
    public interface IBankService : ISqliteTableService<BankDTO, SqliteTableFilter>
    {
        /// <summary>
        /// Grabs one bank by its specific unique item_id
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        Task<BankDTO?> GetByItemId(string itemId);
        /// <summary>
        /// Get all the banks under the userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of BankDTOs</returns>
        Task<List<BankDTO>> GetByUserId(int userId);
        /// <summary>
        /// Used to quickly grab just the item_ids assosiated with that user, rather than the whole bank object, like GetByUserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of strings</returns>
        Task<List<string>> GetItemIdsByUserId(int userId);
    }
}