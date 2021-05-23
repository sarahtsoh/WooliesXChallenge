using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesChallenge.Models.Configuration
{
    public class ResourceOptions
    {
        public const string resource = "resource";
        public string baseUri { get; set; }
        public string token { get; set; }
    }
}
