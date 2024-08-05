using Microsoft.AspNetCore.Mvc;
using NoteSaverAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace NoteSaverAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<NotesController> _logger;

        public NotesController(AppDbContext dbContext, ILogger<NotesController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
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
                _dbContext.Notes.Add(note);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Note saved successfully.");
                return Ok("Note saved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving the note.");
                return StatusCode(500, $"An error occurred while saving the note: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            try
            {
                var notes = await _dbContext.Notes.ToListAsync();
                return Ok(notes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the notes.");
                return StatusCode(500, $"An error occurred while retrieving the notes: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote([FromRoute] int id)
        {
            try
            {
                var note = await _dbContext.Notes.FindAsync(id);
                if (note == null)
                {
                    return NotFound($"Note with ID {id} not found.");
                }

                _dbContext.Notes.Remove(note);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Note with ID {id} deleted successfully.");
                return Ok($"Note with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the note with ID {id}.");
                return StatusCode(500, $"An error occurred while deleting the note: {ex.Message}");
            }
        }



        [HttpPost("{id}/like")]

        public async Task<IActionResult> LikePost([FromRoute] int id)
        {
            try
            {
                var post = await _dbContext.Notes.FindAsync(id);
                if (post == null)
                {
                    return NotFound($"Post with ID {id} not found.");
                }

                post.Likes += 1;
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Post with ID {id} liked.");
                return Ok($"Post with ID {id} liked.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while liking the post with ID {id}.");
                return StatusCode(500, $"An error occurred while liking the post: {ex.Message}");
            }
        }

        [HttpPost("{id}/dislike")]

        public async Task<IActionResult> DislikePost([FromRoute] int id)
        {
            try
            {
                var post = await _dbContext.Notes.FindAsync(id);
                if (post == null)
                {
                    return NotFound($"Post with ID {id} not found.");
                }

                post.Dislikes += 1;
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Post with ID {id} Disliked.");
                return Ok($"Post with ID {id} Disliked.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while Disliking the post with ID {id}.");
                return StatusCode(500, $"An error occurred while Disliking the post: {ex.Message}");
            }
        }


    }
}
