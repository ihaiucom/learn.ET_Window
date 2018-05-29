using System;
using System.Net;
using ETModel;
using MongoDB.Bson;
using TestNetwork;

namespace ETHotfix
{
    [MessageHandler]
    public class C2S_TxtHandler : AMRpcHandler<C2S_Txt,S2C_Txt>
	{
		protected override async void Run(Session session, C2S_Txt message, Action<S2C_Txt> reply)
		{
            S2C_Txt response = new S2C_Txt();
			try
            {
                Log.Info($"C2S_TxtHandler C2S_Txt: {message.Name} {message.Txt}");
                //if (message.Name != "abcdef" || message.Txt != "111111")
                //{
                //    response.Error = ErrorCode.ERR_AccountOrPasswordError;
                //    reply(response);
                //    return;
                //}

				response.Name = message.Name;
				response.Txt = message.Txt;
				reply(response);
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}
	}
}