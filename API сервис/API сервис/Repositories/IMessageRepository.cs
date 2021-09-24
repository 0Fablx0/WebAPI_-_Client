using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_сервис.Repositories
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> Get();

        Task<Message> Add(Message message);

    }
}
