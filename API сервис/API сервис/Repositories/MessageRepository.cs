using API_сервис.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_сервис.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MessageContext _context;
        public MessageRepository (MessageContext context)
        {
            _context = context;
        }
        public async Task<Message> Add (Message message)
        {
            _context.Messages.Add(message);
            await  _context.SaveChangesAsync();

            return message;
        }
        public async Task<IEnumerable<Message>> Get() => await _context.Messages.ToListAsync();

    }
}
