﻿using System;
using Tweetinvi.Core.Attributes;
using Tweetinvi.Parameters;

namespace Tweetinvi.Core.Extensions
{
    public static class TweetSearchFiltersExtension
    {
        public static string GetQueryFilterName(this TweetSearchFilters tweetSearchFilters)
        {
            var field = tweetSearchFilters.GetType().GetField(tweetSearchFilters.ToString());
            var descriptionAttribute = (TweetSearchFilterAttribute)Attribute.GetCustomAttribute(field, typeof(TweetSearchFilterAttribute));
            return descriptionAttribute != null ? descriptionAttribute.FilterName : tweetSearchFilters.ToString().ToLowerInvariant();
        }
    }
}