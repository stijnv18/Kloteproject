using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiApp2.Models;

namespace MauiApp2.Viewmodels
{
    internal class ViewModel_Main : ViewmodelBase
    {
        // Vaiables ----------------------------------------------------------------------------------------------------------------------------
        // Private
        private readonly ApiService _apiService = new ApiService();
        private string apiUrl = "https://localhost:8888/api/notes";
        private string _LabelNotificationText = "";

        // Public
        public ObservableCollection<FullInfoBlogs> ListOfBlogs { get; set; }
        public string LabelNotificationText
        {
            get => _LabelNotificationText;
            set
            {
                _LabelNotificationText = value;
                OnPropertyChanged(nameof(LabelNotificationText));
            }
        }
        public ICommand MyButtonGetBlogs { get; }


        // Constructor -------------------------------------------------------------------------------------------------------------------------
        public ViewModel_Main() 
        {
            ListOfBlogs = new ObservableCollection<FullInfoBlogs>();
            InitGetBlogs();

            MyButtonGetBlogs = new Command(OnGetBlogs);
        }


        // Methodes ----------------------------------------------------------------------------------------------------------------------------
        public async void InitGetBlogs()
        {
            try
            {
                // Call the function to get JSON data
                var blogs = await _apiService.GetDataAsync(apiUrl);

                // Check if blogs is not null
                if (blogs != null)
                {
                    // Add the fetched blogs to the ObservableCollection (Newest blog on top)
                    foreach (var blog in blogs)
                    {
                        blog.DeleteRequested += OnDeleteRequested;
                        ListOfBlogs.Insert(0, blog);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public async void OnGetBlogs()
        {
            try
            {
                // Call the function to get JSON data
                var bloglist = await _apiService.GetDataAsync(apiUrl);

                // Check if blogs is not null
                if (bloglist != null)
                {
                    foreach (var blog in bloglist)
                    {
                        // Find the blog post in the collection
                        var newblog = ListOfBlogs.FirstOrDefault(b => b.Id == blog.Id);
                        if (newblog == null)
                        {
                            blog.DeleteRequested += OnDeleteRequested;
                            ListOfBlogs.Insert(0, blog);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        private void OnDeleteRequested(object sender, int id)
        {
            var delblog = ListOfBlogs.FirstOrDefault(b => b.Id == id);
            if (delblog != null)
            {
                ListOfBlogs.Remove(delblog);
            }
        }
    }
}
