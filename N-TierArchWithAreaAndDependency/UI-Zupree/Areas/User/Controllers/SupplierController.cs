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
    public class SupplierController : Controller
    {
        private ISupplierRepository iSupplierRepository;
        public SupplierController()
        {
                
        }
        public SupplierController(ISupplierRepository _iSupplierRepository)
        {
            iSupplierRepository = _iSupplierRepository;
        }
        // GET: User/Supplier
        public ActionResult Index()
        {
            IEnumerable<SupplierViewModel> listOfSupplierModels = iSupplierRepository.GetAll();
            return View(listOfSupplierModels);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            SupplierViewModel obj = iSupplierRepository.GetById(id);
            ViewBag.Details = "Show";
            return PartialView("~/Areas/User/Views/Supplier/_Edit.cshtml", obj);
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult PostCreate(SupplierViewModel obj)
        {
            var result = false;
            if (obj.SupplierId == 0)
            {
                if (ModelState.IsValid)
                {
                    string fileName = Path.GetFileNameWithoutExtension(obj.ImageFile.FileName);
                    string extension = Path.GetExtension(obj.ImageFile.FileName);
                    obj.ImageName = fileName + extension;
                    obj.ImageUrl = "~/Images/SupplierImages/" + obj.ImageName;
                    fileName = Path.Combine(Server.MapPath(obj.ImageUrl));
                    obj.ImageFile.SaveAs(fileName);
                    iSupplierRepository.Insert(obj);
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
                        obj.ImageUrl = "~/Images/SupplierImages/" + obj.ImageName;
                        fileName = Path.Combine(Server.MapPath(obj.ImageUrl));
                        obj.ImageFile.SaveAs(fileName);
                    }
                    iSupplierRepository.Update(obj);
                    result = true;
                }
            }
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public PartialViewResult Delete(int id)
        {
            SupplierViewModel obj = iSupplierRepository.GetById(id);
            ViewBag.Details = "Show";
            return PartialView("~/Areas/User/Views/Supplier/_Delete.cshtml", obj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult PostDelete(SupplierViewModel deleteViewObj)
        {
            string imageName = deleteViewObj.ImageName;
            DeleteExistingImage(imageName);
            iSupplierRepository.Delete(deleteViewObj.SupplierId);
            return RedirectToAction("Index");
        }

        public PartialViewResult Details(int id)
        {
            SupplierViewModel obj = iSupplierRepository.GetById(id);
            ViewBag.Details = "Show";
            return PartialView("~/Areas/User/Views/Supplier/_Details.cshtml", obj);
        }
        private void DeleteExistingImage(string imagename)
        {
            string root = Server.MapPath("~");
            string parent = Path.GetDirectoryName(root);
            string path = parent + "/Images/SupplierImages/" + imagename;
            FileInfo fileObj = new FileInfo(path);
            if (fileObj.Exists)
            {
                fileObj.Delete();
            }
        }

    }
}