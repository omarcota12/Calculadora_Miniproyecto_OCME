using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Calcular_OCME.ModelView
{
    public class CalculadoraViewModel : INotifyPropertyChanged
    {
        private string _display;
        private string _firstOperand;
        private string _secondOperand;
        private string _operation;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Display
        {
            get { return _display; }
            set
            {
                if (_display != value)
                {
                    _display = value;
                    OnPropertyChanged(nameof(Display));
                }
            }
        }

        public ICommand NumeroCommand { get; }
        public ICommand OperacionCommand { get; }
        public ICommand LimpiarCommand { get; }
        public ICommand CalcularCommand { get; }

        public CalculadoraViewModel()
        {
            _firstOperand = string.Empty;
            _secondOperand = string.Empty;
            _operation = string.Empty;

            Display = "0"; // Inicializar la pantalla

            NumeroCommand = new Command<string>(BTpresionar);
            OperacionCommand = new Command<string>(Operacion);
            LimpiarCommand = new Command(LimpiarP);
            CalcularCommand = new Command(OnCalculatePressed);
        }

        private void BTpresionar(string number)
        {
            if (_operation == string.Empty)
            {
                _firstOperand += number;
                Display = _firstOperand;
            }
            else
            {
                _secondOperand += number;
                Display = _secondOperand;
            }
        }

        private void Operacion(string operation)
        {
            if (_firstOperand != string.Empty)
            {
                _operation = operation;
            }
        }

        private void LimpiarP()
        {
            _firstOperand = string.Empty;
            _secondOperand = string.Empty;
            _operation = string.Empty;
            Display = "0";
        }

        private void OnCalculatePressed()
        {
            if (!string.IsNullOrEmpty(_firstOperand) && !string.IsNullOrEmpty(_secondOperand) && !string.IsNullOrEmpty(_operation))
            {
                double num1 = Convert.ToDouble(_firstOperand);
                double num2 = Convert.ToDouble(_secondOperand);
                double result = 0;

                switch (_operation)
                {
                    case "+":
                        result = num1 + num2;
                        break;
                    case "-":
                        result = num1 - num2;
                        break;
                    case "*":
                        result = num1 * num2;
                        break;
                    case "/":
                        if (num2 != 0)
                            result = num1 / num2;
                        break;
                }

                Display = result.ToString();
                _firstOperand = result.ToString();
                _secondOperand = string.Empty;
                _operation = string.Empty;
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

