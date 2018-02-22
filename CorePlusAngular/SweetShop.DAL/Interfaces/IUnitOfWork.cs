namespace SweetShop.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }

        void Save();
    }
}
