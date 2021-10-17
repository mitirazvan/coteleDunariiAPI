using System.Threading.Tasks;

namespace CoteleDunarii.WebServices.WebScrapper
{
    public interface IScrapper
    {
        Task<bool> RetrieveData();
    }
}
