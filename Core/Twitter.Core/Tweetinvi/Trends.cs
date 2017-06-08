﻿using System;
using System.Collections.Generic;
using Tweetinvi.Core.Controllers;
using Tweetinvi.Models;

namespace Tweetinvi
{
    /// <summary>
    /// Access the trendiest topics from Twitter.
    /// </summary>
    public static class Trends
    {
        [ThreadStatic]
        private static ITrendsController _trendsController;

        /// <summary>
        /// Controller handling any Trends request
        /// </summary>
        public static ITrendsController TrendsController
        {
            get
            {
                if (_trendsController == null)
                {
                    Initialize();
                }

                return _trendsController;
            }
        }

        static Trends()
        {
            Initialize();
        }

        private static void Initialize()
        {
            _trendsController = TweetinviContainer.Resolve<ITrendsController>();
        }

        /// <summary>
        /// Get the trends at a specific location
        /// </summary>
        public static IPlaceTrends GetTrendsAt(long woeid)
        {
            return TrendsController.GetPlaceTrendsAt(woeid);
        }

        /// <summary>
        /// Get the trends at a specific location
        /// </summary>
        public static IPlaceTrends GetTrendsAt(IWoeIdLocation woeIdLocation)
        {
            return TrendsController.GetPlaceTrendsAt(woeIdLocation);
        }

        /// <summary>
        /// Returns the locations that Twitter has trending topic information for.
        /// </summary>
        public static IEnumerable<ITrendLocation> GetAvailableTrendLocations()
        {
            return TrendsController.GetAvailableTrendLocations();
        }

        /// <summary>
        /// Returns the locations that Twitter has trending topic information for, closest to a specified location.
        /// </summary>
        public static IEnumerable<ITrendLocation> GetClosestTrendLocations(double latitude, double longitude)
        {
            return TrendsController.GetClosestTrendLocations(latitude, longitude);
        }

        /// <summary>
        /// Returns the locations that Twitter has trending topic information for, closest to a specified location.
        /// </summary>
        public static IEnumerable<ITrendLocation> GetClosestTrendLocations(ICoordinates coordinates)
        {
            return TrendsController.GetClosestTrendLocations(coordinates);
        }
    }
}