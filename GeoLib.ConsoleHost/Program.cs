﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using GeoLib.Contracts;
using GeoLib.Services;

namespace GeoLib.ConsoleHost
{

    public class Program
    {

        public static void Main(string[] args)
        {
            var hostGeoManager = new ServiceHost(typeof(GeoManager));

            //string address = "net.tcp://localhost:8009/GeoService";
            //Binding binding = new NetTcpBinding();
            //Type contract = typeof(IGeoService);

            //hostGeoManager.AddServiceEndpoint(contract, binding, address);

            hostGeoManager.Open();


            Console.WriteLine("Services started. Press [Enter] to exit.");
            Console.ReadLine();

            hostGeoManager.Close();
        }
    }
}
