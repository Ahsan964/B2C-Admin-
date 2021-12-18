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
    public class OrderDetailServices : DALBase<OrderDetail>, IOrderDetail
    {
        public async Task<OrderDetail> DisableOrderDetail(int Id)
        {
            try
            {
                Current = await _dxcontext.OrderDetails.FirstOrDefaultAsync(o => o.Id == Id);
                if (Current != null)
                {
                    PrimaryKeyValue = Current.Id;
                    Current.IsActive = false;
                }

                Save();
                return Current;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OrderDetail> GetOrderDetailById(long Id)
        {
            Current = await _dxcontext.OrderDetails.FirstOrDefaultAsync(o => o.Id == Id && o.IsActive == true);
           
            return Current;
            
        }

        public async Task<OrderDetail> UpdateOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                Current = await _dxcontext.OrderDetails.FirstOrDefaultAsync(x => x.Id == orderDetail.Id && x.IsActive == true);

                if(Current != null)
                {
                    PrimaryKeyValue = Current.Id;
                    Current.Quantity = orderDetail.Quantity;
                    Current.TotalPrice = orderDetail.TotalPrice;
                    Current.DiscountedPrice = orderDetail.DiscountedPrice;
                }

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