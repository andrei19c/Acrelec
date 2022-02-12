using Acrelec.SCO.Core.Interfaces;
using Acrelec.SCO.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Acrelec.SCO.Core.Providers
{
    /// <summary>
    /// class providing the list of items as retrieved from POS system
    /// </summary>
    public class ItemsProvider : IItemsProvider
    {
        private List<POSItem> _posItems;

        public List<POSItem> AllPOSItems => _posItems;

        public List<POSItem> AvailablePOSItems => _posItems.Where(x=>x.IsAvailable).ToList();

        /// <summary>
        /// constructor
        /// </summary>
        public ItemsProvider()
        {
            _posItems = new List<POSItem>();
            LoadItemsFromPOS();
        }

        /// <summary>
        /// retrieving items from POS is a simple parse of a json
        /// </summary>
        public void LoadItemsFromPOS()
        {
            //todo - implement the code to load items from Data\ContentItems.json file
            string text = File.ReadAllText("Data\\ContentItems.json");
            _posItems = JsonSerializer.Deserialize<List<POSItem>>(text);
        }

        //todo - implement missing methods of interface
    }
}
