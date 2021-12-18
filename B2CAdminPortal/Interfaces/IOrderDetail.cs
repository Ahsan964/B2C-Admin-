using B2C_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2CAdminPortal.Interfaces
{
    public interface IOrderDetail
    {
        Task<OrderDetail> DisableOrderDetail(int Id);
        Task<OrderDetail> GetOrderDetailById(long Id);
        Task<OrderDetail> UpdateOrderDetail(OrderDetail orderDetail);
    }
}
