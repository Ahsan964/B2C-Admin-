using API_Base.Base;
using B2C_Models.Models;
using B2CAdminPortal.Interfaces;
using B2CAdminPortal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace B2CAdminPortal.Services
{
    public class ProductMasterService : DALBase<ProductMaster>, IProductMaster
    {
        public async Task<IEnumerable<ProductCategory>> GetProductCategories()
        {
            try
            {
                //_dxcontext.Configuration.LazyLoadingEnabled = false;
                var obj = await _dxcontext.ProductCategories.Where(x => x.IsActive == true).ToListAsync();
                return obj;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        public async Task<ProductMaster> GetProductById(long? Id)
        {
            try
            {
                var obj = await _dxcontext.ProductMasters
                    
                    .Include(x => x.ProductPrices)
                    .Include(x => x.ProductDetails)
                    .Include(x => x.ProductPackSize)
                    .Include(x=>x.ProductVariant)
                    .Include(x=>x.ProductCategory)
                    .Include(x=>x.ProductBrand)
                    .Where(x => x.IsActive == true)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == Id);

                return obj;

            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public async Task<IEnumerable<ProductMaster>> GetProduct()
        {
            try
            {
                //_dxcontext.Configuration.LazyLoadingEnabled = false;
                var obj = await _dxcontext.ProductMasters//.Where(x => x.IsFeatured && x.IsActive == true)
                    .Include(x => x.ProductPrices)
                   .Include(x => x.ProductDetails)
                   .Where(x=>x.IsActive==true)
                   .AsNoTracking()
                    .OrderByDescending(a => a.Id)
                    .ToListAsync();//  GetAll();
                return obj;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        public async Task<bool> DisableProduct(int Id)
        {
            try
            {
                Current = await _dxcontext.ProductMasters.FirstOrDefaultAsync(o => o.Id == Id && o.IsActive == true);
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

        public async Task<IEnumerable<ProductBrand>> GetProductBrand()
        {
            try
            {
                //_dxcontext.Configuration.LazyLoadingEnabled = false;
                var obj = await _dxcontext.ProductBrands.Where(x => x.IsActive == true).ToListAsync();
                return obj;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public async Task<IEnumerable<ProductVariant>> GetProductVarient()
        {
            try
            {
                //_dxcontext.Configuration.LazyLoadingEnabled = false;
                var obj = await _dxcontext.ProductVariants.Where(x => x.IsActive == true).ToListAsync();
                return obj;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public async Task<IEnumerable<ProductPackSize>> GetProductPackSize()
        {
            try
            {
                //_dxcontext.Configuration.LazyLoadingEnabled = false;
                var obj = await _dxcontext.ProductPackSizes.Where(x => x.IsActive == true).ToListAsync();
                return obj;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public async Task<ProductMaster> InsertProduct(ProductsVM productsVM)
        {
            Current = await _dxcontext.ProductMasters.FirstOrDefaultAsync(x => x.Id == productsVM.Id && x.IsActive == true);
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
            Current.Name = productsVM.Name;
            Current.MasterImageUrl = productsVM.ImageURL;
            Current.ShortDescription = productsVM.ShortDescription;
            Current.LongDescription = productsVM.LongDescription;
            Current.FK_ProductBrand = Convert.ToInt32(productsVM.FK_Brand);
            Current.FK_ProductCategory = Convert.ToInt32(productsVM.FK_Category);
            Current.FK_ProductVeriant = Convert.ToInt32(productsVM.FK_Varient);
            Current.FK_ProductPackSize = Convert.ToInt32(productsVM.FK_PackSize);
            Current.IsFeatured = productsVM.IsFeatured;
            Current.IsActive = true;

            Save();
            return Current;
        }
        public async Task<IEnumerable<City>> GetAllCities()
        {
            try
            {
                var obj = await _dxcontext.Cities.Where(x => x.IsActive == true).ToListAsync();
                return obj;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
     
    }
}