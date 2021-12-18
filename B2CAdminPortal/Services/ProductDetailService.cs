using API_Base.Base;
using B2C_Models.Models;
using B2CAdminPortal.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace B2CAdminPortal.Services
{
    public class ProductDetailService : DALBase<ProductDetail>, IProductDetail
    {
        public async Task<ProductDetail> DisablePictures(int Id)
        {
            try
            {
                Current = await _dxcontext.ProductDetails.FirstOrDefaultAsync(x => x.Id == Id && x.IsActive == true);
                if (Current != null)
                {
                    PrimaryKeyValue = Current.Id;
                    Current.IsActive = false;
                }
                Save();
                return Current;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<IEnumerable<ProductDetail>> GetDetailById(int? FK_ProductMaster)
        {
            try
            {
                var obj = await _dxcontext.ProductDetails.Where(x => x.FK_ProductMaster == FK_ProductMaster && x.IsActive == true).ToListAsync();
                return obj;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<ProductDetail>> GetPicturesById(int Id)
        {
            var obj = await _dxcontext.ProductDetails.Where(x => x.FK_ProductMaster == Id && x.IsActive == true).ToListAsync();
            return obj;

        }

        public async Task<ProductDetail> InsertProductDetail(int FK_ProductMaster, string ImageUrl)
        {
            try
            {
                Current = await _dxcontext.ProductDetails.FirstOrDefaultAsync(x => x.ImageUrl == ImageUrl && x.IsActive == true);
                if (Current != null)
                {
                    Current.ModifiedOn = DateTime.Now;

                }
                else
                {
                    New();
                    Current.CreatedOn = DateTime.Now;
                }
                Current.IsActive = true;
                Current.ImageUrl = ImageUrl;
                Current.FK_ProductMaster = FK_ProductMaster;

                Save();
                return Current;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}