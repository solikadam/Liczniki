using System;
using System.Collections.ObjectModel;
using System.IO;
using Liczniki.Models;

namespace Liczniki.Services
{
    public class CounterService
    {
        private string filePath;

        public CounterService()
        {
            // Ścieżka do pliku tekstowego
            filePath = Path.Combine(FileSystem.AppDataDirectory, "counters.txt");
        }

        // Zapis liczników do pliku
        public void SaveCounters(ObservableCollection<Counter> counters)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                foreach (var counter in counters)
                {
                    writer.WriteLine($"{counter.Name};{counter.Value};{counter.InitialValue}");
                }
            }
        }

        // Odczyt liczników z pliku
        public ObservableCollection<Counter> LoadCounters()
        {
            var counters = new ObservableCollection<Counter>();

            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(';');
                    if (parts.Length == 3 && int.TryParse(parts[1], out int value) && int.TryParse(parts[2], out int initialValue))
                    {
                        counters.Add(new Counter(parts[0], initialValue) { Value = value });
                    }
                }
            }

            return counters;
        }
    }
}

