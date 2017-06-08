﻿using Tweetinvi.Core.Credentials;
using Tweetinvi.Core.Injectinvi;
using Tweetinvi.Core.RateLimit;
using Tweetinvi.Core.Web;
using Tweetinvi.Credentials.QueryDTO;
using Tweetinvi.Credentials.RateLimit;
using Tweetinvi.Models.DTO.QueryDTO;

namespace Tweetinvi.Credentials
{
    public class TweetinviCredentialsModule : ITweetinviModule
    {
        private readonly ITweetinviContainer _container;

        public TweetinviCredentialsModule(ITweetinviContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<ITwitterAccessor, TwitterAccessor>(RegistrationLifetime.InstancePerThread);
            _container.RegisterType<ICredentialsAccessor, CredentialsAccessor>(RegistrationLifetime.InstancePerThread);
            _container.RegisterType<ICredentialsStore, CredentialsStore>(RegistrationLifetime.InstancePerApplication);

            _container.RegisterType<IAuthFactory, AuthFactory>();
            _container.RegisterType<IWebTokenFactory, WebTokenFactory>();
            _container.RegisterType<ICursorQueryHelper, CursorQueryHelper>();

            RegisterRateLimitHandler();
        }

        private void RegisterRateLimitHandler()
        {
            _container.RegisterType<IRateLimitAwaiter, RateLimitAwaiter>(RegistrationLifetime.InstancePerThread);
            _container.RegisterType<IRateLimitCache, RateLimitCache>(RegistrationLifetime.InstancePerApplication);
            _container.RegisterType<IRateLimitCacheManager, RateLimitCacheManager>(RegistrationLifetime.InstancePerThread);
            _container.RegisterType<IRateLimitHelper, RateLimitHelper>(RegistrationLifetime.InstancePerApplication);
            _container.RegisterType<IRateLimitUpdater, RateLimitUpdater>(RegistrationLifetime.InstancePerThread);
        }
    }
}