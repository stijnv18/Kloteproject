using System;
using System.IO;

namespace NoteSaverApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your note:");
            string note = Console.ReadLine();

            string defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "note.txt");
            Console.WriteLine($"Enter the file path to save the note (or press Enter to use the default path: {defaultPath}):");
            string filePath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(filePath))
            {
                filePath = defaultPath;
            }

            SaveNoteToFile(note, filePath);

            Console.WriteLine("Note saved successfully!");
        }

        static void SaveNoteToFile(string note, string filePath)
        {
            try
            {
                File.WriteAllText(filePath, note);
                Console.WriteLine($"Note successfully saved to {filePath}");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: You do not have the necessary permissions to save to this location.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while saving the note: " + ex.Message);
            }
        }
    }
}
