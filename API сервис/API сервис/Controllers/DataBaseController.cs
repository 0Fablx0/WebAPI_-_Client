using API_сервис.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_сервис.Controllers
{
    

    [ApiController]
    [Route("[controller]")]
    public class DataBaseController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;

        public DataBaseController (IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

      
        public async Task<IEnumerable<Message>> Get() => await _messageRepository.Get();

        [HttpPost]
        public async Task<ActionResult<Message>> AddValue([FromBody] Message msg)
        {
            var newMsg = await _messageRepository.Add(msg);
            return CreatedAtAction(nameof(Get), new { id = newMsg.Id }, newMsg);
        }
    }
}
