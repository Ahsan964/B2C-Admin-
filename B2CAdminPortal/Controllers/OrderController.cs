using API_Base.Base;
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
    public class OrderController : BaseController
    {
        private readonly IOrderMaster _order = null;
        private readonly IProductMaster _IProductMaster = null;
        private readonly IOrderDetail _ordersDetail = null;

        public OrderController(IOrderMaster order, IProductMaster productMaster, IOrderDetail orderDetail)
        {
            _order = order;
            _IProductMaster = productMaster;
            _ordersDetail = orderDetail;
        }

        [HttpGet]
        public ActionResult ViewOrders()
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

        public async Task<ActionResult> OrderTable()
        {
            try
            {
                List<OrderVM> orderVMs = new List<OrderVM>();
                var obj = await _order.GetAllOrders();
                foreach (var item in obj)
                {
                    OrderVM orderVM = new OrderVM();
                    orderVM.Id = item.Id;
                    orderVM.TotalQuantity = item.TotalQuantity;
                    orderVM.TotalPrice = item.TotalPrice;
                    orderVM.ShippingAddress = item.ShippingAddress;
                    orderVM.BillingAddress = item.BillingAddress;
                    orderVM.PhoneNo = item.PhoneNo;
                    orderVM.EmailId = item.EmailId;
                    orderVM.City = item.City;
                    orderVM.Country = item.Country;
                    orderVM.ConversionRate = (decimal)item.ConversionRate;
                    orderVM.Currency = item.Currency;
                    orderVM.Status = item.Status;
                    orderVM.PaymentMode = item.PaymentMode;
                    orderVM.PaymentStatus = item.PaymentStatus;
                    orderVM.Status = item.Status;
                    orderVM.PaymentMode = item.PaymentMode;
                    orderVMs.Add(orderVM);

                }


                return PartialView(orderVMs);
            }
            catch(Exception ex)
            {
                return BadResponse(ex);
            }
        }

        public async Task<ActionResult> GetOrderDetails(int Id)
        {
            try
            {
                List<OrderVM> orderVMs = new List<OrderVM>();

                var obj = await _order.GetOrderDetails(Id);
                foreach(var item in obj)
                {
                    var productmasetr = await _IProductMaster.GetProductById(item.FK_ProductMaster);
                    string name = productmasetr.Name;
                    string MasterImageUrl = productmasetr.MasterImageUrl;
                    var price = productmasetr.ProductPrices.Select(x => x.Price).FirstOrDefault();
                    var discount = productmasetr.ProductPrices.Select(x => x.Discount).FirstOrDefault();
                    var actualprice = Math.Round(((decimal)(price * item.Quantity)), 2);
                    var discountedprice = Math.Round(Convert.ToDecimal((price * item.Quantity) * (1 - (discount / 100))), 2);
                    var totalDiscountAmount = Math.Round(((decimal)(price * item.Quantity ) - discountedprice), 2);

                    OrderVM orderVM = new OrderVM();
                    orderVM.MasterImageUrl = MasterImageUrl;
                    orderVM.Name = name;
                    orderVM.Discount = discount;
                    orderVM.Price = price;
                    orderVM.Quantity = item.Quantity;
                    orderVM.SubTotalPrice = actualprice;
                    orderVM.DiscountAmount = discountedprice;
                    orderVM.TotalPrice = totalDiscountAmount;
                    orderVM.Date = item.CreatedOn.ToString();
                    orderVM.FK_ProductMaster = item.FK_ProductMaster;
                    
                    orderVMs.Add(orderVM);
                }
                //return Json(new { data = orderVMs, msg = "Success", success = true, statuscode = 200 }, JsonRequestBehavior.AllowGet);
                return PartialView(orderVMs);
            }
            catch (Exception ex)
            {
                return BadResponse(ex);
            }
        }
        public async Task<ActionResult> DisableOrder(int Id)
        {
            try
            {
                var obj = await _order.DisableOrder(Id);
                return Json(new { data = obj, msg = "Success", success = true, statuscode = 200 }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = ex, msg = "Failed", success = false, statuscode = 400 }, JsonRequestBehavior.AllowGet);
              
            }
        }

        [HttpPost]
        public async Task<ActionResult> PaymentStatusChange(int Id)
        {
            try
            {
                var obj = await _order.PaymentStatusChange(Id);
                return Json(new { data = obj, msg = "Success", success = true, statuscode = 200 }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = ex, msg = "Failed", success = false, statuscode = 400 }, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        public async Task<ActionResult> StatusChange(int Id, string status)
        {
            try
            {
                var obj = await _order.StatusChange(Id,status);
                return Json(new { data = obj, msg = "Success", success = true, statuscode = 200 }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { data = ex, msg = "Failed", success = false, statuscode = 400 }, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpGet]
        public async Task<ActionResult> UpdateOrder(int Id)
        {
            try
            {
                List<OrderVM> orderVMs = new List<OrderVM>();

                var obj = await _order.GetOrderDetails(Id);
                foreach (var item in obj)
                {
                    var productmasetr = await _IProductMaster.GetProductById(item.FK_ProductMaster);
                    string name = productmasetr.Name;
                    string MasterImageUrl = productmasetr.MasterImageUrl;
                    var price = productmasetr.ProductPrices.Select(x => x.Price).FirstOrDefault();
                    var discount = productmasetr.ProductPrices.Select(x => x.Discount).FirstOrDefault();
                    var actualprice = Math.Round(((decimal)(price * item.Quantity)), 2);
                    var discountedprice = Math.Round(Convert.ToDecimal((price * item.Quantity) * (1 - (discount / 100))), 2);
                    var totalDiscountAmount = Math.Round(((decimal)(price * item.Quantity) - discountedprice), 2);
                    var Mode = await _order.GetPaymentMode(item.FK_OrderMaster);
                    var status = await _order.GetStatus(item.FK_OrderMaster);

                    OrderVM orderVM = new OrderVM();
                    orderVM.Id = item.Id;
                    orderVM.MasterImageUrl = MasterImageUrl;
                    orderVM.Name = name;
                    orderVM.Discount = discount;
                    orderVM.Price = price;
                    orderVM.Quantity = item.Quantity;
                    orderVM.SubTotalPrice = actualprice;
                    orderVM.DiscountAmount = discountedprice;
                    orderVM.TotalPrice = totalDiscountAmount;
                    orderVM.Date = item.CreatedOn.ToString();
                    orderVM.FK_ProductMaster = item.FK_ProductMaster;
                    orderVM.FK_OrderMaster = item.FK_OrderMaster;
                    orderVM.CreatedOn = item.CreatedOn;
                    orderVM.PaymentMode = Mode;
                    orderVM.Status = status;
                    orderVMs.Add(orderVM);

                }
                return View(orderVMs);
            }
            catch(Exception ex)
            {
                return BadResponse(ex);
            }
        }

        public async Task<ActionResult> DisableOrderDetail(int Id)
        {
            try
            {
                // Is Active false
                var obj = await _ordersDetail.DisableOrderDetail(Id);

                // Update Order Master
                if (obj != null)
                {
                    var result = await _order.UpdateOrderMaster(obj.FK_OrderMaster, obj.Quantity, (decimal)obj.TotalPrice);
                    return Json(new { data = result, msg = "Success", success = true, statuscode = 200 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { data = "", msg = "Failed", success = false, statuscode = 400 }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { data = ex, msg = "Failed", success = false, statuscode = 400 }, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        public async Task<JsonResult> UpdateOrderList(List<int> orderQuantities, List<int> orderids)
        {
            try
            {
                string msg = string.Empty;
                decimal TPrice = 0.00m;
                int TQty = 0;
                var orderMasterId = 0;

                for (int i = 0; i < orderids.Count(); i++)
                {
                    var orderproducts = await _ordersDetail.GetOrderDetailById(orderids[i]);
                    if (orderproducts != null)
                    {
                        orderproducts.Quantity = orderQuantities[i];
                        var productmasetr = await _IProductMaster.GetProductById(orderproducts.FK_ProductMaster);
                        var discount = productmasetr.ProductPrices.Select(x => x.Discount).FirstOrDefault();
                        var price = productmasetr.ProductPrices.Select(x => x.Price).FirstOrDefault();
                        orderproducts.DiscountedPrice = Math.Round(Convert.ToDecimal(price * (1 - (discount / 100))), 2);
                        orderproducts.TotalPrice = Math.Round(Convert.ToDecimal((price * orderproducts.Quantity) * (1 - (discount / 100))) , 2);
                        var obj = await _ordersDetail.UpdateOrderDetail(orderproducts);
                        TPrice = (decimal)(TPrice + obj.TotalPrice);
                        TQty = TQty + (int)obj.Quantity;
                        orderMasterId = obj.FK_OrderMaster;
                    }
                }
                
                 var result = await _order.UpdateOrder(orderMasterId, TQty, TPrice);
                
                msg =  "Order Updated Successfully !" ;
                return Json(new { data = "", msg = msg, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
               
                return Json(new { data = ex, msg = "Failed", success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }


}