using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.DataAccess.Models;

namespace Demo.DataAccess.Repositories
{
    public class MessageRepository
    {
        private readonly MessageContext _context;

        public MessageRepository(MessageContext context)
        {
            _context = context;

            if (_context.Messages.Count() == 0)
            {
                _context.Messages.AddRange(
                    new Message
                    {
                        MessageValue = "Hello World",
                    });
                _context.SaveChanges();
            }
        }

        public List<Message> GetMessages()
        {
            return _context.Messages.ToList();
        }

        public bool TryGetMessage(int id, out Message message)
        {
            message = _context.Messages.Find(id);

            return (message != null);
        }

        public async Task<int> AddMessageAsync(Message message)
        {
            int rowsAffected = 0;

            _context.Messages.Add(message);
            rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected;
        }
    }
}
