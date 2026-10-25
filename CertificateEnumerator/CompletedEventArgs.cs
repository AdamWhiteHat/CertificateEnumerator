using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertificateEnumerator
{
    public class CompletedEventArgs : EventArgs
    {
        public bool Success { get; set; }

        public CompletedEventArgs(bool success)
        {
            Success = success;
        }
    }
}
