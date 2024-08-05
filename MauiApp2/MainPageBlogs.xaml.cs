using System;
using System.Net.Http;
using System.Text.Json;
using MyMauiApp.Services;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MauiApp2;

public partial class MainPageBlogs : ContentPage
{
    // Vaiables ----------------------------------------------------------------------------------------------------------------------------
    private readonly ApiService _apiService;
    public ObservableCollection<DataBlog> ListOfBlogs { get; set; } = new ObservableCollection<DataBlog>();
    string apiUrl = "https://localhost:8888/api/notes";

    // Constructor -------------------------------------------------------------------------------------------------------------------------
    public MainPageBlogs()
	{
		InitializeComponent();
        _apiService = new ApiService();
        BindingContext = this;
        InitGetBlogs();
    }

    // Methodes ----------------------------------------------------------------------------------------------------------------------------
    public async void OnGetBlogs(object sender, EventArgs e)
    {
        try
        {
            // Call the function to get JSON data
            var blogs = await _apiService.GetDataAsync(apiUrl);

            // Clear the existing items in the ObservableCollection
            ListOfBlogs.Clear();

            // Check if blogs is not null
            if (blogs != null)
            {
                // Add the fetched blogs to the ObservableCollection (Newest blog on top)
                foreach (var blog in blogs)
                {
                    ListOfBlogs.Insert(0, blog);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");

            // Change the content of the Label
            NotificationGetBlogs.Text = "Getting Data Failed!";
            // Change the text color
            NotificationGetBlogs.TextColor = Colors.Red;
        }
    }
    public async void InitGetBlogs()
    {
        try
        {
            // Call the function to get JSON data
            var blogs = await _apiService.GetDataAsync(apiUrl);

            // Clear the existing items in the ObservableCollection
            ListOfBlogs.Clear();

            // Check if blogs is not null
            if (blogs != null)
            {
                // Add the fetched blogs to the ObservableCollection (Newest blog on top)
                foreach (var blog in blogs)
                {
                    ListOfBlogs.Insert(0, blog);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");

            // Change the content of the Label
            NotificationGetBlogs.Text = "Getting Data Failed!";
            // Change the text color
            NotificationGetBlogs.TextColor = Colors.Red;
        }
    }
    public async void PostLikeDislikeCounter(string apiurl, string tempBlogId, int tempBlogLikeCounter)
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
        }
        catch (HttpRequestException httpEx)
        {
            NotificationGetBlogs.Text = $"HTTP Request Error: {httpEx.Message}";
            NotificationGetBlogs.TextColor = Colors.Red;
        }
        catch (Exception ex)
        {
            NotificationGetBlogs.Text = $"General Error: {ex.Message}";
            NotificationGetBlogs.TextColor = Colors.Red;
        }
    }
    public (string blogIdF, int CounterF) GetBlogIDAndCounter(object sender)
    {
        string tempBlogId = "null";
        int tempBlogLikeCounter = -5;

        if (sender is Button button && button.CommandParameter is string blogId)
        {
            // Find the blog post in the collection
            var blog = ListOfBlogs.FirstOrDefault(b => b.Id == blogId);
            if (blog != null)
            {
                tempBlogId = blog.Id;
                tempBlogLikeCounter = blog.LikeCounter;

                return (tempBlogId, tempBlogLikeCounter);
            }
            else
            {
                return (tempBlogId, tempBlogLikeCounter);
            }
        }
        else
        {
            return (tempBlogId, tempBlogLikeCounter);
        }
    }
    public void OnLikeBlog(object sender, EventArgs e)
    {
        string BlogLikeID;
        int NewBlogLikeCounter;

        // Get BlogID and BlogLikeCounter
        var list = GetBlogIDAndCounter(sender);

        // Post BlogID and BlogLikeCounter
        if (list.blogIdF != "null" || list.CounterF > 0)
        {
            BlogLikeID = list.blogIdF;
            NewBlogLikeCounter = list.CounterF + 1;
            PostLikeDislikeCounter(apiUrl, BlogLikeID, NewBlogLikeCounter);
        }

        // Get new Like counter value
        InitGetBlogs();
    }

    public void OnDislikeBlog(object sender, EventArgs e)
    {
        string BlogDislikeID;
        int BlogDislikeCounter;

        // Get BlogID and BlogDislikeCounter
        var list = GetBlogIDAndCounter(sender);

        // Post BlogID and BlogDislikeCounter
        if (list.blogIdF != "null" || list.CounterF > 0)
        {
            BlogDislikeID = list.blogIdF;
            BlogDislikeCounter = list.CounterF + 1;
            PostLikeDislikeCounter(apiUrl, BlogDislikeID, BlogDislikeCounter);
        }

        // Get new Dislikecounter value
        InitGetBlogs();
    }

}