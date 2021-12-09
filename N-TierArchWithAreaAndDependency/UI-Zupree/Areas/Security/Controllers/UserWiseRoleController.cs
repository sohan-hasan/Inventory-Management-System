using IBL_Zupree;
using Model_Zupree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI_Zupree.Areas.Security.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserWiseRoleController : Controller
    {
        IUserWiseRoleRepository iUserWiseRoleRepository;
        IRoleRepository iRoleRepository;
        IAccountRepository iAccountRepository;
        public UserWiseRoleController()
        {

        }
        public UserWiseRoleController(IUserWiseRoleRepository _iUserWiseRoleRepository, IRoleRepository _iRoleRepository, IAccountRepository _iAccountRepository)
        {
            iUserWiseRoleRepository = _iUserWiseRoleRepository;
            iRoleRepository = _iRoleRepository;
            iAccountRepository = _iAccountRepository;
        }

        // GET: Security/UserWiseRoles
        public ActionResult Index()
        {
            return View(iUserWiseRoleRepository.GetAll());
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Role = iRoleRepository.GetAll();
            ViewBag.User = iAccountRepository.GetAll();
            return View();
        }

        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            ViewBag.Role = iRoleRepository.GetAll();
            ViewBag.User = iAccountRepository.GetAll();
            UserWiseRoleViewModel obj = iUserWiseRoleRepository.GetById(id);
            ViewBag.Details = "Show";
            return PartialView("~/Areas/Security/Views/UserWiseRole/_Edit.cshtml", obj);
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult PostCreate(UserWiseRoleViewModel obj)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                if (obj.uwrId==0)
                {
                    iUserWiseRoleRepository.Insert(obj);
                    result = true;
                }
                else
                {
                    iUserWiseRoleRepository.Update(obj);
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
        [HttpGet]
        public PartialViewResult Delete(int id)
        {
            ViewBag.Role = iRoleRepository.GetAll();
            ViewBag.User = iAccountRepository.GetAll();
            UserWiseRoleViewModel obj = iUserWiseRoleRepository.GetById(id);
            ViewBag.Details = "Show";
            return PartialView("~/Areas/Security/Views/UserWiseRole/_Delete.cshtml", obj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult PostDelete(UserWiseRoleViewModel deleteViewObj)
        {
            iUserWiseRoleRepository.Delete(deleteViewObj.uwrId);
            return RedirectToAction("Index");
        }
        public JsonResult GetAll()
        {
            return Json(iAccountRepository.GetAll(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllById(int userId)
        {
            return Json(iAccountRepository.GetById(userId), JsonRequestBehavior.AllowGet);
        }
    }
}