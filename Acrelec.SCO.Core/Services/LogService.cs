using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acrelec.SCO.Core.Interfaces;

namespace Acrelec.SCO.Core.Services
{
    public class LogService : ILogService
    {
        public void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}
