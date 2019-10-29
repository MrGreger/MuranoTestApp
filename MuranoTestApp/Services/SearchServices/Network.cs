using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MuranoTestApp.Services.SearchServices
{
    public static class Network
    {
        public static readonly HttpClient Client = new HttpClient();
    }
}
