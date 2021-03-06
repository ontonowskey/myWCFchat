using System;
using System.ServiceModel;

namespace wcf_chat
{
    [ServiceContract(CallbackContract = typeof(IServerChatCallback))]

    public interface IServiceChat
    {
        [OperationContract]
        int Connect(string name);

        [OperationContract]
        void Disconnect(int id);

        [OperationContract(IsOneWay = true)]
        void SendMsg(string msg, int id); 

    }

    public interface IServerChatCallback
    {
        [OperationContract(IsOneWay = true)]
        
        void MsgCallback(string msg);
    }
}