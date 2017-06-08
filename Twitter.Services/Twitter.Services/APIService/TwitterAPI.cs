using APIManager.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Core;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using Twitter.Services.IServiceModel;

namespace Twitter.Services.APIService
{
    public class TwitterAPI : DisposeObjects,Itwitter
    {
        private readonly ITwitterCredentials _credentials;

        public TwitterAPI()
        {
            _credentials = TwitterConfig.GenerateCredentials();
        }

        public ConsumerCredentials GetCredentials()
        {
            return new ConsumerCredentials(TwitterConfig.CONSUMER_KEY, TwitterConfig.CONSUMER_SECRET);
        }

        public string GetAppRedirectUrl(string Webdomain)
        {
            //Request.Url.Authority 
            return "https://" + Webdomain + "/TwitterAPI/ValidateTwitterAuth";
        }
        public IAuthenticationContext getAuthcontext(ConsumerCredentials appCreds, string redirectURL)
        {
            return AuthFlow.InitAuthentication(appCreds, redirectURL);
        }
        public ITwitterCredentials CredentialsFromVerifierCode(string verifierCode, string authorizationId)
        {
            return AuthFlow.CreateCredentialsFromVerifierCode(verifierCode, authorizationId);
        }
        public IEnumerable<Tweetinvi.Models.IMention> GetNotifications()
        {
            IEnumerable<Tweetinvi.Models.IMention> finaldata;
            finaldata = null;
            var success = Auth.ExecuteOperationWithCredentials(_credentials, () =>
            {
                IEnumerable<Tweetinvi.Models.IMention> Timelines = Timeline.GetMentionsTimeline();
                if (Timelines != null)
                {
                    finaldata = Timelines;
                    return true;
                }

                return false;
            });

            return finaldata;

        }
        public ITweet PublishTweet(string tweet, byte[] fileBytes)
        {
            

            var publishedTweet = Auth.ExecuteOperationWithCredentials(_credentials, () =>
            {
                var publishOptions = new PublishTweetOptionalParameters();
                if (fileBytes != null)
                {
                    publishOptions.MediaBinaries.Add(fileBytes);
                }

                return Tweet.PublishTweet(tweet, publishOptions);
            });
            return publishedTweet;
        }

        public IAuthenticatedUser GetAuthenticatedUser(ITwitterCredentials userCreds)
        {
            return Tweetinvi.User.GetAuthenticatedUser(userCreds);
        }
    }
}
