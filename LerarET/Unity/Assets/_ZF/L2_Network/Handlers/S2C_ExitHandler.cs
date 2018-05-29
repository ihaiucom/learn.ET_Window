using ETModel;
using TestNetwork;

namespace TestMessage
{

    [MessageHandler]
    public class S2C_ExitHandler : AMHandler<S2C_Exit>
    {
        protected override void Run(ETModel.Session session, S2C_Exit msg)
        {
            Log.Info($"S2C_ExitHandler message: {msg.Name}  RpcId= {msg.RpcId}   Error= {msg.Error}   Message= {msg.Message}  ");
        }
    }
}
