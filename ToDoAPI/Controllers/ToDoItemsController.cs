using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Models;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private readonly ToDoContext _context;

        public ToDoItemsController(ToDoContext context)
        {
            _context = context;
        }

        // GET: api/ToDoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItemDTO>>> GetToDoItens()
        {
            var ListaItens = await _context.ToDoItens.ToListAsync();
            List<ToDoItemDTO> ListaItensDTO = new List<ToDoItemDTO>();

            foreach(ToDoItem item in ListaItens)
            {
                ListaItensDTO.Add(new ToDoItemDTO(item));
            }

            return ListaItensDTO;

        }

        // GET: api/ToDoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItemDTO>> GetToDoItem(long id)
        {
            var toDoItem = await _context.ToDoItens.FindAsync(id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            var toDoItemDTO = new ToDoItemDTO(toDoItem);

            return toDoItemDTO;
        }

        // PUT: api/ToDoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoItem(long id, ToDoItemDTO toDoItemDTO)
        {
            if (id != toDoItemDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(toDoItemDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ToDoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ToDoItemDTO>> PostToDoItem(ToDoItemDTO toDoItemDTO)
        {
            var toDoItem = new ToDoItem { Name = toDoItemDTO.Name, IsComplete = toDoItemDTO.IsComplete };

            _context.ToDoItens.Add(toDoItem);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetToDoItem", new { id = toDoItem.Id }, toDoItem);

            return CreatedAtAction(nameof(GetToDoItem), new { id = toDoItem.Id }, toDoItem);
        }

        // DELETE: api/ToDoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem(long id)
        {
            var toDoItem = await _context.ToDoItens.FindAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            _context.ToDoItens.Remove(toDoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToDoItemExists(long id)
        {
            return _context.ToDoItens.Any(e => e.Id == id);
        }
    }
}
