using MyMauiApp.Services;
using System.Threading.Tasks;

namespace MauiApp2;

public partial class SubPageMakeNewBlog : ContentPage
{
    // Vaiables ----------------------------------------------------------------------------------------------------------------------------
    private readonly ApiService _apiService;
    public string newBlogTitel = "Empty value";
    public string newBlogText = "Empty value";
    string apiPostUrl = "https://localhost:8888/api/notes"; // Adjust the URL if needed

    // Constructor ----------------------------------------------------------------------------------------------------------------------------
    public SubPageMakeNewBlog()
	{
        InitializeComponent();
        _apiService = new ApiService();
    }

    // Methodes ----------------------------------------------------------------------------------------------------------------------------
    public async void OnSubmitBlog(object sender, EventArgs e)   // The function connected to a button in SubPageMakeNewBlog.xaml
    {
        var data = new
        {
            title = nameOfBlog.Text,     // nameOfBlog is the name of one of the "entry" in SubPageMakeNewBlog.xaml
            content = textOfBlog.Text       // textOfBlog is the name of one of the "entry" in SubPageMakeNewBlog.xaml
        };

        try
        {
            bool success = await _apiService.PostDataAsync(apiPostUrl, data);
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