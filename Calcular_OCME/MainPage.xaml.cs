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
        
        public string CI = "";

        public MainPage()
        {
            InitializeComponent();
        }

        
        private void Activar(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Text;

            
            if (buttonText != "=" && buttonText != "C" && buttonText != "Delete")
            {
               
                if (buttonText == "." && CI.Contains("."))
                {
                    return;
                }

                
                CI += buttonText;
                entrada.Text = CI;
            }
        }

        
        private void Borrar(object sender, EventArgs e)
        {
            if (CI.Length > 0)
            {
                
                CI = CI.Substring(0, CI.Length - 1);
                entrada.Text = CI;
            }
        }

        
        private void Limpiar(object sender, EventArgs e)
        {
            CI = "";
            entrada.Text = CI;
        }

        
        private void Igual(object sender, EventArgs e)
        {
            try
            {
                
                if (string.IsNullOrEmpty(CI))
                {
                    entrada.Text = "Error";
                    return;
                }

               
                CI = CI.Replace("×", "*").Replace("÷", "/");

                
                var result = Evaluar(CI);

                
                entrada.Text = result.ToString();
                CI = result.ToString(); 
            }
            catch (Exception)
            {
                entrada.Text = "Error";
                CI = "";
            }
        }

       
        private double Evaluar(string expresion)
        {
           
            if (expresion.Contains(".."))
            {
                throw new Exception("Formato incorrecto");
            }

            
            var dataTable = new System.Data.DataTable();
            return Convert.ToDouble(dataTable.Compute(expresion, string.Empty));
        }

    }
}
