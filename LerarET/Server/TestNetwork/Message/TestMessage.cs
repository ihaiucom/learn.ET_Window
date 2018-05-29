using ETModel;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNetwork
{
    [Message(TestOpcode.C2S_Txt)]
    [ProtoContract]
    public partial class C2S_Txt : IRequest
    {
        [ProtoMember(90, IsRequired = true)]
        public int RpcId { get; set; }

        [ProtoMember(1, IsRequired = true)]
        public string Name;

        [ProtoMember(2, IsRequired = true)]
        public string Txt;

    }

    [Message(TestOpcode.S2C_Txt)]
    [ProtoContract]
    public partial class S2C_Txt : IResponse
    {
        [ProtoMember(90, IsRequired = true)]
        public int RpcId { get; set; }

        [ProtoMember(91, IsRequired = true)]
        public int Error { get; set; }

        [ProtoMember(92, IsRequired = true)]
        public string Message { get; set; }

        [ProtoMember(1, IsRequired = true)]
        public string Name;

        [ProtoMember(2, IsRequired = true)]
        public string Txt;
    }


    [Message(TestOpcode.C2S_Exit)]
    [ProtoContract]
    public partial class C2S_Exit : IRequest
    {
        [ProtoMember(90, IsRequired = true)]
        public int RpcId { get; set; }


        [ProtoMember(1, IsRequired = true)]
        public string Name;
    }



    [Message(TestOpcode.S2C_Exit)]
    [ProtoContract]
    public partial class S2C_Exit : IResponse
    {
        [ProtoMember(90, IsRequired = true)]
        public int RpcId { get; set; }

        [ProtoMember(91, IsRequired = true)]
        public int Error { get; set; }

        [ProtoMember(92, IsRequired = true)]
        public string Message { get; set; }


        [ProtoMember(1, IsRequired = true)]
        public string Name;
    }

}
