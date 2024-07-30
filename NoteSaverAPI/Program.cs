using Microsoft.AspNetCore.Mvc;
using NoteSaverAPI.Models;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Add the new endpoint for saving notes
app.MapPost("/saveNote", ([FromBody] Note note) =>
{
    if (note == null || string.IsNullOrEmpty(note.Content) || string.IsNullOrEmpty(note.FilePath))
    {
        return Results.BadRequest("Note content and file path are required.");
    }

    try
    {
        File.WriteAllText(note.FilePath, note.Content);
        return Results.Ok("Note saved successfully.");
    }
    catch (UnauthorizedAccessException)
    {
        return Results.Problem("You do not have permission to save to this location.", statusCode: 403);
    }
    catch (Exception ex)
    {
        return Results.Problem($"An error occurred while saving the note: {ex.Message}", statusCode: 500);
    }
})
.WithName("SaveNote")
.WithOpenApi();

app.Run("https://localhost:8888");

