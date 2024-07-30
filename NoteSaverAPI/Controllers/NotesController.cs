using Microsoft.AspNetCore.Mvc;
using NoteSaverAPI.Models;
using System;
using System.IO;

namespace NoteSaverAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        [HttpPost]
        public IActionResult SaveNote([FromBody] Note note)
        {
            if (note == null || string.IsNullOrEmpty(note.Content) || string.IsNullOrEmpty(note.FilePath))
            {
                return BadRequest("Note content and file path are required.");
            }

            try
            {
                System.IO.File.WriteAllText(note.FilePath, note.Content);
                return Ok("Note saved successfully.");
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(403, "You do not have permission to save to this location.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while saving the note: {ex.Message}");
            }
        }
    }
}
