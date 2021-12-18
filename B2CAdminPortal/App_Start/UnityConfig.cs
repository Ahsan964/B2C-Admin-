using B2CAdminPortal.Interfaces;
using B2CAdminPortal.Services;
using System;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace B2CAdminPortal
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            
            container.RegisterType<IOrderMaster, OrderMasterServices>();
            container.RegisterType<IProductMaster, ProductMasterService>();
            container.RegisterType<IOrderDetail, OrderDetailServices>();
            container.RegisterType<IProductCategory, ProductCategoryService>();
            container.RegisterType<IProductDetail, ProductDetailService>();
            container.RegisterType<IProductPrice, ProductPriceService>();


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        public static void RegisterComponents()
        {
            var container = new UnityContainer();


            container.RegisterType<IOrderMaster, OrderMasterServices>();
            container.RegisterType<IProductMaster, ProductMasterService>();
            container.RegisterType<IOrderDetail, OrderDetailServices>();
            container.RegisterType<IProductCategory, ProductCategoryService>();
            container.RegisterType<IProductDetail, ProductDetailService>();
            container.RegisterType<IProductPrice, ProductPriceService>();


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}