using System;
using System.Net.Http;
using System.Text.Json;
using MyMauiApp.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MauiApp2;

public partial class MainPageBlogs : ContentPage
{
    // Vaiables ----------------------------------------------------------------------------------------------------------------------------
    private readonly ApiService _apiService;
    public ObservableCollection<DataBlog> ListOfBlogs { get; set; } = new ObservableCollection<DataBlog>();
    string apiGetUrl = "https://localhost:8888/api/notes";

    // Constructor -------------------------------------------------------------------------------------------------------------------------
    public MainPageBlogs()
	{
		InitializeComponent();
        _apiService = new ApiService();
        BindingContext = this;
    }

    // Methodes ----------------------------------------------------------------------------------------------------------------------------
    public async void OnGetBlogs(object sender, EventArgs e)
    {
        try
        {
            // Call the function to get JSON data
            var blogs = await _apiService.GetDataAsync(apiGetUrl);

            // Clear the existing items in the ObservableCollection
            ListOfBlogs.Clear();

            // Add the fetched blogs to the ObservableCollection
            foreach (var blog in blogs)
            {
                ListOfBlogs.Add(blog);
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


}