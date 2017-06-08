﻿using System;
using Tweetinvi.Models;

namespace Tweetinvi.WebLogic.Utils
{
    public static class HttpMethodExtensions
    {
        public static HttpMethod ToTweetinviHttpMethod(this System.Net.Http.HttpMethod method)
        {
            switch (method.Method)
            {
                case "GET":
                    return HttpMethod.GET;
                case "POST":
                    return HttpMethod.POST;
            }

            throw new InvalidCastException("Cannot convert http method");
        }
    }
}