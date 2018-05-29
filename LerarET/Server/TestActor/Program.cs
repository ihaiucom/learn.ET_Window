using ETModel;
using System;
using System.Net;
using System.Threading;

namespace TestActor
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.EventSystem.Add(DLLType.Model, typeof(Game).Assembly);
            Game.EventSystem.Add(DLLType.Hotfix, typeof(Program).Assembly);


            Game.Scene.AddComponent<OpcodeTypeComponent>();
            Game.Scene.AddComponent<MessageDispatherComponent>();

            bool isLocation = false;

            if(isLocation)
            {

                Game.Scene.AddComponent<NetInnerComponent, IPEndPoint>(innerConfig.IPEndPoint);
                Game.Scene.AddComponent<LocationComponent>();
            }
            else
            {

                Game.Scene.AddComponent<NetInnerComponent, IPEndPoint>(innerConfig.IPEndPoint);
                Game.Scene.AddComponent<LocationProxyComponent>();
                //负责分发IActorMessage消息,给处理器 IActorTypeHandler, IMActorHandler
                Game.Scene.AddComponent<ActorMessageDispatherComponent>();
                Game.Scene.AddComponent<ActorMessageSenderComponent>();
            }



            while (true)
            {
                try
                {
                    Thread.Sleep(1);
                    Game.EventSystem.Update();
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }
    }
}
