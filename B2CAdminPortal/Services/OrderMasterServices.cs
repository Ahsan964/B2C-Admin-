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
    public class OrderMasterServices : DALBase<OrderMaster>, IOrderMaster
    {
        public async Task<IEnumerable<OrderMaster>> GetAllOrders()
        {
            try
            {
                var result = await _dxcontext.OrderMasters.Where(x => x.IsActive == true).OrderByDescending(x => x.Id).ToListAsync();
                return result;

            }
            catch(Exception ex)
            {
                throw ex;
            }

            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetails(int Id)
        {
            try
            {
                var result = await _dxcontext.OrderDetails.Where(x => x.FK_OrderMaster == Id && x.IsActive == true).ToListAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            //throw new NotImplementedException();
        }

        public async Task<bool> DisableOrder(int Id)
        {
            try
            {
                Current = await _dxcontext.OrderMasters.FirstOrDefaultAsync(o => o.Id == Id);
                if (Current != null)
                {
                    PrimaryKeyValue = Current.Id;
                    Current.Status = "Cancelled"; 
                }

                Save();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> PaymentStatusChange(int Id)
        {
            try
            {
                Current = await _dxcontext.OrderMasters.FirstOrDefaultAsync(o => o.Id == Id);
                if (Current != null)
                {
                    PrimaryKeyValue = Current.Id;
                    if(Current.PaymentStatus == true)
                    {
                        Current.PaymentStatus = false;
                    }
                    else
                    {
                        Current.PaymentStatus = true;
                    }
                    
                }

                Save();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> StatusChange(int Id, string status)
        {
            try
            {
                Current = await _dxcontext.OrderMasters.FirstOrDefaultAsync(o => o.Id == Id);
                if (Current != null)
                {
                    PrimaryKeyValue = Current.Id;
                    Current.Status = status;
                }

                Save();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OrderMaster> UpdateOrder(int orderMasterId, int? Qty, decimal price)
        {
            try
            {
                Current = await _dxcontext.OrderMasters.FirstOrDefaultAsync(o => o.Id == orderMasterId);
                if (Current != null)
                {
                    PrimaryKeyValue = Current.Id;
                    Current.TotalQuantity = Qty;
                    Current.TotalPrice = price;
                   
                }
                Save();
                return Current;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OrderMaster> UpdateOrderMaster(int orderMasterId, int? Qty, decimal price)
        {
            try
            {
                Current = await _dxcontext.OrderMasters.FirstOrDefaultAsync(o => o.Id == orderMasterId);
                if (Current != null)
                {
                    PrimaryKeyValue = Current.Id;
                    Current.TotalQuantity = Current.TotalQuantity - Qty;
                    Current.TotalPrice = Current.TotalPrice - price;

                    if(Current.TotalQuantity == 0)
                    {
                        Current.Status = "Cancelled";
                    }
                }
                Save();
                return Current;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetPaymentMode(int OrderMasterId)
        {
            try
            {
                Current = await _dxcontext.OrderMasters.FirstOrDefaultAsync(x => x.Id == OrderMasterId);
                var mode = Current.PaymentMode;
                return mode;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        public async Task<string> GetStatus(int OrderMasterId)
        {
            try
            {
                Current = await _dxcontext.OrderMasters.FirstOrDefaultAsync(x => x.Id == OrderMasterId);
                var status = Current.Status;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}