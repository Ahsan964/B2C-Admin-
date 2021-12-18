using B2C_Models.Models;
using B2CAdminPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2CAdminPortal.Interfaces
{
    public interface IProductMaster
    {
        Task<ProductMaster> GetProductById(long? Id);
        Task<IEnumerable<ProductCategory>> GetProductCategories();
        Task<IEnumerable<ProductBrand>> GetProductBrand();
        Task<IEnumerable<ProductVariant>> GetProductVarient();
        Task<IEnumerable<ProductPackSize>> GetProductPackSize();

        Task<IEnumerable<ProductMaster>> GetProduct();
        Task<bool> DisableProduct(int Id);
        Task<ProductMaster> InsertProduct(ProductsVM productsVM);
        Task<IEnumerable<City>> GetAllCities();
       

    }
}
