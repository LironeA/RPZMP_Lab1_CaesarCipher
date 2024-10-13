using System;
using System.IO;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Storage;
using Microsoft.Maui.Storage;

namespace CaesarCipher.Services
{
    public class FileService
    {
        // Метод для завантаження тексту з вибраного файлу
        public async Task<string> LoadFromFileAsync()
        {
            try
            {
                var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, new[] { "text/plain" } },
                    { DevicePlatform.WinUI, new[] { ".txt" } },
                    { DevicePlatform.MacCatalyst, new[] { "public.plain-text" } },
                    { DevicePlatform.iOS, new[] { "public.plain-text" } }
                });

                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    FileTypes = customFileType,
                    PickerTitle = "Please select a text file"
                });

                if(result != null)
                {
                    using var stream = await result.OpenReadAsync();
                    using var reader = new StreamReader(stream);
                    return await reader.ReadToEndAsync();
                }
            }
            catch(Exception ex)
            {
                // Обробка винятків
                Console.WriteLine($"Error loading file: {ex.Message}");
            }

            return string.Empty;
        }

        // Метод для збереження файлу в обрану користувачем папку
        public async Task SaveToFileAsync(string content, string fileName)
        {
            try
            {
                // Виклик діалогу вибору файлу для збереження
                var result = await FolderPicker.Default.PickAsync();

                if(result != null)
                {
                    var folderPath = result.Folder.Path;
                    var filePath = Path.Combine(folderPath, fileName);

                    using var writer = File.CreateText(filePath);
                    await writer.WriteAsync(content);

                    await Application.Current.MainPage.DisplayAlert("Success", $"File saved to {filePath}", "OK");
                }
            }
            catch(Exception ex)
            {
                // Обробка винятків
                Console.WriteLine($"Error saving file: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to save the file.", "OK");
            }
        }
    }
}
