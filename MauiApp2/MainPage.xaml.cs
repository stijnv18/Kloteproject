using MauiApp2.Viewmodels;

namespace MauiApp2;

public partial class MainPage : ContentPage
{
    // Vaiables ----------------------------------------------------------------------------------------------------------------------------
    ViewModel_Main vm_ViewModel_Main;


    // Constructor -------------------------------------------------------------------------------------------------------------------------
    public MainPage()
    {
        InitializeComponent();
        vm_ViewModel_Main = new ViewModel_Main();

        BindingContext = vm_ViewModel_Main;
    }
}