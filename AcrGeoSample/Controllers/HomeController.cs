﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Collections;

namespace AcrGeoSample.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            try
            {
                var registryURL = Environment.GetEnvironmentVariable("APPSETTING_DOCKER_REGISTRY_SERVER_URL") ??
                                                "acrdemos.azurecr.io";
                ViewData["REGISTRYURL"] = registryURL;

                var hostEntry = await System.Net.Dns.GetHostEntryAsync(registryURL);
                ViewData["HOSTENTRY"] = hostEntry.HostName;

                string region = hostEntry.HostName.Split('.')[1];
                ViewData["REGION"] = region;

                string mapImage = string.Format("{0}map.png", region.ToLower());
                ViewData["MAPIMAGE"] = mapImage;

                var registryIp = System.Net.Dns.GetHostAddresses(registryURL)[0].ToString();
                ViewData["REGISTRYIP"] = registryIp;

                //StringBuilder envVars = new StringBuilder();
                //foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
                //    envVars.Append(string.Format("<strong>{0}</strong>:{1}<br \\>", de.Key, de.Value));

                //ViewData["ENV_VARS"] = envVars.ToString();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            return View();
        }
    }
}