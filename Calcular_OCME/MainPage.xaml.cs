using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calcular_OCME
{
    public partial class MainPage : ContentPage
    {
        // Cambié 'currentInput' por 'CI' para que coincida con tu solicitud
        public string CI = "";

        public MainPage()
        {
            InitializeComponent();
        }

        // Cambié el nombre de 'OnButtonClicked' a 'Activar'
        private void Activar(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Text;

            // Si el botón no es igual (=), borrar (C) ni eliminar (Delete)
            if (buttonText != "=" && buttonText != "C" && buttonText != "Delete")
            {
                // Si el botón es un punto (.) y ya contiene un punto, no lo añadimos
                if (buttonText == "." && CI.Contains("."))
                {
                    return;
                }

                // Agregar el texto del botón al input actual (CI)
                CI += buttonText;
                entrada.Text = CI;
            }
        }

        // Cambié el nombre de 'OnDeleteClicked' a 'Borrar'
        private void Borrar(object sender, EventArgs e)
        {
            if (CI.Length > 0)
            {
                // Eliminar el último carácter ingresado
                CI = CI.Substring(0, CI.Length - 1);
                entrada.Text = CI;
            }
        }

        // Cambié el nombre de 'OnClearClicked' a 'Limpiar'
        private void Limpiar(object sender, EventArgs e)
        {
            CI = "";
            entrada.Text = CI;
        }

        // Cambié el nombre de 'OnEqualsClicked' a 'Igual'
        private void Igual(object sender, EventArgs e)
        {
            try
            {
                // Validar que la entrada no esté vacía
                if (string.IsNullOrEmpty(CI))
                {
                    entrada.Text = "Error";
                    return;
                }

                // Reemplazar los operadores de la calculadora
                CI = CI.Replace("×", "*").Replace("÷", "/");

                // Evaluar la expresión
                var result = Evaluar(CI);

                // Mostrar el resultado en la pantalla
                entrada.Text = result.ToString();
                CI = result.ToString(); // Actualizamos la entrada con el resultado para nuevas operaciones
            }
            catch (Exception)
            {
                entrada.Text = "Error";
                CI = "";
            }
        }

        // Cambié el nombre de 'EvaluateExpression' a 'Evaluar'
        private double Evaluar(string expresion)
        {
            // Validar que no haya dos puntos consecutivos
            if (expresion.Contains(".."))
            {
                throw new Exception("Formato incorrecto");
            }

            // Evaluar la expresión usando System.Data (esto evalúa expresiones matemáticas)
            var dataTable = new System.Data.DataTable();
            return Convert.ToDouble(dataTable.Compute(expresion, string.Empty));
        }

    }
}
