using B2C_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace B2CAdminPortal.Interfaces
{
    public interface IOrderMaster
    {
        Task<IEnumerable<OrderMaster>> GetAllOrders();
        Task<string> GetPaymentMode(int OrderMasterId);
        Task<string> GetStatus(int OrderMasterId);
        Task<bool> DisableOrder(int Id);
        Task<bool> PaymentStatusChange(int Id);
        Task<bool> StatusChange(int Id, string status);
        Task<IEnumerable<OrderDetail>> GetOrderDetails(int Id);
        Task<OrderMaster> UpdateOrder(int orderMasterId, int? Qty, decimal price);
        Task<OrderMaster> UpdateOrderMaster(int orderMasterId, int? Qty, decimal price);

    }
}