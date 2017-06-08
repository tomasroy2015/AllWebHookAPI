using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Models;

namespace Twitter.Services.IServiceModel
{
    public interface Itwitter
    {
       
        IAuthenticationContext getAuthcontext(ConsumerCredentials appCreds, string redirectURL);
        ConsumerCredentials GetCredentials();
        string GetAppRedirectUrl(string Webdomain);

        ITwitterCredentials CredentialsFromVerifierCode(string verifierCode, string authorizationId);
        IAuthenticatedUser GetAuthenticatedUser(ITwitterCredentials userCreds);
        IEnumerable<Tweetinvi.Models.IMention> GetNotifications();
        ITweet PublishTweet(string tweet,byte[] fileBytes);
       
    }
}
