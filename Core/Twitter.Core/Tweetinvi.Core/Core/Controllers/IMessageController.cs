﻿using System.Collections.Generic;
using Tweetinvi.Models;
using Tweetinvi.Models.DTO;
using Tweetinvi.Parameters;

namespace Tweetinvi.Core.Controllers
{
    public interface IMessageController
    {
        IEnumerable<IMessage> GetLatestMessagesReceived(int maximumMessages = TweetinviConsts.MESSAGE_GET_COUNT);
        IEnumerable<IMessage> GetLatestMessagesReceived(IMessagesReceivedParameters messagesReceivedParameters);

        IEnumerable<IMessage> GetLatestMessagesSent(int maximumMessages = TweetinviConsts.MESSAGE_GET_COUNT);
        IEnumerable<IMessage> GetLatestMessagesSent(IMessagesSentParameters messagesSentParameters);

        // Publish Message
        IMessage PublishMessage(string text, IUserIdentifier recipient);
        IMessage PublishMessage(string text, long recipientId);
        IMessage PublishMessage(string text, string recipientUserName);
        IMessage PublishMessage(IPublishMessageParameters parameter);

        // Destroy Message
        bool DestroyMessage(IMessage message);
        bool DestroyMessage(IMessageDTO messageDTO);
        bool DestroyMessage(long messageId);
    }
}