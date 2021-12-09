using IBL_Zupree;
using Model_Zupree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI_Zupree.Areas.User.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        ICategoryRepository iCategoryRepository;
        public CategoryController()
        {

        }
        public CategoryController(ICategoryRepository _iCategoryRepository)
        {
            iCategoryRepository = _iCategoryRepository;
        }
        // GET: User/Category
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            IEnumerable<CategoryViewModel> listOfCategory = iCategoryRepository.GetAll();
            return View(listOfCategory);
        }
        public ActionResult AjaxCreate(CategoryViewModel obj)
        {
            if (ModelState.IsValid == true)
            {
                System.Threading.Thread.Sleep(1000);
                iCategoryRepository.Insert(obj);
            }
            var Model = iCategoryRepository.GetAll();
            return PartialView("~/Areas/User/Views/Category/_Category.cshtml", Model);
        }
        public PartialViewResult Edit(int id)
        {
            CategoryViewModel obj = iCategoryRepository.GetById(id);
            ViewBag.Details = "Show";
            return PartialView("~/Areas/User/Views/Category/_Edit.cshtml", obj);

        }
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult PostEdit(CategoryViewModel obj)
        {
            var result = false;
            if (obj.CategoryId > 0)
            {
                if (ModelState.IsValid)
                {
                    iCategoryRepository.Update(obj);
                    result = true;
                }
            }
            if (result)
            {
                return RedirectToAction("Create");
            }
            else
            {
                return View();
            }
        }
        public PartialViewResult Delete(int id)
        {
            CategoryViewModel obj = iCategoryRepository.GetById(id);
            ViewBag.Details = "Show";
            return PartialView("~/Areas/User/Views/Category/_Delete.cshtml", obj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult PostDelete(CategoryViewModel deleteViewObj)
        {
            iCategoryRepository.Delete(deleteViewObj.CategoryId);
            return RedirectToAction("Create");
        }
    }
}