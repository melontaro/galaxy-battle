using System;
using ETModel;

namespace ETHotfix
{
    public interface IMHandler
    {
        ETVoid Handle(ETModel.Session session, object message);
        Type GetMessageType();
    }

}