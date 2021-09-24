using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_сервис
{
    public class Message
    {
        public int Id { get; set;}
        public DateTime MessageTime { get; set;}

        public string UserName { get; set; }

        public string MsgText { get; set; }
    }
}
