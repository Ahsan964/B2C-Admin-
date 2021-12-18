using API_Base.Base;
using B2C_Models.Models;
using B2CAdminPortal.Interfaces;
using B2CAdminPortal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace B2CAdminPortal.Services
{
    public class ProductCategoryService : DALBase<ProductCategory>, IProductCategory
    {
        public async Task<bool> DisableProductCategory(int Id)
        {
            try
            {
                Current = await _dxcontext.ProductCategories.FirstOrDefaultAsync(o => o.Id == Id);
                if (Current != null)
                {
                    PrimaryKeyValue = Current.Id;
                    Current.IsActive = false;
                }

                Save();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductCategory> GetById(int? Id)
        {
            var res = await _dxcontext.ProductCategories.FirstOrDefaultAsync(x => x.Id == Id && x.IsActive == true);
            return res;
        }

        public async Task<ProductCategory> Insert(ProductCategory productCategory)
        {
            Current = await _dxcontext.ProductCategories.FirstOrDefaultAsync(x => x.Id == productCategory.Id && x.IsActive == true);
            if(Current != null)
            {
                PrimaryKeyValue = Current.Id;
            }
            else
            {
                New();
                Current.IsActive = true;
            }
            Current.Name = productCategory.Name;
            Save();
            return Current;

        }
    }
}