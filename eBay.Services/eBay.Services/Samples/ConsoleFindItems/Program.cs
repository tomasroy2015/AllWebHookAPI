
using System;
using eBay.Services;
using eBay.Services.Finding;
using Slf;
using System.Text;
using eBay.Service.Core.Sdk;
using eBay.Service.Util;
using eBay.Service.Core.Soap;
using eBay.Service.Call;


namespace ConsoleFindItems
{
    /// <summary>
    /// A sample to show eBay Finding servcie call using the simplied interface provided by the FindingKit.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Init log
            // This sample and the FindingKit use <a href="http://slf.codeplex.com/">Simple Logging Facade(SLF)</a>,
            // Here is a <a href="http://slf.codeplex.com/">good introdution</a> about SLF(for example, supported log levels, how to log to a file)
            LoggerService.SetLogger(new Slf.ConsoleLogger());
            ILogger logger = LoggerService.GetLogger();

            // Basic service call flow:
            // 1. Setup client configuration
            // 2. Create service client
            // 3. Create outbound request and setup request parameters
            // 4. Call the operation on the service client and receive inbound response
            // 5. Handle response accordingly
            // Handle exception accrodingly if any of the above steps goes wrong.
            try
            {
                MakeGetOrders();
                Console.ReadLine();
                
            }
            catch (Exception ex)
            {
                // Handle exception if any
                logger.Error(ex);
            }

            Console.WriteLine("Press any key to close the program.");
            Console.ReadKey();

        }
        public void searchitemfront() {
            
                APIManager.Core.Configuration.ClientConfig config = new APIManager.Core.Configuration.ClientConfig();
                // Initialize service end-point configuration
                config.EndPointAddress = "http://svcs.ebay.com/services/search/FindingService/v1";
                // set eBay developer account AppID
                config.ApplicationId = "JohnDeft-Deftsoft-PRD-f9a6113e8-5f7db885";

                // Create a service client
                FindingServicePortTypeClient client = FindingServiceClientFactory.getServiceClient(config);

                // Create request object
                FindItemsByKeywordsRequest request = new FindItemsByKeywordsRequest();
                // Set request parameters
                request.keywords = "harry potter phoenix";
                PaginationInput pi = new PaginationInput();
                pi.entriesPerPage = 2;
                pi.entriesPerPageSpecified = true;
                request.paginationInput = pi;

                // Call the service
                FindItemsByKeywordsResponse response = client.findItemsByKeywords(request);

                // Show output
               //  logger.Info("Ack = " + response.ack);
              //  logger.Info("Find " + response.searchResult.count + " items.");
                SearchItem[] items = response.searchResult.item;
                for (int i = 0; i < items.Length; i++)
                {
                //    logger.Info(items[i].title);
                }
                
        }
        private static void MakeGetOrders()
        {
            //create the context
            ApiContext context = new ApiContext();

            //set the User token
            
            context.ApiCredential.eBayToken = "AgAAAA**AQAAAA**aAAAAA**FZ+8Vw**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6AAkYKlCJOGogudj6x9nY+seQ**omgDAA**AAMAAA**Q0DAfQ1ClUTnjY/1N8ujyoSJrBmqlU1B7dbbXdb8LSR5rHjOlI3DfRtJDiiveIoMsjbuGsXw1JyoqzepO6gauJHGZfcl1MILus7Kf2VFGylUd5QyHtY8/dY8NxVAyk3U1yut3KEHlRYzCEAxcX3XrnaEFdsb0vC6jno2de69WShmC1KQ2u5nSi2PQL/xrMmycXiQYXUqh+exHR6K/SWFRl5ro43l/9wwaQN6i5k5KRxPVq8hcPXKZVhrl25fryEF74Et1nh/ISodB0YtJDdUdf+gt3euzc8TnIPY06SS6LyAk2ZWNxuen4qQQ0Tyjy8r9gwBSbCCPeBjF1AVshT0gQenGcqcn8Hf+Fk8wproUz1GxbIRicqef1xIUU1Q7SioFBtRCI47/qTz3/Sxg/uOtlRXdr2+231HPQNQoSEIqFyWloaXrZlSRXzfzUK48+yYvU+XfD3T0f7UVsQqyGHjgDRsP9UW1r79Jr5Z4ZlqpqETJqV1k/aeDAbJe5pSHt5k/svmsz0awzAIjVvZ4R3kwltsSPN8voqnC+tUkWFf09vN0BsbIT0OUUq21TfkLNzHjDEpA1S7ddwZt2ZTHhfzJoQCRe7JXwvf4AS9l9Sj1sG3joovEehzZlivrxShQ8ZheJLM4Ue9LYLaSGABHSGGqo7NKT528WzZXCCR3Kt3vSanzVUw8cX1hlYHeDd8nYWL0xXaDC2162VbIuTQO+zrAbU/K2Rb5fENrZ5mr1op0RKChM567l5TWKTTYotksVXY";

            //set the server url
            context.SoapApiServerUrl = "https://api.ebay.com/wsapi";
        
            //enable logging
            context.ApiLogManager = new ApiLogManager();
            context.ApiLogManager.ApiLoggerList.Add(new FileLogger("log.txt", true, true, true));
            context.ApiLogManager.EnableLogging = true;
            //set the version
            context.Version = "817";
            context.Site = SiteCodeType.UK;
            bool blnHasMore = true;
            DateTime CreateTimeFromPrev, CreateTimeFrom, CreateTimeTo;

            

            //GetMyEbaySelling API 
            GetMyeBaySellingCall getmyselling = new GetMyeBaySellingCall(context);
            getmyselling.DetailLevelList = new DetailLevelCodeTypeCollection();
            getmyselling.DetailLevelList.Add(DetailLevelCodeType.ReturnAll);
            getmyselling.Execute();

            

            //int totalEntries = check.paginationOutput.totalEntries;

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

            if (getOrders.ApiResponse.Ack != AckCodeType.Failure)
            {
                //Check if any orders are returned
                if (getOrders.ApiResponse.OrderArray.Count != 0)
                {
                    foreach (OrderType order in getOrders.ApiResponse.OrderArray)
                    {
                        //Update your system with the order information.
                        Console.WriteLine("Order Number: " + order.OrderID);
                        Console.WriteLine("OrderStatus: " + order.OrderStatus);
                        Console.WriteLine("Order Created On: " + order.CreatedTime);

                        //Get Order Details
                        TransactionTypeCollection orderTrans = order.TransactionArray;

                        //Order could be comprised of one or more items
                        foreach (TransactionType transaction in orderTrans)
                        {
                            Console.WriteLine("Order for: " + transaction.Item.ItemID + ", " + transaction.Item.SKU + ", " + transaction.Item.Title);

                            //If you are listing variation items, you will need to retrieve the variation
                            //details as chosen by the buyer
                            if (transaction.Variation.SKU != null)
                            {
                                Console.WriteLine("Variation: " + transaction.Variation.SKU);
                            }
                            Console.WriteLine("OrderLineItemID: " + transaction.OrderLineItemID);
                            Console.WriteLine("Qty Purchased: " + transaction.QuantityPurchased);
                            Console.WriteLine("Buyer Info: " + order.BuyerUserID + ", " + transaction.Buyer.Email);
                        }

                        if (order.CheckoutStatus.Status == CompleteStatusCodeType.Complete)
                        {
                            //Get Payment Details
                            Console.WriteLine("Order Adjustment Amount: " + order.AdjustmentAmount.Value);
                            Console.WriteLine("Order Amount Paid: " + order.AmountPaid.Value);
                            Console.WriteLine("Payment Method: " + order.CheckoutStatus.PaymentMethod);
                            ExternalTransactionTypeCollection extTrans = order.ExternalTransaction;
                            foreach (ExternalTransactionType extTran in extTrans)
                            {
                                Console.WriteLine("External TransactionID: " + extTran.ExternalTransactionID);
                                Console.WriteLine("External Transaction Time: " + extTran.ExternalTransactionTime);
                                Console.WriteLine("Payment/Refund Amount: " + extTran.PaymentOrRefundAmount.Value);
                            }

                            //Get shipping information
                            ShippingServiceOptionsType shipping;
                            shipping = order.ShippingServiceSelected;
                            Console.WriteLine("Shipping Service Selected: " + order.ShippingServiceSelected.ShippingService);

                            //Get Shipping Address - Address subject to change if the buyer has not completed checkout                      
                            AddressType address = order.ShippingAddress;
                            StringBuilder sAdd = new StringBuilder();
                            sAdd = sAdd.Append(address.Name);
                            if (address.Street != null && address.Street != "")
                                sAdd.Append(", " + address.Street);

                            if (address.Street1 != null && address.Street1 != "")
                                sAdd.Append(", " + address.Street1);

                            if (address.Street2 != null && address.Street2 != "")
                                sAdd.Append(", " + address.Street2);

                            if (address.CityName != null && address.CityName != "")
                                sAdd.Append(", " + address.CityName);

                            if (address.StateOrProvince != null && address.StateOrProvince != "")
                                sAdd.Append(", " + address.StateOrProvince);

                            if (address.PostalCode != null && address.PostalCode != "")
                                sAdd.Append(", " + address.PostalCode);

                            if (address.CountryName != null && address.CountryName != "")
                                sAdd.Append(", " + address.CountryName);

                            if (address.Phone != null && address.Phone != "")
                                sAdd.Append(": Phone" + address.Phone);

                            Console.WriteLine("Shipping Address: " + sAdd);
                            double salesTax;

                            //Get the sales tax
                            if (order.ShippingDetails.SalesTax.SalesTaxAmount == null)
                                salesTax = 0.00;
                            else
                                salesTax = order.ShippingDetails.SalesTax.SalesTaxAmount.Value;

                            Console.WriteLine("Sales Tax: " + salesTax);
                            if (order.BuyerCheckoutMessage != null)
                            {
                                Console.WriteLine("Buyer Checkout Message: " + order.BuyerCheckoutMessage);
                            }
                        }
                        Console.WriteLine("********************************************************");
                    }
                }
                else
                {
                    Console.WriteLine("No Order available");
                    Console.WriteLine("TotalNumberOfPages: " + getOrders.ApiResponse.PaginationResult.TotalNumberOfPages.ToString());
                    Console.WriteLine("TotalNumberOfEntries: " + getOrders.ApiResponse.PaginationResult.TotalNumberOfEntries.ToString());
                    Console.WriteLine("********************************************************");
                }
            }
        }

    }
}
