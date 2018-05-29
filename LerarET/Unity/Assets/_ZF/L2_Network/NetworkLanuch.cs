using ETModel;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using UnityEngine;
using TestNetwork;

public class NetworkLanuch : MonoBehaviour
{

    private readonly OneThreadSynchronizationContext contex = new OneThreadSynchronizationContext();
    async void Start ()
    {
        SynchronizationContext.SetSynchronizationContext(this.contex);

        DontDestroyOnLoad(gameObject);
        Game.EventSystem.Add(DLLType.Model, typeof(NetworkLanuch).Assembly);


        Game.Scene.AddComponent<OpcodeTypeComponent>();
        Game.Scene.AddComponent<MessageDispatherComponent>();


        Game.Scene.AddComponent<NetOuterComponent>();

        IPEndPoint connetEndPoint = NetworkHelper.ToIPEndPoint("127.0.0.1:6060");
        Session session = Game.Scene.GetComponent<NetOuterComponent>().Create(connetEndPoint);
        Game.Scene.AddComponent<SessionComponent>().Session = session;


        S2C_Txt msg = (S2C_Txt)await SessionComponent.Instance.Session.Call(new C2S_Txt() { Name = "AAA", Txt="Call Helo" });

        Log.Info($"S2C_Txt: {msg.Name} {msg.Txt} RpcId= {msg.RpcId}   Error= {msg.Error}   Message= {msg.Message}  ");

        //SessionComponent.Instance.Session.Send(new C2S_Txt() { Name = "BBB", Txt = "Send Helo" });


        SessionComponent.Instance.Session.Send(new C2S_Exit() { Name = "~~~~~~~~~~~~~~" });


    }


    private void Update()
    {
        this.contex.Update();
        Game.EventSystem.Update();
    }

    private void LateUpdate()
    {
        Game.EventSystem.LateUpdate();
    }

    private void OnApplicationQuit()
    {
        Game.Close();
    }
}
