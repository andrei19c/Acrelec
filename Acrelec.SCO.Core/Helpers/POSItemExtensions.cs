using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acrelec.SCO.DataStructures;

namespace Acrelec.SCO.Core.Helpers
{
    public static class POSItemExtensions
    {
        //todo - write an extension method which returns only the first 3 letters of the POSItem.Name
        public static string GetShortName(this POSItem posItem)
        {
            if (posItem.Name.Length < 3)
                return posItem?.Name;

            return posItem?.Name.Substring(0, 3);
        }
    }
}
