using Hearth.Core.Data;
using Hearth.Services.Interfaces;
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
        /// Checks the payload for validity before creating or updating. Throws false if invalid. Implemented by the concrete service class.
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        protected abstract void ValidatePayload(TDto payload);
        public virtual async Task<TDto?> GetById(int id)
        {
            var entity = await DbSet.FindAsync(id);
            return entity is null ? default : ToDto(entity);
        }

        public virtual async Task<List<TDto>> GetAll()
        {
            var entities = await DbSet.AsNoTracking().ToListAsync();
            return entities.Select(ToDto).ToList();
        }

        public virtual async Task<TDto> Create(TDto payload)
        {
            try
            {
                ValidatePayload(payload);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Invalid payload for {typeof(TEntity).Name}: {payload} --- {ex.Message}");
            }
            catch(NotImplementedException)
            {
                // If ValidatePayload is not implemented, we can choose to ignore validation or throw an exception.
            }
            var entity = ToEntity(payload);
            DbSet.Add(entity);
            await _context.SaveChangesAsync();
            return ToDto(entity);
        }

        public virtual async Task Delete(int id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity is null) return;

            DbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Update(TDto payload)
        {
            var entity = await DbSet.FindAsync(payload.Id)
                ?? throw new KeyNotFoundException($"{typeof(TEntity).Name} {payload.Id} not found");

            ApplyUpdate(payload, entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateRange(List<TDto> payloads)
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

            await _context.SaveChangesAsync();
        }
    }
}
