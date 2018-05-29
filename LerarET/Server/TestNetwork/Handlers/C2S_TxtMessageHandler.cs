using ETModel;
using TestNetwork;
namespace TestMessage
{

    [MessageHandler]
    public class C2S_TxtMessageHandler : AMHandler<C2S_Txt>
    {
        protected override void Run(ETModel.Session session, C2S_Txt msg)
        {
            Log.Info($"C2S_TxtMessageHandler message: {msg.Name} {msg.Txt} RpcId= {msg.RpcId}    ");
        }
    }
}
