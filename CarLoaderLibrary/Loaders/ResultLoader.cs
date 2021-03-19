using System;
using System.Collections.Generic;
using System.Text;

namespace CarLoaderLibrary.Loaders
{
    public class ResultLoader
    {
        public Exception Exception { get; set; }
        public string Cursor { get; set; }
        public StatusResultLoader Status { get; set; }
    }
    public enum StatusResultLoader
    {
        Success,
        Error
    }
}
