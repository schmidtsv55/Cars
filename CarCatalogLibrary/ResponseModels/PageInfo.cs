using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace CarCatalogLibrary.ResponseModels
{
    public class PageInfo
    {
        public bool hasNextPage { get; set; }
        public string endCursor { get; set; }
    }
}
