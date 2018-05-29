using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNetwork;

namespace TestMessage
{

    [MessageHandler]
    public class S2C_TxtMessageHandler : AMHandler<S2C_Txt>
    {
        protected override void Run(ETModel.Session session, S2C_Txt msg)
        {
            Log.Info($"S2C_TxtMessageHandler message: {msg.Name} {msg.Txt} RpcId= {msg.RpcId}   Error= {msg.Error}   Message= {msg.Message}  ");
        }
    }
}
