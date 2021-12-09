using IBL_Zupree;
using Model_Zupree;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace UI_Zupree.Areas.Security.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        IAccountRepository iAccountRepository;
        public AccountController()
        {

        }
        public AccountController(IAccountRepository _iAccountRepository)
        {
            iAccountRepository = _iAccountRepository;
        }
        [HttpGet]
        public ActionResult Index(string SearchString, int? page)
        {
            if (SearchString != null)
            {
                page = 1;
            }
            IEnumerable<UserModelView> list = iAccountRepository.GetAll();
            if (!string.IsNullOrEmpty(SearchString))
            {
                list = list.Where(e => e.UserName.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        [AllowAnonymous]
        // GET: Security/Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ActionName("Login")]

        public ActionResult PostLogin(UserModelView obj)
        {
            int count = iAccountRepository.UserVilidationCheck(obj);

            if (count <= 0)
            {
                ModelState.AddModelError("UserName", "Invalid User Name or Password");
                //ViewBag.Message = "Invalid User Name or Password";
                return View();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(obj.UserName, false);
                return RedirectToAction("Index", "Home", new { area = "Common" });
            }
        }
        [HttpGet]
        public PartialViewResult userInfo()
        {
            string userName = User.Identity.Name;
            IEnumerable<UserModelView> objForId = iAccountRepository.GetByUserName(userName);
            var userId = objForId.Select(c => c.UserId);
            UserModelView obj = iAccountRepository.GetById(Convert.ToInt32(userId));
            return PartialView("~/Areas/Security/Views/Account/_UserDiv.cshtml", obj);
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ActionName("Signup")]
        public ActionResult PostSignup(UserModelView obj)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                bool isExists = iAccountRepository.isUserExists(obj);
                if (isExists)
                {
                    ModelState.AddModelError("UserName", "User Name Exists");
                    result = false;
                }
                else
                {
                    string fileName = Path.GetFileNameWithoutExtension(obj.ImageFile.FileName);
                    string extension = Path.GetExtension(obj.ImageFile.FileName);
                    obj.ImageName = fileName + extension;
                    obj.ImageUrl = "~/Images/UserImages/" + obj.ImageName;
                    fileName = Path.Combine(Server.MapPath(obj.ImageUrl));
                    obj.ImageFile.SaveAs(fileName);
                    iAccountRepository.Insert(obj);
                    result = true;
                }
            }
            if (result)
            {
                return RedirectToAction("Login", "Account", new { area = "Security" });
            }
            else
            {
                return View();
            }
        }
        public ActionResult SelectAndDelete(FormCollection formCollection)
        {
            string[] ids = formCollection["userId"].Split(new char[] { ',' });
            string imageName = "";
            foreach (string id in ids)
            {
                UserModelView obj = iAccountRepository.GetById(int.Parse(id));
                imageName = obj.ImageName;
                DeleteExistingImage(imageName);
                iAccountRepository.Delete(int.Parse(id));
            }
            return RedirectToAction("Index");
        }
        private void DeleteExistingImage(string imagename)
        {
            string root = Server.MapPath("~");
            string parent = Path.GetDirectoryName(root);
            string path = parent + "/Images/UserImages/" + imagename;
            FileInfo fileObj = new FileInfo(path);
            if (fileObj.Exists)
            {
                fileObj.Delete();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", new { area = "Security" });
        }
    }
}