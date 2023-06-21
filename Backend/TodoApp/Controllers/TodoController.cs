using System;
using System.Threading.Tasks;
using Foundation.API.Types;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Domain.Commands.Todo;
using TodoApp.Domain.Dtos;
using TodoApp.Domain.Queries.Todo;

namespace TodoApp.Controllers
{
    public class TodoController : ApiControllerBase
    {
        public TodoController(IMediator mediator) : base(mediator)
        {
        }
        
        /// <summary>
        /// Get Todos
        /// </summary>
        /// <returns>Todos information</returns>
        [HttpGet]
        [ProducesResponseType(typeof(TodosDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<TodosDto>> GetTodos()
        {
            return NotNull(await QueryAsync(new GetTodosQuery()));
        }
        
        /// <summary>
        /// Get Todo by id
        /// </summary>
        /// <param name="id">Id of Todo</param>
        /// <returns>Todo</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TodoDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<TodoDto>> GetTodoAsync([FromRoute] Guid id)
        {
            return NotNull(await QueryAsync(new GetTodoByIdQuery(id)));
        }
        
        /// <summary>
        /// Create Todo
        /// </summary>
        /// <param name="command">Info of Todo</param>
        /// <returns>Todo</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateTodoAsync([FromBody] CreateTodoCommand command)
        {
            return Ok(await CommandAsync(command));
        }

        /// <summary>
        /// Update Todo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command">Info of Todo</param>
        /// <returns>Todo</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> UpdateTodoAsync([FromRoute] Guid id, [FromBody] UpdateTodoCommand command)
        {
            command.Id = id;
            return Ok(await CommandAsync(command));
        }
        
        /// <summary>
        /// Delete Todo
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Todo</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> DeleteTodoAsync([FromRoute] Guid id)
        {
            var command = new DeleteTodoCommand(id);
            return Ok(await CommandAsync(command));
        }
    }
}
