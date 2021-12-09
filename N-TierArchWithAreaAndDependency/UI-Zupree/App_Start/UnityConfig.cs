using BLL_Zupree;
using IBL_Zupree;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace UI_Zupree
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<ISupplierRepository,SupplierRepository>();
            container.RegisterType<ICategoryRepository,CategoryRepository>();
            container.RegisterType<IProductRepository,ProductRepository>();
            container.RegisterType<ISalesRepository,SalesRepository>();
            container.RegisterType<IPaymentRepository,PaymentRepository>();
            container.RegisterType<IAccountRepository, AccountRepository>();
            container.RegisterType<IRoleRepository, RoleRepository>();
            container.RegisterType<IUserWiseRoleRepository, UserWiseRoleRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}