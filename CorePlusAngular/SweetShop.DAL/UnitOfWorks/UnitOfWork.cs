using System;
using System.Collections.Generic;
using System.Text;
using SweetShop.DAL.Context;
using SweetShop.DAL.Interfaces;
using SweetShop.DAL.Repositories;

namespace SweetShop.DAL.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly Lazy<IProductRepository> _productRepository;
        private readonly ApplicationContext _db;
        private bool _disposed = false;

        public UnitOfWork(ApplicationContext context)
        {
            _db = context;
            _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(_db));
        }

        public IProductRepository Products => _productRepository.Value;

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
