using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CertificateEnumerator 
{
    public class WebClientWithTimeout : WebClient
    {
        private readonly int timeoutMilliseconds;
        public WebClientWithTimeout(int timeoutMilliseconds)
        {
            this.timeoutMilliseconds = timeoutMilliseconds;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var result = base.GetWebRequest(address);
            result.Timeout = timeoutMilliseconds;
            return result;
        }
    }
}
