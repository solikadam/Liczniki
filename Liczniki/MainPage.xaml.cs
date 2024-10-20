using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using Liczniki.Models;

namespace Liczniki;

public partial class MainPage : ContentPage
{
    public ObservableCollection<Counter> Counters { get; set; }
    private string FileName => Path.Combine(FileSystem.AppDataDirectory, "counters.json");

    public MainPage()
    {
        InitializeComponent();
        Counters = new ObservableCollection<Counter>();
        LoadCounters();
    }

    private async void LoadCounters()
    {
        if (File.Exists(FileName))
        {
            var json = await File.ReadAllTextAsync(FileName);
            var counters = JsonSerializer.Deserialize<List<Counter>>(json);
            if (counters != null)
            {
                foreach (var counter in counters)
                {
                    Counters.Add(counter);
                }
            }
        }

        // Dodaj licznik początkowy, jeśli go jeszcze nie ma
        if (!Counters.Any(c => c.Name == "Licznik początkowy"))
        {
            Counters.Add(new Counter("Licznik początkowy", 0));
            SaveCounters();
        }

        CountersCollectionView.ItemsSource = Counters;
    }

    private async void SaveCounters()
    {
        var json = JsonSerializer.Serialize(Counters.ToList());
        await File.WriteAllTextAsync(FileName, json);
    }

    private void OnPlusClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var counter = (Counter)button.CommandParameter;
        counter.Value++;
        SaveCounters();
    }

    private void OnMinusClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var counter = (Counter)button.CommandParameter;
        counter.Value--;
        SaveCounters();
    }

    private async void OnShowAddCounterFormClicked(object sender, EventArgs e)
    {
        var addCounterPage = new AddCounterPage();
        addCounterPage.CounterAdded += OnCounterAdded;
        await Navigation.PushModalAsync(addCounterPage);
    }

    private void OnCounterAdded(object sender, string counterName)
    {
        if (counterName == "Licznik początkowy" && Counters.Any(c => c.Name == "Licznik początkowy"))
        {
            DisplayAlert("Błąd", "Licznik początkowy może być dodany tylko raz.", "OK");
            return;
        }

        var newCounter = new Counter(counterName, 0);
        Counters.Add(newCounter);
        SaveCounters();
    }

    private void OnDeleteCounterClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var counter = (Counter)button.CommandParameter;
        Counters.Remove(counter);
        SaveCounters();
    }
}
