﻿namespace Tweetinvi.Models
{
    public interface IConsumerCredentials
    {
        /// <summary>
        /// Key identifying a specific consumer application
        /// </summary>
        string ConsumerKey { get; set; }

        /// <summary>
        /// Secret Key identifying a specific consumer application
        /// </summary>
        string ConsumerSecret { get; set; }

        /// <summary>
        /// Token required for Application Only Authentication
        /// </summary>
        string ApplicationOnlyBearerToken { get; set; }

        /// <summary>
        /// Clone the current credentials.
        /// </summary>
        IConsumerCredentials Clone();

        /// <summary>
        /// Are credentials correctly set up for application only authentication.
        /// </summary>
        bool AreSetupForApplicationAuthentication();
    }

    public class ConsumerCredentials : IConsumerCredentials
    {
        public ConsumerCredentials(string consumerKey, string consumerSecret)
        {
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
        }

        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }

        public string ApplicationOnlyBearerToken { get; set; }
        
        public IConsumerCredentials Clone()
        {
            var clone = new ConsumerCredentials(ConsumerKey, ConsumerSecret);

            CopyPropertiesToClone(clone);

            return clone;
        }

        public bool AreSetupForApplicationAuthentication()
        {
            return !string.IsNullOrEmpty(ConsumerKey) &&
                   !string.IsNullOrEmpty(ConsumerSecret);
        }

        protected void CopyPropertiesToClone(IConsumerCredentials clone)
        {
            clone.ApplicationOnlyBearerToken = ApplicationOnlyBearerToken;
        }
    }
}