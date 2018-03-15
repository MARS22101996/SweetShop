using System;
using System.Threading.Tasks;
using SweetShop.DAL.Context;
using SweetShop.DAL.Entities;
using SweetShop.DAL.Interfaces;
using SweetShop.DAL.Repositories;

namespace SweetShop.DAL.UnitOfWorks
{
   public class UnitOfWork : IUnitOfWork, IDisposable
   {
      private readonly Lazy<IProductRepository> _productRepository;
      private readonly Lazy<ICustomerRepository> _customerRepository;
      private readonly Lazy<IGenericRepository<Company>> _companyRepository;
      private readonly Lazy<IGenericRepository<ProductCustomer>> _productCustomerRepository;
      private readonly ApplicationContext _db;
      private bool _disposed = false;

      public UnitOfWork(ApplicationContext context)
      {
         _db = context;
         _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(_db));
         _companyRepository = new Lazy<IGenericRepository<Company>>(() => new GenericRepository<Company>(_db));
         _customerRepository = new Lazy<ICustomerRepository>(() => new CustomersRepository(_db));
         _productCustomerRepository =
         new Lazy<IGenericRepository<ProductCustomer>>(() => new GenericRepository<ProductCustomer>(_db));
      }

      public IProductRepository Products => _productRepository.Value;

      public IGenericRepository<Company> Companies => _companyRepository.Value;

      public ICustomerRepository Customers => _customerRepository.Value;

      public IGenericRepository<ProductCustomer> ProductCustomers => _productCustomerRepository.Value;

      public void Save()
      {
         _db.SaveChanges();
      }

      public async Task SaveChangesAsync()
      {
         await _db.SaveChangesAsync();
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
