using AuthenticationUsingDapperFromScratch.Domain.Interfaces;
using AuthenticationUsingDapperFromScratch.Infrastructure.Repositories;
using System.Data;

namespace AuthenticationUsingDapperFromScratch.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork() { }
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;
        private IUserRepository _userRepository;
        private ICompanyRepository _companyRepository;

        public UnitOfWork(IDBConnectionFactory connectionFactory)
        {
            var _connectionfactory = connectionFactory ?? throw new ArgumentNullException(nameof(IDBConnectionFactory));
            _connection=_connectionfactory.CreateConnection();
        }

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_connection);
        public ICompanyRepository CompanyRepository => _companyRepository ??= new CompanyRepository(_connection);
        public void BeginTransaction()
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("Transaction is already in progress.");
            }

            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No transaction to commit.");
            }

            _transaction.Commit();
            _transaction.Dispose();
            _transaction = null;
        }

        public void Rollback()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No transaction to rollback.");
            }

            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
                _transaction = null;
            }

            _connection.Dispose();
        }

    }
}
