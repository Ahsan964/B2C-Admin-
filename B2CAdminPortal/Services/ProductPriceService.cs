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
    public class ProductPriceService : DALBase<ProductPrice>, IProductPrice
    {
        public async Task<ProductPrice> GetPriceById(int? Id)
        {
            try
            {
                var obj = await _dxcontext.ProductPrices.FirstOrDefaultAsync(x => x.IsActive == true && x.FK_ProductMaster == Id);
                return obj;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            throw new NotImplementedException();
        }

        public async Task<ProductPrice> Insert(int FK_ProductMaster, ProductsVM productsVM)
        {
            try
            {
                Current = await _dxcontext.ProductPrices.FirstOrDefaultAsync(x => x.FK_ProductMaster == FK_ProductMaster && x.IsActive == true);
                if(Current != null)
                {
                    PrimaryKeyValue = Current.Id;
                    Current.ModifiedOn = DateTime.Now;
                }
                else
                {
                    New();
                    Current.CreatedOn = DateTime.Now;
                }
                Current.IsActive = true;
                Current.Price = productsVM.Price;
                Current.Discount = productsVM.Discount;
                Current.FK_City = productsVM.FK_City;
                Current.FK_ProductMaster = FK_ProductMaster;

                Save();
                return Current;

            }
            catch(Exception ex)
            {
                throw ex;

            }
           
        }
    }
}