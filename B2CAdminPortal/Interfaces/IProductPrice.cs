using B2C_Models.Models;
using B2CAdminPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace B2CAdminPortal.Interfaces
{
    public interface IProductPrice
    {
        Task<ProductPrice> Insert(int FK_ProductMaster, ProductsVM productsVM);

        Task<ProductPrice> GetPriceById(int? Id);
    }
}