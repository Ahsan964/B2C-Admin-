using B2C_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace B2CAdminPortal.Models
{
    public class ProductsVM
    {
        public int Id { get; set; }
        public int totalProduct { get; set; }

        public string Name { get; set; }
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
        //public string ImageUrl { get; set; }
        public string MasterImageUrl { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public decimal? DiscountedAmount { get; set; }

        public string UOM { get; set; }
        public SingleProduct product { get; set; }
        public decimal? TotalRating { get; set; }
        public decimal? AvgRating { get; set; }
        public int TotalRatingCount { get; set; }
        public string FK_Category { get; set; }
        public string FK_Brand { get; set; }
        public string FK_PackSize { get; set; }
        public string FK_Varient { get; set; }
        public bool IsFeatured { get; set; }
        public int FK_City { get; set; }
        public string[] ProductPicPath { get; set; }
        public HttpPostedFileWrapper[] PicturePath { get; set; }
        public List<LP_Products_Picture> ProductPictureList { get; set; }
        
        public string ImageURL { get;  set; }
        public List<ProductsVM> productsVMs { get; set; }

        public static implicit operator ProductsVM(ProductCategory v)
        {
            throw new NotImplementedException();
        }
    }

    public class LP_Products_Picture
    {

        public int ID { get; set; }
        public int ProductID { get; set; }
        public string PicturePath { get; set; }

    }
    public class ProductsCommentVM
    {
        public int? Id { get; set; }
        public int TotalRatingCount { get; set; }
        public decimal? AvgRating { get; set; }



    }
    public class SingleProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
        public List<string> ImageUrl { get; set; }
        public string MasterImageUrl { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
    }
}