using BLL_Zupree;
using IBL_Zupree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UI_Zupree
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AccountRepository obj = new AccountRepository();
            obj.CreateInitialUserAndRole();
            UnityConfig.RegisterComponents();
            GlobalFilters.Filters.Add(new AuthorizeAttribute());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }


        public class FilterConfig
        {
            public static void RegisterGlobalFilters(GlobalFilterCollection filters)
            {
                //filters.Add(new HandleErrorAttribute());
            }
        }


    }
}
