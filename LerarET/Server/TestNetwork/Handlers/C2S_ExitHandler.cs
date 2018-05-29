
using ETModel;
using TestNetwork;
namespace TestMessage
{

    [MessageHandler]
    public class C2S_ExitHandler : AMHandler<C2S_Exit>
    {
        protected override void Run(ETModel.Session session, C2S_Exit msg)
        {
            Log.Info($"C2S_ExitHandler message: {msg.Name}  RpcId= {msg.RpcId}    ");

            session.Send(new S2C_Exit() { Name="Server TestNetwork"});
        }
    }
}
