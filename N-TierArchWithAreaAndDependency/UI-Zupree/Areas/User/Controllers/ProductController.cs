using CrystalDecisions.CrystalReports.Engine;
using IBL_Zupree;
using Model_Zupree;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI_Zupree.Areas.User.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        IProductRepository iProductRepository;
        ISupplierRepository iSupplierRepository;
        ICategoryRepository iCategoryRepository;
        public ProductController()
        {

        }
        public ProductController(IProductRepository _iProductRepository, ISupplierRepository _iSupplierRepository, ICategoryRepository _iCategoryRepository)
        {
            iProductRepository = _iProductRepository;
            iSupplierRepository = _iSupplierRepository;
            iCategoryRepository = _iCategoryRepository;
        }

        // GET: User/Product
        public ActionResult Index(string SearchString, string CurrentFilter, string SortOrder, int? page)
        {
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = CurrentFilter;
            }
            ViewBag.SortNameParam = string.IsNullOrEmpty(SortOrder) ? "name_desc" : "";
            ViewBag.CurrentFilter = SearchString;
            IEnumerable<ProductViewModel> listOfProductModels = iProductRepository.GetAll();
            if (!string.IsNullOrEmpty(SearchString))
            {
                listOfProductModels = listOfProductModels.Where(e => e.ProductName.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }
            switch (SortOrder)
            {
                case "name_desc":
                    listOfProductModels = listOfProductModels.OrderByDescending(e => e.ProductId).ToList();
                    break;
                default:
                    listOfProductModels = listOfProductModels.OrderBy(e => e.ProductId).ToList();
                    break;

            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(listOfProductModels.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ViewResult Create()
        {
            IEnumerable<CategoryViewModel> listOfCategory = iCategoryRepository.GetAll();
            ViewBag.Categories = listOfCategory;
            IEnumerable<SupplierViewModel> listOfSupplier = iSupplierRepository.GetAll();
            ViewBag.Suppliers = listOfSupplier;
            return View();
        }
        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            ViewBag.Categories = iCategoryRepository.GetAll();
            ViewBag.Suppliers = iSupplierRepository.GetAll();
            ProductViewModel obj = iProductRepository.GetById(id);
            ViewBag.Details = "Show";
            return PartialView("~/Areas/User/Views/Product/_Edit.cshtml", obj);
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult PostCreate(ProductViewModel obj)
        {
            var result = false;
            if (obj.ProductId == 0)
            {
                if (ModelState.IsValid)
                {
                    string fileName = Path.GetFileNameWithoutExtension(obj.ImageFile.FileName);
                    string extension = Path.GetExtension(obj.ImageFile.FileName);
                    obj.ImageName = fileName + extension;
                    obj.ImageUrl = "~/Images/ProductImages/" + obj.ImageName;
                    fileName = Path.Combine(Server.MapPath(obj.ImageUrl));
                    obj.ImageFile.SaveAs(fileName);
                    iProductRepository.Insert(obj);
                    result = true;
                }
            }
            else
            {
                if (ModelState.IsValid)
                {

                    if (obj.ImageFile != null)
                    {
                        DeleteExistingImage(obj.ImageName);
                        string fileName = Path.GetFileNameWithoutExtension(obj.ImageFile.FileName);
                        string extension = Path.GetExtension(obj.ImageFile.FileName);
                        obj.ImageName = fileName + extension;
                        obj.ImageUrl = "~/Images/ProductImages/" + obj.ImageName;
                        fileName = Path.Combine(Server.MapPath(obj.ImageUrl));
                        obj.ImageFile.SaveAs(fileName);
                    }
                    iProductRepository.Update(obj);
                    result = true;
                }
            }
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                IEnumerable<CategoryViewModel> listOfCategory = iCategoryRepository.GetAll();
                ViewBag.Categories = listOfCategory;
                IEnumerable<SupplierViewModel> listOfSupplier = iSupplierRepository.GetAll();
                ViewBag.Suppliers = listOfSupplier;
                return View();
            }
        }
        public PartialViewResult Delete(int id)
        {
            ProductViewModel obj = iProductRepository.GetById(id);
            ViewBag.Details = "Show";
            IEnumerable<CategoryViewModel> listOfCategory = iCategoryRepository.GetAll();
            ViewBag.Categories = listOfCategory;
            IEnumerable<SupplierViewModel> listOfSupplier = iSupplierRepository.GetAll();
            ViewBag.Suppliers = listOfSupplier;
            return PartialView("~/Areas/User/Views/Product/_Delete.cshtml", obj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult PostDelete(ProductViewModel deleteViewObj)
        {
            string imageName = deleteViewObj.ImageName;
            DeleteExistingImage(imageName);
            iProductRepository.Delete(deleteViewObj.ProductId);
            return RedirectToAction("Index");
        }

        public ActionResult SelectAndDelete(FormCollection formCollection)
        {
            string[] ids = formCollection["productId"].Split(new char[] { ',' });
            string imageName = "";
            foreach (string id in ids)
            {
                ProductViewModel obj =  iProductRepository.GetById(int.Parse(id));
                imageName = obj.ImageName;
                DeleteExistingImage(imageName);
                iProductRepository.Delete(int.Parse(id));
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public PartialViewResult Details(int id)
        {
            ProductViewModel obj = iProductRepository.GetById(id);
            IEnumerable<CategoryViewModel> listOfCategory = iCategoryRepository.GetAll();
            ViewBag.Categories = listOfCategory;
            IEnumerable<SupplierViewModel> listOfSupplier = iSupplierRepository.GetAll();
            ViewBag.Suppliers = listOfSupplier;
            ViewBag.Details = "Show";
            return PartialView("~/Areas/User/Views/Product/_Details.cshtml", obj);
        }
        private void DeleteExistingImage(string imagename)
        {
            string root = Server.MapPath("~");
            string parent = Path.GetDirectoryName(root);
            string path = parent + "/Images/ProductImages/" + imagename;
            FileInfo fileObj = new FileInfo(path);
            if (fileObj.Exists)
            {
                fileObj.Delete();
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ExportReport()
        {
            ReportDocument rd = new ReportDocument();
            string root = Server.MapPath("~");
            string parent = Path.GetDirectoryName(root);
            string productImagePath = parent + "/Images/ProductImages/";
            string path = parent + "/Reports/" + "CrystalReport1.rpt";
            rd.Load(path);
            IEnumerable<ProductViewModel> listOfProductModels = iProductRepository.GetAll();
            rd.SetDataSource(listOfProductModels.Select(c => new
            {
                ProductId = c.ProductId,
                ProductName = c.ProductName,
                PurchaseDate = c.PurchaseDate,
                CategoryName = c.CategoryName,
                SupplierName = c.SupplierName,
                Quantity = c.Quantity,
                UnitPrice = c.UnitPrice,
                ImageName = productImagePath + c.ImageName,
                ImageUrl = c.ImageUrl
            }).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "PurchaseReport.pdf");
        }
    }
}