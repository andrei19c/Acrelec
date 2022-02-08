using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acrelec.SCO.Server.Model;

namespace Acrelec.SCO.Server.Validation
{
    public interface IOrderValidation
    {
        Dictionary<string, List<string>> ValidateRequest(InjectOrderRequest injectOrderRequest);
    }
}
