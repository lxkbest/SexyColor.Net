using SexyColor.BusinessComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SexyColor.Web
{
    public class OrderDetailsModel
    {
        private OrderInfo orderInfo;
        private IEnumerable<OrderLine> orderLines;
        private UserAddress userAddress;

        public OrderDetailsModel(OrderInfo orderInfo, IEnumerable<OrderLine> orderLines, UserAddress userAddress)
        {
            this.OrderInfo = orderInfo ?? OrderInfo.New();
            this.OrderLines = orderLines ?? new List<OrderLine>();
            this.UserAddress = userAddress ?? UserAddress.New();
        }

        public OrderInfo OrderInfo { get => orderInfo; set => orderInfo = value; }
        public IEnumerable<OrderLine> OrderLines { get => orderLines; set => orderLines = value; }
        public UserAddress UserAddress { get => userAddress; set => userAddress = value; }
    }
}
