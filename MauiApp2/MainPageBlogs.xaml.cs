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
    public async Task<bool> PostLikeDislikeCounter(string apiurl, int tempBlogId, int tempBlogLikeCounter)
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
            NotificationGetBlogs.Text = $"HTTP Request Error: {httpEx.Message}";
            NotificationGetBlogs.TextColor = Colors.Red;
            return false;
        }
        catch (Exception ex)
        {
            NotificationGetBlogs.Text = $"General Error: {ex.Message}";
            NotificationGetBlogs.TextColor = Colors.Red;
            return false;
        }
    }
    public (int blogIdF, int CounterF) GetBlogIDAndCounter(object sender)
    {
        int tempBlogId = -4;
        int tempBlogLikeCounter = -5;

        if (sender is Button button && button.CommandParameter is int blogId)
        {
            // Find the blog post in the collection
            var blog = ListOfBlogs.FirstOrDefault(b => b.Id == blogId);
            if (blog != null)
            {
                tempBlogId = blog.Id;
                tempBlogLikeCounter = blog.Likes;

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
    public async void OnLikeBlog(object sender, EventArgs e)
    {
        int BlogLikeID;
        int NewBlogLikeCounter;

        // Get BlogID and BlogLikeCounter
        var list = GetBlogIDAndCounter(sender);

        // Post BlogID and BlogLikeCounter
        if (list.blogIdF > 0 || list.CounterF > 0)
        {
            BlogLikeID = list.blogIdF;
            NewBlogLikeCounter = list.CounterF + 1;
            string tempUrl = apiUrl + "/" + BlogLikeID.ToString() + "/" + "like";
            await PostLikeDislikeCounter(tempUrl, BlogLikeID, NewBlogLikeCounter);
        }

        // Get new Like counter value
        InitGetBlogs();  
    }
    public async void OnDislikeBlog(object sender, EventArgs e)
    {
        int BlogDislikeID;
        int BlogDislikeCounter;
        
        // Get BlogID and BlogDislikeCounter
        var list = GetBlogIDAndCounter(sender);

        // Post BlogID and BlogDislikeCounter
        if (list.blogIdF > 0 || list.CounterF > 0)
        {
            BlogDislikeID = list.blogIdF;
            BlogDislikeCounter = list.CounterF + 1;
            string tempUrl = apiUrl + "/" + BlogDislikeID.ToString() + "/" + "dislike";
            await PostLikeDislikeCounter(tempUrl, BlogDislikeID, BlogDislikeCounter);
        }

        // Get new Dislikecounter value
        InitGetBlogs();
    }
    public async void OnDeleteBlog(object sender, EventArgs e)
    {
        string DeleteURl = "";

        // Get BlogID and BlogLikeCounter
        var list = GetBlogIDAndCounter(sender);

        // Post BlogID and BlogLikeCounter
        if (list.blogIdF > 0)
        {
            DeleteURl = apiUrl + "/" + list.blogIdF.ToString();
            bool success = await _apiService.DeleteDataAsync(DeleteURl);
            if (success)
            {
                // Get new Dislikecounter value
                InitGetBlogs();
            }
        }


    }

}