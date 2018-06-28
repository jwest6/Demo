using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.DataAccess.Models;
using Demo.DataAccess.Repositories;

namespace Demo.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly MessageRepository _repository;

        public MessageController(MessageRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Message>>> Get()
        {
            return _repository.GetMessages();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        public ActionResult<Message> GetById(int id)
        {
            if (!_repository.TryGetMessage(id, out var message))
            {
                return NotFound();
            }

            return message;
        }

        [HttpPost]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Message>> CreateAsync(Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddMessageAsync(message);

            return CreatedAtAction(nameof(GetById),
                new { id = message.MessageValue }, message);
        }
    }
}