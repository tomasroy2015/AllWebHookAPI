﻿using System.Collections.Generic;
using Tweetinvi.Models;

namespace Tweetinvi.Core.RateLimit
{
    /// <summary>
    /// Update the rate limit cached information.
    /// </summary>
    public interface IRateLimitUpdater
    {
        /// <summary>
        /// Inform the updater a specific query has been executed.
        /// </summary>
        void QueryExecuted(string query, int numberOfRequests = 1);

        /// <summary>
        /// Inform the updater a specific query has been executed with a specific set of credentials.
        /// </summary>
        void QueryExecuted(string query, ITwitterCredentials credentials, int numberOfRequests = 1);

        /// <summary>
        /// Inform the updater a specific query has been executed with a specific set of credentials.
        /// </summary>
        void QueryExecuted(string query, ITwitterCredentials credentials, Dictionary<string, IEnumerable<string>> rateLimitHeaders);

        /// <summary>
        /// Inform that you want to query rate limits to be set to 0.
        /// </summary>
        void ClearRateLimitsForQuery(string query);
    }
}