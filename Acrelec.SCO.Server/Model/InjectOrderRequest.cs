using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrelec.SCO.Server.Model
{
    public class InjectOrderRequest
    {
        public Order Order { get; set; }
        public Customer Customer { get; set; }
    }
}
