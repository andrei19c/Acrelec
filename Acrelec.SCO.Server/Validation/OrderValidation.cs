using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acrelec.SCO.Server.Model;

namespace Acrelec.SCO.Server.Validation
{
    public class OrderValidation : IOrderValidation
    {
        private const string CustomerError = "Customer Error";
        private const string OrderError = "Order Error";

        public Dictionary<string, List<string>> ValidateRequest(InjectOrderRequest injectOrderRequest)
        {
            Dictionary<string, List<string>> validationResult = new Dictionary<string, List<string>>();

            if (injectOrderRequest.Order == null)
                validationResult.AddOrUpdate(OrderError, "Order cannot be null");

            if (injectOrderRequest.Order?.OrderItems.Length == 0)
                validationResult.AddOrUpdate(OrderError, "Order must have at least one item");

            for (int i = 0; i < injectOrderRequest.Order?.OrderItems.Length; i++)
            {
                if (injectOrderRequest.Order.OrderItems[i].Qty <= 0)
                    validationResult.AddOrUpdate(OrderError, $"Qty for order {i} cannot be smaller than 0");

                if (string.IsNullOrWhiteSpace(injectOrderRequest.Order.OrderItems[i].ItemCode))
                    validationResult.AddOrUpdate(OrderError, $"Item code for order {i} must have a value");
            }

            if (injectOrderRequest.Customer == null)
                validationResult.AddOrUpdate(CustomerError, "Customer cannot be null");

            if (string.IsNullOrWhiteSpace(injectOrderRequest.Customer?.Address) || string.IsNullOrWhiteSpace(injectOrderRequest.Customer?.Firstname))
                validationResult.AddOrUpdate(CustomerError, "Customer details are missing");

            return validationResult;
        }
    }
}
