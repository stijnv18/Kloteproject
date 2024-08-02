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
    string apiUrl = "https://localhost:8888/api/notes"; // Adjust the URL if needed


    public async void OnSubmitBlog(object sender, EventArgs e)   // The function connected to a button in SubPageMakeNewBlog.xaml
    {
        var data = new
        {
            title = nameOfBlog.Text,     // nameOfBlog is the name of one of the "entry" in SubPageMakeNewBlog.xaml
            content = textOfBlog.Text       // textOfBlog is the name of one of the "entry" in SubPageMakeNewBlog.xaml
        };

        TestStatusLabel.Text = $"{data.title} -- {data.content} -- {apiUrl}";

        try
        {
            bool success = await _apiService.PostDataAsync(apiUrl, data);
            if (success)
            {
                // Change the content of the Label
                notifyApiStatusLabel.Text = "Data posted successfully.";
                // Change the text color
                notifyApiStatusLabel.TextColor = Colors.Green;

                // Change the input fields back to empty
                nameOfBlog.Text = "";
                textOfBlog.Text = "";
            }
            else
            {
                // Change the content of the Label
                notifyApiStatusLabel.Text = "Failed to post data.";
                // Change the text color 
                notifyApiStatusLabel.TextColor = Colors.Red;
            }
        }
        catch (HttpRequestException httpEx)
        {
            notifyApiStatusLabel.Text = $"HTTP Request Error: {httpEx.Message}";
            notifyApiStatusLabel.TextColor = Colors.Red;
        }
        catch (Exception ex)
        {
            notifyApiStatusLabel.Text = $"General Error: {ex.Message}";
            notifyApiStatusLabel.TextColor = Colors.Red;
        }
    }

}