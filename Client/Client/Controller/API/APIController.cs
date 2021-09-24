using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Client.Model;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Client.Controller.API
{
    class APIController
    {
        
        const string uri = "https://localhost:44356/database";

        public async Task<ObservableCollection<Message>> APIGetMsg()
        {
            using (HttpClient client = new HttpClient())
            {
                ObservableCollection<Message> arrMsg = null;
                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode) arrMsg = await response.Content.ReadAsAsync<ObservableCollection<Message>>();
                

                return arrMsg;
            }
        }
   

        public async Task APISendMsgAsync (string username,string msgText)
        {
            using (HttpClient client = new HttpClient()) 
            {
                Message msg = new Message() { Id = 0, MessageTime = DateTime.Now, UserName = username, MsgText = msgText };
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, msg);
            }

        }

    }
}
