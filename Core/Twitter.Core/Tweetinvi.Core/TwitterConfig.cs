using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Models;

namespace Tweetinvi.Core
{
   public class TwitterConfig
    {
        public static string CONSUMER_KEY = "Qk3gj9MOflnPOtLasKmqLg79O";
        public static string CONSUMER_SECRET = "4EB0SaojIF4iUJsXeMIOhqLJK95SxavhWHoWvZ12iHeQrziFZp";
        public static string ACCESS_TOKEN = "3167104476-6WBzQV2ipk340DeWNXWiFEdQ0sgRVbgd1scyCHk";
        public static string ACCESS_TOKEN_SECRET = "fYUcuCWvlfeHcmjnJcHWsIzs7TVZQNjCU0CdCFarVZQD8";

        public static ITwitterCredentials GenerateCredentials()
        {
            return new TwitterCredentials(CONSUMER_KEY, CONSUMER_SECRET, ACCESS_TOKEN, ACCESS_TOKEN_SECRET);
        }
    }
}
