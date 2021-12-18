using API_Base.Base;
using B2C_Models.Models;
using B2CAdminPortal.Interfaces;
using B2CAdminPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace B2CAdminPortal.Controllers
{
    public class ProductController : BaseController
    {
        #region Instances
        private readonly IProductMaster _IProductMaster = null;
        private readonly IProductDetail _IProductDetail = null;
        private readonly IProductCategory _IProductCategory = null;
        private readonly IProductPrice _IProductPrice = null;

        public ProductController(IProductPrice productPrice, IProductMaster productMaster, IProductCategory productCategory, IProductDetail productDetail)
        {
            _IProductMaster = productMaster;
            _IProductCategory = productCategory;
            _IProductDetail = productDetail;
            _IProductPrice = productPrice;
        }
        #endregion
       
        #region Product Categories
        public async Task<ActionResult> ViewProductCategories()
        {
            try
            {
                ProductsVM productsVM = new ProductsVM();
                List<ProductsVM> productsVMs = new List<ProductsVM>();
                var obj = await _IProductMaster.GetProductCategories();
                if(obj != null && obj.Count() > 0)
                {
                    foreach (var item in obj)
                    {
                        productsVM = new ProductsVM
                        {
                            Id = item.Id,
                            Name = item.Name,
                        };
                        productsVMs.Add(productsVM);
                    }
                }
                productsVM.productsVMs = productsVMs;
                
               
                return View(productsVM);

            }
            catch(Exception ex)
            {
                return BadResponse(ex);
            }
        }
        public async Task<ActionResult> AddNewProductCategory(int? Id)
        {
            try
            {
                ProductsVM productsVM = new ProductsVM();
                if(Id !=0 && Id > 0)
                {
                    productsVM.Id = (int)Id;
                    var obj = await _IProductCategory.GetById(Id);
                    productsVM.Id = obj.Id;
                    productsVM.Name = obj.Name;
                }
                return PartialView("_AddNewCategory", productsVM);
            }
            catch(Exception ex)
            {
                return BadResponse(ex);
            }
        }
        public async Task<JsonResult> AddUpdateCategory(ProductCategory productCategory)
        {
            try
            {
                if(productCategory.Id > 0)
                {
                    var obj = await _IProductCategory.Insert(productCategory);
                    return Json(new { data = obj, msg = "Success", success = true, statuscode = 200 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var obj = await _IProductCategory.Insert(productCategory);
                    return Json(new { data = obj, msg = "Success", success = true, statuscode = 200 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json(new { data = ex.Message, msg = "Success", success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> DisableProductCategory(int Id)
        {
            try
            {
                var obj = await _IProductCategory.DisableProductCategory(Id);
                return Json(new { data = obj, msg = "Success", success = true, statuscode = 200 }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = ex, msg = "Failed", success = false, statuscode = 400 }, JsonRequestBehavior.AllowGet);

            }
        }

        #endregion

        #region Product

        [HttpGet]
        public async Task<ActionResult> ViewProducts()
        {
            try
            {
                return View();
            }
            catch(Exception ex)
            {
                return BadResponse(ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult> ProductTable()
        {
            try
            {
                ProductsVM productsVM = new ProductsVM();
                List<ProductsVM> productsVMs = new List<ProductsVM>();
                var obj = await _IProductMaster.GetProduct();
                if(obj != null && obj.Count() > 0)
                {
                    foreach(var item in obj)
                    {
                        var productobj = await _IProductMaster.GetProductById(item.Id);
                        var discount = productobj.ProductPrices.Select(x => x.Discount).FirstOrDefault();
                        var price = productobj.ProductPrices.Select(x => x.Price).FirstOrDefault();
                        var discountedprice = Math.Round(Convert.ToDecimal(price * (1 - (discount / 100))), 2);
                        var packsize = productobj.ProductPackSize.UOM;
                        var Brand = productobj.ProductBrand.Name;
                        var Category = productobj.ProductCategory.Name;
                        var varient = productobj.ProductVariant.Name;
                        productsVM = new ProductsVM
                        {
                            Id = item.Id,
                            Name = item.Name,
                            MasterImageUrl = item.MasterImageUrl,
                            FK_Category = Category,
                            FK_Brand = Brand,
                            FK_PackSize = packsize,
                            FK_Varient = varient,
                            Price = price,
                            Discount = discount,
                            DiscountedAmount = discountedprice,

                        };
                        productsVMs.Add(productsVM);
                    }
                }

                productsVM.productsVMs = productsVMs;
                return PartialView(productsVM);
            }
            catch(Exception ex)
            {
                return BadResponse(ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult> AddNewProduct(int? Id)
        {
            try
            {
                #region Dropdowns
                ProductsVM productsVM = new ProductsVM();
                ProductCategory productCategory = new ProductCategory();
                ProductBrand productBrand = new ProductBrand();
                ProductPackSize productPackSize = new ProductPackSize();
                ProductVariant productVariant = new ProductVariant();
                City city = new City();

                List<LP_Products_Picture> LSTProductsPicture = new List<LP_Products_Picture>();
                List<ProductCategory> productCategories = new List<ProductCategory>();
                List<ProductBrand> productBrands = new List<ProductBrand>();
                List<ProductVariant> productVariants = new List<ProductVariant>();
                List<ProductPackSize> productPackSizes = new List<ProductPackSize>();
                List<City> cities = new List<City>();


                var category = await _IProductMaster.GetProductCategories();
                var Brand = await _IProductMaster.GetProductBrand();
                var varient = await _IProductMaster.GetProductVarient();
                var Packsize = await _IProductMaster.GetProductPackSize();
                var AllCities = await _IProductMaster.GetAllCities();

                foreach (var item in AllCities)
                {
                    city = new City
                    {
                        Id = item.Id,
                        Name = item.Name,
                    };
                    cities.Add(city);
                }
                ViewBag.cities = cities;

                foreach (var item in category)
                {
                    productCategory = new ProductCategory
                    {
                        Id = item.Id,
                        Name = item.Name,
                    };
                    productCategories.Add(productCategory);
                }
                ViewBag.ProductCat = productCategories;

                foreach (var item in Brand)
                {
                    productBrand = new ProductBrand
                    {
                        Id = item.Id,
                        Name = item.Name,
                    };
                    productBrands.Add(productBrand);
                }
                ViewBag.ProductBrand = productBrands;

                foreach (var item in varient)
                {
                    productVariant = new ProductVariant
                    {
                        Id = item.Id,
                        Name = item.Name,
                    };
                    productVariants.Add(productVariant);
                }
                ViewBag.ProductVar = productVariants;

                foreach (var item in Packsize)
                {
                    productPackSize = new ProductPackSize
                    {
                        Id = item.Id,
                        UOM = item.UOM,
                    };
                    productPackSizes.Add(productPackSize);
                }
                ViewBag.ProductPack = productPackSizes;
                #endregion
                // Edit Page
                if (Id != 0 && Id > 0)
                {
                    // Getting Product Master data
                    productsVM.Id = (int)Id;
                    var obj = await _IProductMaster.GetProductById(Id);
                    productsVM.Id = obj.Id;
                    productsVM.Name = obj.Name;
                    productsVM.ImageURL = obj.MasterImageUrl;
                    productsVM.ShortDescription = obj.ShortDescription;
                    productsVM.LongDescription = obj.LongDescription;
                    productsVM.FK_Category = obj.FK_ProductCategory.ToString();
                    productsVM.FK_Brand = obj.FK_ProductBrand.ToString();
                    productsVM.FK_PackSize = obj.FK_ProductPackSize.ToString();
                    productsVM.FK_Varient = obj.FK_ProductVeriant.ToString();
                    productsVM.IsFeatured = obj.IsFeatured;

                    // Getting Product Detail Data
                    var detail = await _IProductDetail.GetDetailById(Id);

                    foreach (var dr in detail)
                    {
                        LP_Products_Picture objprdctpic = new LP_Products_Picture();

                        objprdctpic.PicturePath = dr.ImageUrl.ToString();
                        LSTProductsPicture.Add(objprdctpic);

                    }
                    ViewBag.ProductPics = LSTProductsPicture;

                    // Getting Price data
                    var prices = await _IProductPrice.GetPriceById(Id);
                    productsVM.FK_City = prices.FK_City;
                    productsVM.Price = prices.Price;
                    productsVM.Discount = prices.Discount;
                }


                return PartialView("_AddNewProduct", productsVM);
            }
            catch (Exception ex)
            {
                return BadResponse(ex);
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddUpdateProduct(ProductsVM formdata)
        {
            try
            {
                string[] picturepath;
                // Update
                if (formdata.Id > 0)
                {
                    // List of old pictures
                    List<LP_Products_Picture> toFindDeletedImages = formdata.ProductPictureList;

                    // Find if any image is deleted
                    if (toFindDeletedImages != null)
                    {
                        List<LP_Products_Picture> ExistingImages = new List<LP_Products_Picture>();
                        var ProdctPic = await _IProductDetail.GetPicturesById(formdata.Id);

                        foreach (var dr in ProdctPic)
                        {
                            LP_Products_Picture objprflepicpth = new LP_Products_Picture();
                            objprflepicpth.PicturePath = dr.ImageUrl.ToString();
                            objprflepicpth.ID = Convert.ToInt16(dr.Id.ToString());
                            ExistingImages.Add(objprflepicpth);
                        }

                        List<int> imagestoDelete = new List<int>();
                        for (int i = 0; i < ExistingImages.Count; i++)
                        {
                            var t = toFindDeletedImages
                                .Any(x => x.PicturePath == ExistingImages[i].PicturePath);
                            if (t == false)
                            {
                                imagestoDelete.Add(ExistingImages[i].ID);
                                var deleted = await _IProductDetail.DisablePictures(ExistingImages[i].ID);
                            }
                        }

                    }

                    // Update Product Master
                    var obj = await _IProductMaster.InsertProduct(formdata);
                    var FK_ProductMaster = obj.Id;
                    // Insert Product Prices
                    var priceInserted = await _IProductPrice.Insert(FK_ProductMaster, formdata);

                    // Newly Added Pictures Update Product Detail
                    if (formdata.PicturePath != null)
                    {
                        picturepath = SavePicture(formdata.PicturePath);
                        formdata.ProductPictureList = new List<LP_Products_Picture>();
                        for (int i = 0; i < picturepath.Length; i++)
                        {
                            LP_Products_Picture objprflepicpth = new LP_Products_Picture();
                            objprflepicpth.PicturePath = picturepath[i].ToString();
                            formdata.ProductPictureList.Add(objprflepicpth);
                        }
                        foreach (var item in formdata.ProductPictureList)
                        {
                            var ImageUrl = item.PicturePath;
                            var result = await _IProductDetail.InsertProductDetail(FK_ProductMaster, ImageUrl);
                        }
                    }

                }



                // New Insert
                else
                {
                    if (formdata.PicturePath != null)
                    {

                        picturepath = SavePicture(formdata.PicturePath);

                        formdata.ProductPictureList = new List<LP_Products_Picture>();
                        for (int i = 0; i < picturepath.Length; i++)
                        {
                            LP_Products_Picture objprflepicpth = new LP_Products_Picture();
                            objprflepicpth.PicturePath = picturepath[i].ToString();
                            formdata.ProductPictureList.Add(objprflepicpth);
                        }

                    }

                    // Insert Product Master
                    formdata.MasterImageUrl = formdata.ImageURL;
                    var obj = await _IProductMaster.InsertProduct(formdata);
                    var FK_ProductMaster = obj.Id;

                    // Insert Product Prices
                    var priceInserted = await _IProductPrice.Insert(FK_ProductMaster, formdata);

                    // Insert Product Details
                    if (formdata.ProductPictureList != null)
                    {
                        foreach(var item in formdata.ProductPictureList)
                        {
                            
                            var ImageUrl = item.PicturePath;
                            var result = await _IProductDetail.InsertProductDetail(FK_ProductMaster,ImageUrl);
                        }
                    }
                }
                return Json(new { data = "", msg = "Success", success = true, statuscode = 200, count = 0 }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = ex.Message, msg = "Failed", success = false, statuscode = 400, count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        
        public async Task<ActionResult> DisableProduct(int Id)
        {
            try
            {
                var obj = await _IProductMaster.DisableProduct(Id);
                return Json(new { data = obj, msg = "Success", success = true, statuscode = 200 }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = ex, msg = "Failed", success = false, statuscode = 400 }, JsonRequestBehavior.AllowGet);

            }
        }
        #endregion
    }
}