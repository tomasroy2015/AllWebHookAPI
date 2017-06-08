using APIManager.Core;
using System;
using eBay.Services.Finding;
using eBay.Service.Core.Sdk;
using eBay.Service.Call;
using eBay.Service.Core.Soap;
using APIManager.Core.Utils;
using eBay.Services.IServiceModel;
using APIManager.Core.Configuration;

namespace eBay.Services.APIService
{
    public class ebayAPI : DisposeObjects, Iebay
    {

        public SearchItem[] SearchItemFront(string keywords)
        {
            // Initialize service end-point configuration
           APIManager.Core.Configuration.ClientConfig config = new APIManager.Core.Configuration.ClientConfig();

            // Initialize service end-point configuration
            config.EndPointAddress = "http://svcs.ebay.com/services/search/FindingService/v1";
            // set eBay developer account AppID
            config.ApplicationId = "JohnDeft-Deftsoft-PRD-f9a6113e8-5f7db885";

            // Create a service client
            FindingServicePortTypeClient client = FindingServiceClientFactory.getServiceClient(config);

            // Create Ebay API request object
            FindItemsByKeywordsRequest request = new FindItemsByKeywordsRequest();
            
            /* 
            *  Set request parameters
            *  Sample Keyword CodeBelow 
            *  request.keywords = "harry potter phoenix";
            */

            request.keywords = keywords;

            PaginationInput pi = new PaginationInput();
            pi.entriesPerPage = 2;
            pi.entriesPerPageSpecified = true;
            request.paginationInput = pi;

            // Call the Ebay API service
            FindItemsByKeywordsResponse response = client.findItemsByKeywords(request);

            //Get Ebay Items From Response
            SearchItem[] items = response.searchResult.item;

            return items;
        }
        public ApiContext InitializeContext()
        {
            //create the context
            ApiContext context = new ApiContext();

            //set the User token
            context.ApiCredential.eBayToken = "AgAAAA**AQAAAA**aAAAAA**FZ+8Vw**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6AAkYKlCJOGogudj6x9nY+seQ**omgDAA**AAMAAA**Q0DAfQ1ClUTnjY/1N8ujyoSJrBmqlU1B7dbbXdb8LSR5rHjOlI3DfRtJDiiveIoMsjbuGsXw1JyoqzepO6gauJHGZfcl1MILus7Kf2VFGylUd5QyHtY8/dY8NxVAyk3U1yut3KEHlRYzCEAxcX3XrnaEFdsb0vC6jno2de69WShmC1KQ2u5nSi2PQL/xrMmycXiQYXUqh+exHR6K/SWFRl5ro43l/9wwaQN6i5k5KRxPVq8hcPXKZVhrl25fryEF74Et1nh/ISodB0YtJDdUdf+gt3euzc8TnIPY06SS6LyAk2ZWNxuen4qQQ0Tyjy8r9gwBSbCCPeBjF1AVshT0gQenGcqcn8Hf+Fk8wproUz1GxbIRicqef1xIUU1Q7SioFBtRCI47/qTz3/Sxg/uOtlRXdr2+231HPQNQoSEIqFyWloaXrZlSRXzfzUK48+yYvU+XfD3T0f7UVsQqyGHjgDRsP9UW1r79Jr5Z4ZlqpqETJqV1k/aeDAbJe5pSHt5k/svmsz0awzAIjVvZ4R3kwltsSPN8voqnC+tUkWFf09vN0BsbIT0OUUq21TfkLNzHjDEpA1S7ddwZt2ZTHhfzJoQCRe7JXwvf4AS9l9Sj1sG3joovEehzZlivrxShQ8ZheJLM4Ue9LYLaSGABHSGGqo7NKT528WzZXCCR3Kt3vSanzVUw8cX1hlYHeDd8nYWL0xXaDC2162VbIuTQO+zrAbU/K2Rb5fENrZ5mr1op0RKChM567l5TWKTTYotksVXY";

            //set the server url
            context.SoapApiServerUrl = "https://api.ebay.com/wsapi";

            //set the version
            context.Version = "817";
            context.Site = SiteCodeType.UK;
            

            return context;
        }
        public GetOrdersCall GetOrders()
        {
            // Initialize Ebay API Context With Essential Credentials
            ApiContext context = InitializeContext();

            //Initialize Local Variables 
            bool blnHasMore = true;
            DateTime CreateTimeFromPrev, CreateTimeFrom, CreateTimeTo;

            //Get Orders API CALL
            GetOrdersCall getOrders = new GetOrdersCall(context);
            getOrders.DetailLevelList = new DetailLevelCodeTypeCollection();
            getOrders.DetailLevelList.Add(DetailLevelCodeType.ReturnAll);

            

            //CreateTimeTo set to the current time
            CreateTimeTo = DateTime.Now.ToUniversalTime();

            //Assumption call is made every 15 sec. So CreateTimeFrom of last call was 15 mins
            //prior to now
            TimeSpan ts1 = new TimeSpan(9000000000);
            CreateTimeFromPrev = CreateTimeTo.Subtract(ts1);

            //Set the CreateTimeFrom the last time you made the call minus 2 minutes
            TimeSpan ts2 = new TimeSpan(1200000000);
            CreateTimeFrom = CreateTimeFromPrev.Subtract(ts2);
            getOrders.CreateTimeFrom = CreateTimeFrom;
            getOrders.CreateTimeTo = CreateTimeTo;
            getOrders.Execute();

            return getOrders;
        }

        public string ResutlMessage(SearchItem[] items)
        {
            string result = "";

            if (items != null)
            {
                result = "Mail Successfully Sent";
            }
            else
            {
                result = "Unexpected Error Occured";
            }
            return result;

        }
        public string ResutlMessage(GetOrdersCall getOrders)
        {
            string result = "";

            string message = "";
            if (getOrders.ApiResponse.Ack != AckCodeType.Failure)
            {
                message = "Records Successfully Fetched";
            }
            else
            {
                message = "Unexpected Error Occured";
            }
            return result;

        }
    }
}
