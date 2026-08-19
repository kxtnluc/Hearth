using Hearth.Core.Data;
using Hearth.Services.DTOs;
using Hearth.Services.Interfaces;
using Hearth.Services.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Abstract
{
    public abstract class ASqliteTableService<TEntity, TDto> : ISqliteTableService<TDto>
        where TEntity : class
        where TDto : IDTO
    {
        protected readonly HearthDbContext _context;

        protected ASqliteTableService(HearthDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// The DbSet this service operates on, e.g. _context.Accounts
        /// </summary>
        protected abstract DbSet<TEntity> DbSet { get; }
        /// <summary>
        /// Maps an entity to its DTO — implemented by calling the entity's Mapperly-generated ToDto()
        /// </summary>
        protected abstract TDto ToDto(TEntity entity);
        /// <summary>
        /// Maps a DTO to a new entity — implemented by calling the DTO's Mapperly-generated ToEntity()
        /// </summary>
        protected abstract TEntity ToEntity(TDto dto);
        /// <summary>
        /// Applies non-null DTO values onto the tracked entity.
        /// </summary>
        protected abstract void ApplyUpdate(TDto dto, TEntity entity);
        /// <summary>
        /// Checks the payload for validity before creating or updating. Throws an InvalidPayloadException if invalid. Implemented by the concrete service class.
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        protected abstract void ValidatePayload(TDto payload);
        public virtual async Task<TDto?> GetById(int id)
        {
            var entity = await DbSet.FindAsync(id);

            if (entity == null) throw new HearthRecordNotFoundException();
            // i think this is redundant a bit
            return entity is null ? default : ToDto(entity);
        }

        public virtual async Task<List<TDto>> GetAll()
        {
            var entities = await DbSet.AsNoTracking().ToListAsync();
            return entities.Select(ToDto).ToList();
        }
        /// <summary>
        /// Creates the payload object in its table. [Save] may be set to false to reduce use of SaveChangesAsync(), allowing it to instead be called manually.
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        /// <exception cref="HearthInvalidPayloadException"></exception>
        /// <exception cref="HearthRecordAlreadyExistsException"></exception>
        public virtual async Task<TDto> Create(TDto payload, bool saveChanges = true)
        {
            try
            {
                ValidatePayload(payload);
            }
            catch (HearthInvalidPayloadException ex)
            {
                throw new HearthInvalidPayloadException($"Invalid payload for {typeof(TEntity).Name}: {payload} --- {ex.Message}");
            }
            catch (HearthRecordAlreadyExistsException ex)
            {
                throw new HearthRecordAlreadyExistsException($"Record already exists in Hearth of payload: {typeof(TEntity).Name}: {payload} --- {ex.Message}");
            }
            catch (NotImplementedException)
            {
                // If ValidatePayload is not implemented, we can choose to ignore validation or throw an exception.
            }
            var entity = ToEntity(payload);
            DbSet.Add(entity);
            if(saveChanges) await _context.SaveChangesAsync();
            return ToDto(entity);
        }

        /// <summary>
        /// Creates all payload objects as new rows. [saveChanges] may be set to false to reduce use of SaveChangesAsync(), allowing it to instead be called manually.
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        /// <exception cref="HearthInvalidPayloadException"></exception>
        /// <exception cref="HearthRecordAlreadyExistsException"></exception>
        public virtual async Task<List<TDto>> CreateRange(List<TDto> payload, bool saveChanges = true)
        {
            if (payload.Count == 0) return new List<TDto>();

            var entities = new List<TEntity>();

            foreach (var item in payload)
            {
                try
                {
                    ValidatePayload(item);
                }
                catch (HearthInvalidPayloadException ex)
                {
                    throw new HearthInvalidPayloadException($"Invalid payload for {typeof(TEntity).Name}: {item} --- {ex.Message}");
                }
                catch (HearthRecordAlreadyExistsException ex)
                {
                    throw new HearthRecordAlreadyExistsException($"Record already exists in Hearth of payload: {typeof(TEntity).Name}: {item} --- {ex.Message}");
                }
                catch (NotImplementedException)
                {
                    // If ValidatePayload is not implemented, we can choose to ignore validation or throw an exception.
                }

                entities.Add(ToEntity(item));
            }

            DbSet.AddRange(entities);
            if (saveChanges) await _context.SaveChangesAsync();

            return entities.Select(ToDto).ToList();
        }

        public virtual async Task Delete(int id, bool saveChanges = true)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity is null) return;

            DbSet.Remove(entity);
            if (saveChanges) await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes all rows matching the given ids. [saveChanges] may be set to false to reduce use of SaveChangesAsync(), allowing it to instead be called manually.
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        public virtual async Task DeleteRange(int[] ids, bool saveChanges = true)
        {
            if (ids.Length == 0) return;

            var entities = await DbSet
                .Where(e => ids.Contains(EF.Property<int>(e, "Id")))
                .ToListAsync();

            if (entities.Count == 0) return;

            DbSet.RemoveRange(entities);
            if (saveChanges) await _context.SaveChangesAsync();
        }

        public virtual async Task Update(TDto payload, bool saveChanges = true)
        {
            var entity = await DbSet.FindAsync(payload.Id)
                ?? throw new KeyNotFoundException($"{typeof(TEntity).Name} {payload.Id} not found");

            ApplyUpdate(payload, entity);
            if (saveChanges) await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateRange(List<TDto> payloads, bool saveChanges = true)
        {
            if (payloads.Count == 0) return;

            var ids = payloads.Select(p => p.Id).ToList();
            var entities = await DbSet
                .Where(e => ids.Contains(EF.Property<int>(e, "Id")))   // fine — still inside the query
                .ToListAsync();

            var idProperty = typeof(TEntity).GetProperty("Id")
                ?? throw new InvalidOperationException($"{typeof(TEntity).Name} has no 'Id' property.");

            var entityLookup = entities.ToDictionary(e => (int)idProperty.GetValue(e)!);

            foreach (var payload in payloads)
            {
                if (entityLookup.TryGetValue(payload.Id, out var entity))
                {
                    ApplyUpdate(payload, entity);
                }
            }

            if (saveChanges) await _context.SaveChangesAsync();
        }

        public virtual async Task<__TableDataDTO> GetTableData()
        {
            var rowCount = await DbSet.CountAsync();
            return new __TableDataDTO
            {
                Exists = rowCount > 0,
                IsEmpty = rowCount == 0,
                RowCount = rowCount
            };
        }
        
        public virtual async Task<bool> Exists(int id)
        {
            var entity = await DbSet.FindAsync(id);

            if(entity == null) return false;
            else return true;
        }
    }
}
