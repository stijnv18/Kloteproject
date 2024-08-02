namespace MauiApp2;

public partial class SubPageMakeNewBlog : ContentPage
{
	public SubPageMakeNewBlog()
	{
        InitializeComponent();
    }

    public string newBlogTitel = "Empty value";
    public string newBlogText = "Empty value";

    private void OnSubmitBlog(object sender, EventArgs e)   // The function connected to a button in SubPageMakeNewBlog.xaml
    {
        newBlogTitel = nameOfBlog.Text;                     // nameOfBlog is the name of one of the "entry" in SubPageMakeNewBlog.xaml 
        newBlogText = textOfBlog.Text;                      // textOfBlog is the name of one of the "entry" in SubPageMakeNewBlog.xaml
    }
}