using Microsoft.Maui.Controls;

namespace Liczniki;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new NavigationPage(new MainPage());
    }
}
