using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Repository
{
    public interface IDisposedTracker
    {
        bool IsDisposed { get; set; }
    }
}