using FoodshareMVC.Infrastructure.Helpers.IPInfoManage;
using FoodshareMVC.Infrastructure.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FoodshareMVC.Domain.Helpers;
using FoodshareMVC.Domain.Models.HelperModels;

namespace FoodshareMVC.Infrastructure.Helpers.IPInfoManage
{
    public class IPInfoManager : IIPInfoManager
    {
        public IPInfo SetIPInfo()
        {
            var ipInfo = new IPInfo();
            try
            {
                string url = "https://ipinfo.io?token=97d6472d4b29b1";
                var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);
                var myCountryRII = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myCountryRII.EnglishName;
            }
            catch (Exception)
            {
                return null;
            }
            return ipInfo;
        }
    }
}
