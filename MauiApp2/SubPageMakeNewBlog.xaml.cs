using MyMauiApp.Services;
using System.Threading.Tasks;

namespace MauiApp2;

public partial class SubPageMakeNewBlog : ContentPage
{
    private readonly ApiService _apiService;

    public SubPageMakeNewBlog()
	{
        InitializeComponent();
        _apiService = new ApiService();
    }

    public string newBlogTitel = "Empty value";
    public string newBlogText = "Empty value";
    string apiUrl = "https://localhost:8888/saveNote"; // Adjust the URL if needed


    public async void OnSubmitBlog(object sender, EventArgs e)   // The function connected to a button in SubPageMakeNewBlog.xaml
    {
        var data = new
        {
            newBlogTitel = nameOfBlog.Text,     // nameOfBlog is the name of one of the "entry" in SubPageMakeNewBlog.xaml
            newBlogText = textOfBlog.Text       // textOfBlog is the name of one of the "entry" in SubPageMakeNewBlog.xaml
        };

        bool success = await _apiService.PostDataAsync(apiUrl, data);
        if (success)
        {
            // Change the content of the Label
            notifyApiStatusLabel.Text = "Data posted successfully.";
            // Change the text color
            notifyApiStatusLabel.TextColor = Colors.Green;
        }
        else
        {
            // Change the content of the Label
            notifyApiStatusLabel.Text = "Failed to post data.";
            // Change the text color 
            notifyApiStatusLabel.TextColor = Colors.Red;
        }
    }
}