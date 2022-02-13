using Acrelec.SCO.Core.Interfaces;
using Acrelec.SCO.Core.Model.RestExchangedMessages;
using Acrelec.SCO.Core.Services;
using Acrelec.SCO.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrelec.SCO.Core.Managers
{
    public class OrderManager : IOrderManager
    {
        private readonly IScoHttpClient _scoHttpClient;

        /// <summary>
        /// constructor
        /// </summary>
        public OrderManager(IScoHttpClient scoHttpClient)
        {
            _scoHttpClient = scoHttpClient;
        }

        public async Task<string> InjectOrderAsync(Order orderToInject)
        {
            InjectOrderRequest request = new InjectOrderRequest
            {
                Order = orderToInject,
                Customer = new Customer
                {
                    Address = "Home",
                    Firstname = "John"
                }
            };
            var result = await _scoHttpClient.AddOrder(request);
            return result.OrderNumber;
        }

        //todo - implement interface knowing that it has to call the REST API described in readme.txt file 
    }
}
