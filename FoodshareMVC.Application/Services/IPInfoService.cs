using FoodshareMVC.Application.Helpers;
using FoodshareMVC.Application.Interfaces;
using FoodshareMVC.Application.ViewModels.Post;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FoodshareMVC.Application.Services
{
    public class IPInfoService : IIPInfoService
    {
        public IPInfo SetIPInfo()
        {
            var ipInfo = new IPInfo();
            try
            {
                string url = "https://ipinfo.io?token=97d6472d4b29b1";
                var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);
                var myRII = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRII.EnglishName;
            }
            catch (Exception)
            {
                return null;
            }
            return ipInfo;
        }
    }
}
