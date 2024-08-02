using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using NoteSaverAPI.Models;
using System;
using System.Threading.Tasks;

namespace NoteSaverAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public NotesController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> SaveNote([FromBody] Note note)
        {
            if (note == null || string.IsNullOrEmpty(note.Content))
            {
                return BadRequest("Note content is required.");
            }

            try
            {
                // Save the note to the database
                _dbContext.Notes.Add(note);
                await _dbContext.SaveChangesAsync();
                return Ok("Note saved successfully.");  
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while saving the note: {ex.Message}");
            }
        }
    }
}
