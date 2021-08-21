using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoteleDunarii.WebScrapper
{
    public interface IScrapper
    {
        Task<bool> RetrieveData();
    }
}
