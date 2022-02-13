using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acrelec.SCO.Core.Model.RestExchangedMessages;

namespace Acrelec.SCO.Core.Interfaces
{
    public interface IScoHttpClient
    {
        Task<bool> IsAvailable();
        Task<InjectOrderResponse> AddOrder(InjectOrderRequest order);
    }
}
