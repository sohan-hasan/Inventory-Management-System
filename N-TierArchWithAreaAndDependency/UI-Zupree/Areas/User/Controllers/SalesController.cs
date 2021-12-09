using IBL_Zupree;
using Model_Zupree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI_Zupree.Areas.User.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        ISalesRepository iSalesRepository;
        IProductRepository iProductRepository;
        IPaymentRepository iPaymentRepository;
        public SalesController()
        {

        }
        public SalesController(ISalesRepository _iSalesRepository, IProductRepository _iProductRepository, IPaymentRepository _iPaymentRepository)
        {
            iSalesRepository = _iSalesRepository;
            iProductRepository = _iProductRepository;
            iPaymentRepository = _iPaymentRepository;
        }
        // GET: User/Sales
        public ActionResult Index()
        {
            IEnumerable<SalesViewModel> listOfSalesModels = iSalesRepository.GetAll();
            return View(listOfSalesModels);
        }
        [HttpGet]
        public ViewResult Create()
        {
            IEnumerable<PaymentViewModel> listOfPaymentType = iPaymentRepository.GetAll();
            ViewBag.Payments = listOfPaymentType;
            return View();
        }
        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            IEnumerable<PaymentViewModel> listOfPaymentType = iPaymentRepository.GetAll();
            ViewBag.Payments = listOfPaymentType;
            IEnumerable<ProductViewModel> listOfProduct = iProductRepository.GetAll();
            ViewBag.Products = listOfProduct;
            SalesViewModel obj = iSalesRepository.GetById(id);
            ViewBag.Details = "Show";
            return PartialView("~/Areas/User/Views/Sales/_Edit.cshtml", obj);
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult PostCreate(SalesViewModel obj)
        {
            var result = false;
            if (obj.SalesNumber == 0)
            {
                if (ModelState.IsValid)
                {
                    iSalesRepository.Insert(obj);
                    result = true;
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    iSalesRepository.Update(obj);
                    result = true;
                }
            }
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                IEnumerable<PaymentViewModel> listOfPaymentType = iPaymentRepository.GetAll();
                ViewBag.Payments = listOfPaymentType;
                return View();
            }
        }
        public PartialViewResult Delete(int id)
        {
            SalesViewModel obj = iSalesRepository.GetById(id);
            ViewBag.Details = "Show";
            IEnumerable<PaymentViewModel> listOfPaymentType = iPaymentRepository.GetAll();
            ViewBag.Payments = listOfPaymentType;
            IEnumerable<ProductViewModel> listOfProduct = iProductRepository.GetAll();
            ViewBag.Products = listOfProduct;
            return PartialView("~/Areas/User/Views/Sales/_Delete.cshtml", obj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult PostDelete(SalesViewModel deleteViewObj)
        {
            iSalesRepository.Delete(deleteViewObj.SalesNumber);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public PartialViewResult Details(int id)
        {
            SalesViewModel obj = iSalesRepository.GetById(id);
            ViewBag.Details = "Show";
            IEnumerable<PaymentViewModel> listOfPaymentType = iPaymentRepository.GetAll();
            ViewBag.Payments = listOfPaymentType;
            IEnumerable<ProductViewModel> listOfProduct = iProductRepository.GetAll();
            ViewBag.Products = listOfProduct;
            return PartialView("~/Areas/User/Views/Sales/_Details.cshtml", obj);
        }
        public JsonResult GetProduct()
        {
            return Json(iProductRepository.GetAll(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllById(int productId)
        {
            return Json(iProductRepository.GetById(productId), JsonRequestBehavior.AllowGet);
        }
    }
}