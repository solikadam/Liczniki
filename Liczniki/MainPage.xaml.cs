using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using Liczniki.Models;
using Liczniki.Services;

namespace Liczniki
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Counter> Counters { get; set; }
        private CounterService _counterService;

        public MainPage()
        {
            InitializeComponent();

            _counterService = new CounterService();
            Counters = _counterService.LoadCounters();

            CountersCollectionView.ItemsSource = Counters;
        }

        // Dodawanie nowego licznika
        private void OnAddCounterClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CounterNameEntry.Text) && int.TryParse(CounterInitialValueEntry.Text, out int initialValue))
            {
                var newCounter = new Counter(CounterNameEntry.Text, initialValue);
                Counters.Add(newCounter);

                // Zapisanie zmian do pliku
                _counterService.SaveCounters(Counters);

                // Wyczyszczenie pól formularza
                CounterNameEntry.Text = string.Empty;
                CounterInitialValueEntry.Text = string.Empty;
            }
        }

        // Obsługa kliknięcia przycisku "-"
        private void OnMinusClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var counter = button.CommandParameter as Counter;

            if (counter != null && counter.Value > 0)
            {
                counter.Value--;
                _counterService.SaveCounters(Counters);
            }
        }

        // Obsługa kliknięcia przycisku "+"
        private void OnPlusClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var counter = button.CommandParameter as Counter;

            if (counter != null)
            {
                counter.Value++;
                _counterService.SaveCounters(Counters);
            }
        }

        // Obsługa resetowania licznika
        private void OnResetClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var counter = button.CommandParameter as Counter;

            if (counter != null)
            {
                counter.Reset();
                _counterService.SaveCounters(Counters);
            }
        }
    }
}
