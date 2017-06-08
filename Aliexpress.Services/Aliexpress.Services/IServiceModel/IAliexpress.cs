


namespace Aliexpress.Services.IServiceModel
{
    public interface IAliexpress
    {
        #region Variables
        /* AliExpress API Key */
        int ApiKey { get; set; }
        /* AliExpress API Signature */
        string ApiSignature { get; set; }
        /* AliExpress API BaseUrl */
        string BaseUrl { get; set; }
        #endregion
        #region Methods

        #endregion
        string SearchString(string Keyword);
        string GetOrders(string StartDate, string EndDate, string OrderStatus);
    }
}
