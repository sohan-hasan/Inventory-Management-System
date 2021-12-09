using IBL_Zupree;
using Model_Zupree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI_Zupree.Areas.Common.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        IProductRepository iProductRepository;
        public HomeController()
        {

        }
        public HomeController(IProductRepository _iProductRepository)
        {
            iProductRepository = _iProductRepository;
        }
        // GET: Common/Home
        public ActionResult Index()
        {
           //IEnumerable<ProductViewModel> list = iProductRepository.GetAll();
           return View();
        }
    }
}