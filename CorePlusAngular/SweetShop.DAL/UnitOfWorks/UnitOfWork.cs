using System;
using SweetShop.DAL.Context;
using SweetShop.DAL.Entities;
using SweetShop.DAL.Interfaces;
using SweetShop.DAL.Repositories;

namespace SweetShop.DAL.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly Lazy<IGenericRepository<Product>> _productRepository;
        private readonly Lazy<IGenericRepository<Company>> _companyRepository;
        private readonly ApplicationContext _db;
        private bool _disposed = false;

        public UnitOfWork(ApplicationContext context)
        {
            _db = context;
            _productRepository = new Lazy<IGenericRepository<Product>>(() => new GenericRepository<Product>(_db));
            _companyRepository = new Lazy<IGenericRepository<Company>>(() => new GenericRepository<Company>(_db));
        }

        public IGenericRepository<Product> Products => _productRepository.Value;

        public IGenericRepository<Company> Companies => _companyRepository.Value;

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _db?.Dispose();
            }

            _disposed = true;
        }
    }
}
