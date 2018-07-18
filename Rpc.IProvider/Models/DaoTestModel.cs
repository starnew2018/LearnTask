using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rpc.IProvider.Models
{
    [Alias("testtb")]
    public class DaoTestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Psw { get; set; }
    }
}
