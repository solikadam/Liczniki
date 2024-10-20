using System;

namespace Liczniki;

public partial class AddCounterPage : ContentPage
{
    public event EventHandler<string> CounterAdded;

    public AddCounterPage()
    {
        InitializeComponent();
    }

    private async void OnAddCounterClicked(object sender, EventArgs e)
    {
        var counterName = CounterNameEntry.Text;

        if (string.IsNullOrWhiteSpace(counterName))
        {
            await DisplayAlert("B³¹d", "WprowadŸ nazwê licznika", "OK");
            return;
        }

        CounterAdded?.Invoke(this, counterName);
        await Navigation.PopModalAsync();
    }
}

