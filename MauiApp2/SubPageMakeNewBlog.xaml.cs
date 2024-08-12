using MauiApp2.Viewmodels;

namespace MauiApp2;

public partial class SubPageMakeNewBlog : ContentPage
{
    // Vaiables -------------------------------------------------------------------------------------------------------------------------------
    ViewModel_MakeNewBlog vm_MakeNewBlog;

    // Constructor ----------------------------------------------------------------------------------------------------------------------------
    public SubPageMakeNewBlog()
	{
        InitializeComponent();
        vm_MakeNewBlog = new ViewModel_MakeNewBlog();

        BindingContext = vm_MakeNewBlog;
    }
}