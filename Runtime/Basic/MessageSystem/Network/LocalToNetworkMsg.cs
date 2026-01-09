using UnityMessageCenter.Basic;
namespace UnityMessageCenter.Basic.Network
{
    public struct LocalToNetworkMsg : IMessageWithData
    {
        public string eventName;
        public EventPayload payload;
    }
}