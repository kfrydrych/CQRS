using CQRS.Query;
using System.Net;
using System.Threading.Tasks;

namespace QuickUseDemoWithAutofac
{
    public class GetDataQueryHandler : IHandleQueryAsync<GetDataQuery, string>
    {
        public async Task<string> Handle(GetDataQuery query)
        {
            var webClient = new WebClient();
            return await webClient.DownloadStringTaskAsync("http://msdn.microsoft.com");
        }
    }
}
