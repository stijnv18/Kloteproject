using System.Windows.Input;
using MauiApp2.Models;

namespace MauiApp2.Viewmodels
{
    internal class ViewModel_MakeNewBlog : ViewmodelBase
    {
        // Vaiables ----------------------------------------------------------------------------------------------------------------------------
        // Private
        private readonly ApiService _apiService = new ApiService();
        private string _Title = "";
        private string _Content = "";
        private string _LabelNotifyStatusText = "";
        // Public
        string apiPostUrl = "https://localhost:8888/api/notes"; // Adjust the URL if needed
        public string Title
        {
            get => _Title;
            set
            {
                if (_Title != value)
                {
                    _Title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }
        public string Content
        {
            get => _Content;
            set
            {
                if (_Content != value)
                {
                    _Content = value;
                    OnPropertyChanged(nameof(Content));
                }
            }
        }
        public string LabelNotifyStatusText
        {
            get => _LabelNotifyStatusText;
            set
            {
                if (_LabelNotifyStatusText != value)
                {
                    _LabelNotifyStatusText = value;
                    OnPropertyChanged(nameof(LabelNotifyStatusText));
                }
            }
        }
        public ICommand MyButtonsubmitBlog { get; }
        
        // Constructor -------------------------------------------------------------------------------------------------------------------------
        public ViewModel_MakeNewBlog()
        {
            MyButtonsubmitBlog = new Command(OnSubmitBlog);
        }

        // Methodes ----------------------------------------------------------------------------------------------------------------------------
        public async void OnSubmitBlog()
        {
            var data = new
            {
                title = Title,
                content = Content
            };

            try
            {
                bool success = await _apiService.PostDataAsync(apiPostUrl, data);
                if (success)
                {
                    Title = "";
                    Content = "";
                    LabelNotifyStatusText = "Your blog is posted.";
                }
                else
                {
                    LabelNotifyStatusText = "Something went wrong!";
                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"An error occurred: {httpEx.Message}");
                LabelNotifyStatusText = "Something went wrong!";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                LabelNotifyStatusText = "Something went wrong!";
            }
        }
    }
}
