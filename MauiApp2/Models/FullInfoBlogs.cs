using System.Windows.Input;
using System.ComponentModel;

namespace MauiApp2.Models
{
    public class FullInfoBlogs : INotifyPropertyChanged
    {
        // Vaiables ----------------------------------------------------------------------------------------------------------------------------
        // Private
        private string apiUrl = "https://localhost:8888/api/notes";
        private readonly ApiService _apiService = new ApiService();
        private int _Id;
        private string _Title;
        private string _Content;
        private DateTime _CreatedAt;
        private int _Likes;
        private int _Dislikes;

        // Public
        public int Id
        {
            get => _Id;
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }
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
        public DateTime CreatedAt
        {
            get => _CreatedAt;
            set
            {
                if (_CreatedAt != value)
                {
                    _CreatedAt = value;
                    OnPropertyChanged(nameof(CreatedAt));
                }
            }
        }
        public int Likes
        {
            get => _Likes;
            set
            {
                if (_Likes != value)
                {
                    _Likes = value;
                    OnPropertyChanged(nameof(Likes));
                }
            }
        }
        public int Dislikes
        {
            get => _Dislikes;
            set
            {
                if (_Dislikes != value)
                {
                    _Dislikes = value;
                    OnPropertyChanged(nameof(Dislikes));
                }
            }
        }


        public event EventHandler<int> DeleteRequested;
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand MyButtonDeleteBlogs { get;}
        public ICommand MyButtonLikeBlogs { get; }
        public ICommand MyButtonDislikeBlogs { get; }


        // Constructor -------------------------------------------------------------------------------------------------------------------------
        public FullInfoBlogs()
        {
            MyButtonDeleteBlogs = new Command<int>(OnDeleteBlog);
            MyButtonLikeBlogs = new Command<int>(OnLikeBlog);
            MyButtonDislikeBlogs = new Command<int>(OnDislikeBlog);
        }


        // Methodes ----------------------------------------------------------------------------------------------------------------------------
        public async void OnLikeBlog(int blogIdF)
        {
            // Try to change the value in the database
            int templike = Likes + 1;
            string tempUrl = apiUrl + "/" + blogIdF.ToString() + "/" + "like";
            bool success = await PostLikeDislikeCounter(tempUrl, blogIdF, templike);
            if (success)
            {
                // Change the value here in the class
                Likes = Likes + 1;
            }
        }
        public async void OnDislikeBlog(int blogIdF)
        {
            // Try to change the value in the database
            int tempDislike = Dislikes + 1;
            string tempUrl = apiUrl + "/" + blogIdF.ToString() + "/" + "like";
            bool success = await PostLikeDislikeCounter(tempUrl, blogIdF, tempDislike);
            if (success)
            {
                // Change the value here in the class
                Dislikes = Dislikes + 1;
            }
        }
        public async void OnDeleteBlog(int blogIdF)
        {
            string DeleteURl = "";

            // Post BlogID and BlogLikeCounter
            if (blogIdF > 0)
            {
                DeleteURl = apiUrl + "/" + blogIdF.ToString();
                bool success = await _apiService.DeleteDataAsync(DeleteURl);
                if (success)
                {
                    DeleteRequested?.Invoke(this, blogIdF);
                }
            }
        }

        // extra methodes
        private async Task<bool> PostLikeDislikeCounter(string apiurl, int tempBlogId, int tempBlogLikeCounter)
        {
            // Place ID and Counter in data class
            var data = new
            {
                BlogId = tempBlogId,
                BlogLikeCounter = tempBlogLikeCounter
            };

            // connectie met database
            try
            {
                bool success = await _apiService.PostDataAsync(apiurl, data);
                return true;
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"An error occurred: {httpEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

