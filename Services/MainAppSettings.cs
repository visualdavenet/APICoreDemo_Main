using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICoreDemo.Services
{
    public class MainAppSettings
    {
        public const string SectionName = "ConnectionStrings";
        public string DemoDB { get; set; }
    }
}
