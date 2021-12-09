using DAL_Zupree;
using IBL_Zupree;
using Model_Zupree;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Zupree
{
    public class SupplierRepository : ISupplierRepository
    {
        private ZupreeDBEntities dbObj;
        public SupplierRepository()
        {
            dbObj = new ZupreeDBEntities();
        }
        public IEnumerable<SupplierViewModel> GetAll()
        {
            IEnumerable<SupplierViewModel> listOfSupplier = dbObj.Suppliers.Select(e => new SupplierViewModel
            {
                SupplierId = e.SupplierId,
                CompanyName = e.CompanyName,
                ContactName = e.ContactName,
                Address = e.Address,
                Phone = e.Phone,
                Email = e.Email,
                ImageName = e.ImageName,
                ImageUrl = e.ImageUrl
            }).ToList();
            return listOfSupplier;
        }


        public void Insert(SupplierViewModel objModel)
        {
            Supplier objSupplier = new Supplier()
            {
                CompanyName = objModel.CompanyName,
                ContactName = objModel.ContactName,
                Address = objModel.Address,
                Phone = objModel.Phone,
                Email = objModel.Email,
                ImageName = objModel.ImageName,
                ImageUrl = objModel.ImageUrl
            };
            dbObj.Suppliers.Add(objSupplier);
            Save();
        }

        public SupplierViewModel GetById(int id)
        {
            Supplier supplier = dbObj.Suppliers.Find(id);
            SupplierViewModel obj = new SupplierViewModel();
            obj.SupplierId = supplier.SupplierId;
            obj.CompanyName = supplier.CompanyName;
            obj.ContactName = supplier.ContactName;
            obj.Address = supplier.Address;
            obj.Phone = supplier.Phone;
            obj.Email = supplier.Email;
            obj.ImageName = supplier.ImageName;
            obj.ImageUrl = supplier.ImageUrl;
            return obj;
        }
        public void Update(SupplierViewModel supplier)
        {
            Supplier obj = new Supplier();
            obj.SupplierId = supplier.SupplierId;
            obj.CompanyName = supplier.CompanyName;
            obj.ContactName = supplier.ContactName;
            obj.Address = supplier.Address;
            obj.Phone = supplier.Phone;
            obj.Email = supplier.Email;
            obj.ImageName = supplier.ImageName;
            obj.ImageUrl = supplier.ImageUrl;
            dbObj.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Delete(int id)
        {
            Supplier supplier = dbObj.Suppliers.Find(id);
            dbObj.Suppliers.Remove(supplier);
            Save();
        }

        public void Save()
        {
            dbObj.SaveChanges();
        }

    }
    public class CategoryRepository : ICategoryRepository
    {
        private ZupreeDBEntities dbObj;
        public CategoryRepository()
        {
            dbObj = new ZupreeDBEntities();
        }

        public IEnumerable<CategoryViewModel> GetAll()
        {
            IEnumerable<CategoryViewModel> listOfCategory = dbObj.Categories.Select(e => new CategoryViewModel
            {
                CategoryId = e.CategoryId,
                CategoryName = e.CategoryName,
            }).ToList();
            return listOfCategory;
        }

        public CategoryViewModel GetById(int id)
        {
            Category obj1 = dbObj.Categories.Find(id);
            CategoryViewModel obj = new CategoryViewModel();
            obj.CategoryId = obj1.CategoryId;
            obj.CategoryName = obj1.CategoryName;
            return obj;
        }

        public void Insert(CategoryViewModel objModel)
        {
            Category obj = new Category();
            obj.CategoryName = objModel.CategoryName;
            dbObj.Categories.Add(obj);
            Save();
        }


        public void Update(CategoryViewModel updateObj)
        {
            Category obj = new Category();
            obj.CategoryId = updateObj.CategoryId;
            obj.CategoryName = updateObj.CategoryName;
            dbObj.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Delete(int id)
        {
            Category category = dbObj.Categories.Find(id);
            dbObj.Categories.Remove(category);
            Save();
        }
        public void Save()
        {
            dbObj.SaveChanges();
        }
    }
    public class ProductRepository : IProductRepository
    {
        private ZupreeDBEntities dbObj;
        public ProductRepository()
        {
            dbObj = new ZupreeDBEntities();
        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            IEnumerable<ProductViewModel> listOfSupplier = dbObj.Products.Select(e => new ProductViewModel
            {
                ProductId = e.ProductId,
                ProductName = e.ProductName,
                PurchaseDate = e.PurchaseDate,
                CategoryName = e.Category.CategoryName,
                SupplierName = e.Supplier.CompanyName,
                Quantity = e.Quantity,
                UnitPrice = e.UnitPrice,
                MSRP = e.MSRP,
                ImageName = e.ImageName,
                ImageUrl = e.ImageUrl
            }).ToList();
            return listOfSupplier;
        }

        public ProductViewModel GetById(int id)
        {
            Product obj1 = dbObj.Products.Find(id);
            ProductViewModel obj = new ProductViewModel();
            obj.ProductId = obj1.ProductId;
            obj.ProductName = obj1.ProductName;
            obj.PurchaseDate = obj1.PurchaseDate;
            obj.CategoryName = obj1.Category.CategoryName;
            obj.SupplierName = obj1.Supplier.CompanyName;
            obj.Quantity = obj1.Quantity;
            obj.UnitPrice = obj1.UnitPrice;
            obj.MSRP = obj1.MSRP;
            obj.ImageName = obj1.ImageName;
            obj.ImageUrl = obj1.ImageUrl;
            return obj;
        }

        public void Insert(ProductViewModel e)
        {
            Product objProduct = new Product()
            {
                ProductId = e.ProductId,
                ProductName = e.ProductName,
                PurchaseDate = e.PurchaseDate,
                CategoryId = e.CategoryId,
                SupplierId = e.SupplierId,
                Quantity = e.Quantity,
                UnitPrice = e.UnitPrice,
                MSRP = e.MSRP,
                ImageName = e.ImageName,
                ImageUrl = e.ImageUrl
            };
            dbObj.Products.Add(objProduct);
            Save();
        }

        public void Update(ProductViewModel obj1)
        {
            Product obj = new Product();
            obj.ProductId = obj1.ProductId;
            obj.ProductName = obj1.ProductName;
            obj.PurchaseDate = obj1.PurchaseDate;
            obj.CategoryId = obj1.CategoryId;
            obj.SupplierId = obj1.SupplierId;
            obj.Quantity = obj1.Quantity;
            obj.UnitPrice = obj1.UnitPrice;
            obj.MSRP = obj1.MSRP;
            obj.ImageName = obj1.ImageName;
            obj.ImageUrl = obj1.ImageUrl;
            dbObj.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Delete(int id)
        {
            Product product = dbObj.Products.Find(id);
            dbObj.Products.Remove(product);
            Save();
        }
        public void Save()
        {
            dbObj.SaveChanges();
        }

    }
    public class SalesRepository : ISalesRepository
    {
        private ZupreeDBEntities dbObj;
        public SalesRepository()
        {
            dbObj = new ZupreeDBEntities();
        }

        public IEnumerable<SalesViewModel> GetAll()
        {

            IEnumerable<SalesViewModel> listOfSupplier = dbObj.Sales.Select(e => new SalesViewModel
            {
                SalesNumber = e.SalesNumber,
                CustomerName = e.CustomerName,
                CustomerPhone = e.CustomerPhone,
                CustomerEmail= e.CustomerEmail,
                ProductName = e.Product.ProductName,
                ImageName=e.Product.ImageName,
                ImageUrl=e.Product.ImageUrl,
                PaymentType = e.Payment.PaymentType,
                SalesUnitPrice = e.SalesUnitPrice,
                Quantity = e.Quantity,
                TotalPrice = e.SalesUnitPrice * e.Quantity,
            }).ToList();
            return listOfSupplier;

        }

        public SalesViewModel GetById(int id)
        {
            Sale obj1 = dbObj.Sales.Find(id);
            Product obj2 = dbObj.Products.Find(obj1.ProductId);
            SalesViewModel obj  = new SalesViewModel();
            obj.SalesNumber = obj1.SalesNumber;
            obj.CustomerName = obj1.CustomerName;
            obj.CustomerPhone = obj1.CustomerPhone;
            obj.CustomerEmail = obj1.CustomerEmail;
            obj.ProductId = obj1.ProductId;
            obj.PaymentId = obj1.PaymentId;
            obj.Quantity = obj1.Quantity;
            obj.SalesUnitPrice = obj1.SalesUnitPrice;
            obj.ImageName = obj2.ImageName;
            obj.ImageUrl = obj2.ImageUrl;
            return obj;
        }

        public void Insert(SalesViewModel e)
        {
            Sale objSales = new Sale()
            {
                CustomerName = e.CustomerName,
                CustomerPhone = e.CustomerPhone,
                CustomerEmail = e.CustomerEmail,
                ProductId = e.ProductId,
                PaymentId = e.PaymentId,
                Quantity = e.Quantity,
                SalesUnitPrice = e.SalesUnitPrice,
            };
            dbObj.Sales.Add(objSales);
            Save();
        }

        public void Update(SalesViewModel obj1)
        {
            Sale obj = new Sale();
            obj.SalesNumber = obj1.SalesNumber;
            obj.CustomerName = obj1.CustomerName;
            obj.CustomerPhone = obj1.CustomerPhone;
            obj.CustomerEmail = obj1.CustomerEmail;
            obj.ProductId = obj1.ProductId;
            obj.PaymentId = obj1.PaymentId;
            obj.Quantity = obj1.Quantity;
            obj.SalesUnitPrice = obj1.SalesUnitPrice; 
            dbObj.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Delete(int id)
        {
            Sale salesObj = dbObj.Sales.Find(id);
            dbObj.Sales.Remove(salesObj);
            Save();
        }

        public void Save()
        {
            dbObj.SaveChanges();
        }
    }
    public class PaymentRepository : IPaymentRepository
    {
        private ZupreeDBEntities dbObj;
        public PaymentRepository()
        {
            dbObj = new ZupreeDBEntities();
        }
        public IEnumerable<PaymentViewModel> GetAll()
        {

            IEnumerable<PaymentViewModel> listOfPaymentType = dbObj.Payments.Select(e => new PaymentViewModel
            {
                PaymentId = e.PaymentId,
                PaymentType = e.PaymentType,
            }).ToList();
            return listOfPaymentType;

        }
    }
}
