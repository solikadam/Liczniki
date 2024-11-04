using Microsoft.Maui.Controls;

namespace Liczniki;

public partial class AddCounterPage : ContentPage
{
    public event EventHandler<(string counterName, int initialValue)> CounterAdded;

    public AddCounterPage()
    {
        InitializeComponent();
    }

    private void OnAddCounterClicked(object sender, EventArgs e)
    {
        var counterName = CounterNameEntry.Text;
        if (int.TryParse(InitialValueEntry.Text, out int initialValue))
        {
            CounterAdded?.Invoke(this, (counterName, initialValue));
            Navigation.PopModalAsync();
        }
        else
        {
            DisplayAlert("B³¹d", "Proszê wprowadziæ prawid³ow¹ wartoœæ pocz¹tkow¹.", "OK");
        }
    }

    private void OnCancelClicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}
