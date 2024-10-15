namespace Liczniki.Models
{
    public class Counter
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public int InitialValue { get; set; }

        public Counter(string name, int initialValue)
        {
            Name = name;
            InitialValue = initialValue;
            Value = initialValue;
        }

        public void Reset()
        {
            Value = InitialValue;
        }
    }
}