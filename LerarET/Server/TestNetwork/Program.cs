using ETModel;
using MongoDB.Driver.Core.Misc;
using System;
using System.Net;
using System.Threading;

namespace TestNetwork
{
    class Program
    {
        static void Main(string[] args)
        {

            Game.EventSystem.Add(DLLType.Model, typeof(Game).Assembly);
            Game.EventSystem.Add(DLLType.Hotfix, typeof(Program).Assembly);


            EndPoint endPoint = null;
            bool result = EndPointHelper.TryParse("127.0.0.1:6060", out endPoint);
            Console.WriteLine($"EndPoint Parse result:{result} endPoint:{endPoint}");

            Game.Scene.AddComponent<OpcodeTypeComponent>();
            Game.Scene.AddComponent<MessageDispatherComponent>();
            Game.Scene.AddComponent<NetOuterComponent,  IPEndPoint>( (IPEndPoint)endPoint);


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
            Console.ReadLine();
        }
    }
}
