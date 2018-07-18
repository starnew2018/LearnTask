using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rpc.Contract.Models
{
    [ProtoContract]
    public class TestModelDto
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public string Psw { get; set; }
    }
}
