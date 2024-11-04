using System;
using System.ComponentModel;

namespace Liczniki.Models
{
    public class Counter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _name;
        private int _value;
        private int _initialValue;

        // Unikalne ID dla każdego obiektu
        public Guid Id { get; private set; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public int InitialValue
        {
            get => _initialValue;
            set
            {
                _initialValue = value;
                OnPropertyChanged(nameof(InitialValue));
            }
        }

        // Konstruktor z generowaniem unikalnego ID
        public Counter(string name, int initialValue)
        {
            Id = Guid.NewGuid(); // Generuje unikalne ID przy tworzeniu obiektu
            Name = name;
            InitialValue = initialValue;
            Value = initialValue;
        }

        // Konstruktor bezparametrowy dla deserializacji
        public Counter()
        {
            Id = Guid.NewGuid(); // Generuje unikalne ID przy tworzeniu obiektu
        }

        public void Reset()
        {
            Value = InitialValue;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
