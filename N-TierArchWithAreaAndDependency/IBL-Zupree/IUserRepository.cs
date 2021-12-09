using Model_Zupree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL_Zupree
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryViewModel> GetAll();
        CategoryViewModel GetById(int id);
        void Insert(CategoryViewModel objModel);
        void Update(CategoryViewModel updateObj);
        void Delete(int id);
        void Save();
    }
    public interface ISupplierRepository
    {
        IEnumerable<SupplierViewModel> GetAll();
        SupplierViewModel GetById(int id);
        void Insert(SupplierViewModel objModel);
        void Update(SupplierViewModel updateObj);
        void Delete(int id);
        void Save();
    }
    public interface IProductRepository
    {
        IEnumerable<ProductViewModel> GetAll();
        ProductViewModel GetById(int id);
        void Insert(ProductViewModel objModel);
        void Update(ProductViewModel updateObj);
        void Delete(int id);
        void Save();
    }
    public interface ISalesRepository
    {
        IEnumerable<SalesViewModel> GetAll();
        SalesViewModel GetById(int id);
        void Insert(SalesViewModel objModel);
        void Update(SalesViewModel updateObj);
        void Delete(int id);
        void Save();
    }
    public interface IPaymentRepository
    {
        IEnumerable<PaymentViewModel> GetAll();
    }
}
