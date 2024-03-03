using System;
using Xamarin.Forms;

namespace MaricosApp.Tools
{
    public partial class NumericUpDown : ContentView
    {
        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value), typeof(int), typeof(NumericUpDown), 1,
                propertyChanged: (bindable, oldValue, newValue) =>
                    ((NumericUpDown)bindable).ValueText.Text = newValue.ToString(),
                defaultBindingMode: BindingMode.TwoWay);

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly BindableProperty MinimumProperty =
            BindableProperty.Create(nameof(Minimum), typeof(int), typeof(NumericUpDown), 1);

        public int Minimum
        {
            get => (int)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        public static readonly BindableProperty MaximumProperty =
            BindableProperty.Create(nameof(Maximum), typeof(int), typeof(NumericUpDown), 10);

        public int Maximum
        {
            get => (int)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        public static readonly BindableProperty StepProperty =
            BindableProperty.Create(nameof(Step), typeof(int), typeof(NumericUpDown), 1);

        public int Step
        {
            get => (int)GetValue(StepProperty);
            set => SetValue(StepProperty, value);
        }

        public NumericUpDown()
        {
            InitializeComponent();
            // Configura el valor inicial
            ValueText.Text = Value.ToString();
        }

        private void MinusTapped(object sender, EventArgs e)
        {
            // Implementa la lógica para decrementar el valor
            if (Value > Minimum)
            {
                Value = Math.Max(Minimum, Value - Step);
            }
        }

        private void PlusTapped(object sender, EventArgs e)
        {
            // Implementa la lógica para incrementar el valor
            if (Value < Maximum)
            {
                Value = Math.Min(Maximum, Value + Step);
            }
        }
    }
}
