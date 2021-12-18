using B2C_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace B2CAdminPortal.Interfaces
{
    public interface IProductDetail
    {
        Task<ProductDetail> InsertProductDetail(int FK_ProductMaster, string ImageUrl);
        Task<IEnumerable<ProductDetail>> GetDetailById(int? FK_ProductMaster);

        Task<IEnumerable<ProductDetail>> GetPicturesById(int Id);
        Task<ProductDetail> DisablePictures(int Id);
    }
}