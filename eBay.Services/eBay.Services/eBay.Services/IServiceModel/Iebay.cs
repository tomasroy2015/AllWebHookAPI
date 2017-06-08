using eBay.Service.Call;
using eBay.Service.Core.Sdk;
using eBay.Services.Finding;

namespace eBay.Services.IServiceModel
{
    public interface Iebay
    {
        SearchItem[] SearchItemFront(string Keyword);
        GetOrdersCall GetOrders();

        string ResutlMessage(SearchItem[] items);

        string ResutlMessage(GetOrdersCall getOrders);

        ApiContext InitializeContext();
    }
}
