 using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Base
{
    public class SubscriptionInfo
    {
        public Type HandlerType { get;private set; }

        public SubscriptionInfo(Type handlerType)
        {
            HandlerType = handlerType;
        }

        public static SubscriptionInfo Typed(Type handlerType)
        {
            return new SubscriptionInfo(handlerType);
        }
    }
}
