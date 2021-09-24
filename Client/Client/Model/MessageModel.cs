using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    class Message
    {
        public int Id { get; set; }
        public DateTime MessageTime { get; set; }

        public string UserName { get; set; }

        public string MsgText { get; set; }
    }
}
