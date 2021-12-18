using B2C_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2CAdminPortal.Interfaces
{
    public interface IProductCategory
    {
        Task<bool> DisableProductCategory(int Id);
        Task<ProductCategory> GetById(int? Id);
        Task<ProductCategory> Insert(ProductCategory productCategory);
        
    }
}
