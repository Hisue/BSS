using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BSSUnitTest.Orders
{
    /**
     * IOrderService servisas turi metoda GetAvailableIdAsync, kuris patikrina ar duotas orderId yra DB,
     * jei yra sugeneruoja nauja unikalu pagal duoto id patterna.
     * Reikia order servisui parasyti unit testus.
     */
    public interface IOrderService
    {
        Task<AvailableId> GetAvailableIdAsync(string orderId);
    }

    public interface IOrderRepository
    {
        Task<Order> GetOrderAsync(string orderId);
        Task<IList<Order>> FindOrdersWithSimilarIdAsync(string orderIdStartsWith);
    }

    public class Order
    {
        public string OrderId { get; private set; }

        public Order(string orderId)
        {
            OrderId = orderId;
        }
    }

    public class AvailableId
    {
        public bool IsAvailable { get; set; }
        public string Suggest { get; set; }
    }

    public class OrderRepository : IOrderRepository
    {
        public async Task<Order> GetOrderAsync(string orderId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<Order>> FindOrdersWithSimilarIdAsync(string orderIdStartsWith)
        {
            throw new System.NotImplementedException();
        }
    }

    public class OrderService : IOrderService
    {
        const string Pattern = @"([a-zA-Z])+([\s\-:#=]*)(\d*)";

        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<AvailableId> GetAvailableIdAsync(string orderId)
        {
            var order = await _repository.GetOrderAsync(orderId).ConfigureAwait(false);

            if (order == null)
                return new AvailableId { IsAvailable = true };

            var match = Regex.Match(orderId, Pattern);

            var orderIdStartsWith = match.Groups[1].Captures[0].Value + match.Groups[2].Captures.FirstOrDefault()?.Value;
            var list = await _repository.FindOrdersWithSimilarIdAsync(orderIdStartsWith).ConfigureAwait(false);

            var lastOrder = list.OrderBy(l => l.OrderId).Last();

            var match2 = Regex.Match(lastOrder.OrderId, Pattern);
            var numberString = match2.Groups[3].Captures.FirstOrDefault()?.Value;
            if (!int.TryParse(numberString, out var number))
            {
                number = 0;
            }

            var format = "";
            if (!string.IsNullOrEmpty(numberString))
            {
                format = $"D{numberString.Length}";
            }

            number++;
            return new AvailableId
            {
                IsAvailable = false,
                Suggest = orderIdStartsWith + number.ToString(format)
            };
        }
    }
}