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
    public class RoleController : Controller
    {
        IRoleRepository iRoleRepository;
        public RoleController()
        {

        }
        public RoleController(IRoleRepository _iRoleRepository)
        {
            iRoleRepository = _iRoleRepository;
        }
        // GET: Security/Role
        public ActionResult Index()
        {
            IEnumerable<RoleViewModel> roleList = iRoleRepository.GetAll();
            return View(roleList);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            RoleViewModel obj = iRoleRepository.GetById(id);
            ViewBag.Details = "Show";
            return PartialView("~/Areas/Security/Views/Role/_Edit.cshtml", obj);
        }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult PostCreate(RoleViewModel obj)
        {
            var result = false;
            if (obj.RoleId == 0)
            {
                if (ModelState.IsValid)
                {
                    iRoleRepository.Insert(obj);
                    result = true;
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    iRoleRepository.Update(obj);
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
            RoleViewModel obj = iRoleRepository.GetById(id);
            ViewBag.Details = "Show";
            return PartialView("~/Areas/Security/Views/Role/_Delete.cshtml", obj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult PostDelete(RoleViewModel deleteViewObj)
        {
            iRoleRepository.Delete(deleteViewObj.RoleId);
            return RedirectToAction("Index");
        }

    }
}