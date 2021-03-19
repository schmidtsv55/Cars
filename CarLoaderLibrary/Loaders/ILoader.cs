using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarLoaderLibrary.Loaders
{
    interface ILoader
    {
        Task LoadAsync();
    }
}
