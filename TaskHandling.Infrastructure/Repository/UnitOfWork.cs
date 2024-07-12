using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsHandling.Application.Common.Abstraction.Factory;
using TicketsHandling.Application.Common.Abstraction.Repositories;
using TicketsHandling.Domain.Models;
using TicketsHandling.Persistence.Context;

namespace TicketsHandling.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;
        private readonly IRepositoryFactory _repositoryFactory;
        private IDbContextTransaction _transaction;

        public UnitOfWork(ApplicationDbContext context, IRepositoryFactory repositoryFactory)
        { 
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }
        public IDbContextTransaction BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
            return _transaction;
        }
        public async Task CommitTransactionAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
            }
        }
        public async Task RollbackTransactionAsync()
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
        public IBaseRepository<Ticket> TicketRepository => _repositoryFactory.GetRepository<Ticket>();

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
