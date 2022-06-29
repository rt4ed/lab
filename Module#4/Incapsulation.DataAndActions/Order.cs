using System;
using System.Collections.Generic;
using Incapsulation.DataAndActions.Enums;

namespace Incapsulation.DataAndActions
{
    public class Order
    {
        private List<OrderDetail> _orderDetails;
        public Order(int id, DateTime orderDate, int userId, OrderStatus status)
        {
            Id = id;
            OrderDate = orderDate;
            UserId = userId;
            Status = status;
        }
        public int Id { get; private set; }

        public DateTime OrderDate { get; private set; }

        public int UserId { get; private set; }

        public OrderStatus Status { get; private set; }

        public List<OrderDetail> OrderDetails
        {
            get { return new List<OrderDetail>(_orderDetails); }
        }
    }
}
