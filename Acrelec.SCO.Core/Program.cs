﻿using Acrelec.SCO.Core.Helpers;
using Acrelec.SCO.Core.Interfaces;
using Acrelec.SCO.Core.Managers;
using Acrelec.SCO.Core.Providers;
using Acrelec.SCO.DataStructures;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Acrelec.SCO.Core.Services;
using Microsoft.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Linq;

namespace Acrelec.SCO.Core
{
    
    public class Program
    {
        private static ILogService _logService { get; set; }
        private static IOrderManager _orderManager { get; set; }
        private static IItemsProvider _itemsProvider { get; set; }
        private static IScoHttpClient _scoHttpClient { get; set; }

        public static async Task Main(string[] args)
        {
            IWebHost webHost = CreateWebHostBuilder(args).Build();

            //init
            _logService = webHost.Services.GetService<ILogService>();
            _itemsProvider = webHost.Services.GetService<IItemsProvider>();
            _orderManager = webHost.Services.GetService<IOrderManager>();
            _scoHttpClient = webHost.Services.GetService<IScoHttpClient>();

            //end init
            _logService.Print("SCO - Self Check Out System");

            //list POS items
            ListAllItems();

            //todo - check if server is available for order injection
            if (!await _scoHttpClient.IsAvailable())
            {
                _logService.Print("Server not available");
                Environment.Exit(0);
            }


            //todo - create an order containing the following items:
            //1*Coke
            //2*Water
            
            var newOrder = new Order();
            newOrder.OrderItems.Add(new OrderedItem { ItemCode = "100", Qty = 1 });
            newOrder.OrderItems.Add(new OrderedItem { ItemCode = "200", Qty = 1 });
            //...            

            //inject the order to POS
            var assignedOrderNumber = await _orderManager.InjectOrderAsync(newOrder);

            if (!string.IsNullOrEmpty(assignedOrderNumber))
                _logService.Print("Order injected with success");
            else
                _logService.Print("Error injecting order");

            Console.ReadLine();
        }

        /// <summary>
        /// list in Console all items (with all their properties)
        /// </summary>
        private static void ListAllItems()
        {
            var itemsProvider = _itemsProvider.AllPOSItems;
            //todo - list items and for each item a short code generated by the POSItemExtensions
            _logService.Print("----List All Items ----");
            for (int i = 0; i < itemsProvider.Count; i++)
            {
                _logService.Print($"Item {i}: ItemCode:{itemsProvider[i].ItemCode}, " +
                    $"Name:{itemsProvider[i].Name}, " +
                    $"ShortName:{itemsProvider[i].GetShortName()}, " +
                    $"IsAvailable:{itemsProvider[i].IsAvailable}, " +
                    $"UnitPrice:{itemsProvider[i].IsAvailable}");
            }
            _logService.Print("------------------------");
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
             WebHost.CreateDefaultBuilder(args)
                 .UseStartup<Startup>()
                 .UseUrls("http://localhost:5500");
    }
}

