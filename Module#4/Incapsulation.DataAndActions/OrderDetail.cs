namespace Incapsulation.DataAndActions
{
	public class OrderDetail
	{
        public OrderDetail(int orderId, int productId, int count, decimal price)
        {
			OrderId = orderId;
			ProductId = productId;
			Count = count;
			Price = price;
        }
		public int OrderId { get; private set; }

		public int ProductId { get; private set; }

		public int Count { get; private set; }

		public decimal Price { get; private set; }
	}
}