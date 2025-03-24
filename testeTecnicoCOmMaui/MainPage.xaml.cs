using System;
using System.IO;
using System.Text.Json;
using Microsoft.Maui.Controls;

namespace testeTecnicoCOmMaui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCalcularClicked(object sender, EventArgs e)
        {
            int INDICE = 13, SOMA = 0;

            for (int K = 1; K <= INDICE; K++)
            {
                SOMA += K;
            }

            lblResultado.Text = $"O valor final da variável SOMA é: {SOMA}";
        }

        private void OnVerificarFibonacciClicked(object sender, EventArgs e)
        {
            if (int.TryParse(txtNumero.Text, out int numero))
            {
                if (EhFibonacci(numero))
                {
                    lblFibonacci.Text = $"O número {numero} pertence à sequência de Fibonacci!";
                    lblFibonacci.TextColor = Colors.Green;
                }
                else
                {
                    lblFibonacci.Text = $"O número {numero} NÃO pertence à sequência de Fibonacci!";
                    lblFibonacci.TextColor = Colors.Red;
                }
            }
           
        }

        private bool EhFibonacci(int num)
        {
            if (num == 0 || num == 1) return true;

            int a = 0, b = 1, soma = 0;

            for (; soma < num; a = b, b = soma)
            {
                soma = a + b;
            }

            return soma == num;
        }

        // Função para calcular o faturamento
        private async void OnCalcularFaturamentoClicked(object sender, EventArgs e)
        {
            // Caminho do arquivo JSON (ajuste conforme necessário)
            string jsonFilePath = @"C:\Users\R\source\repos\testeTecnicoCOmMaui\dados.json";

            // Verificando se o arquivo existe
            if (File.Exists(jsonFilePath))
            {
                // Chamando a função de processamento de faturamento
                var resultado = Faturamento.ProcessarFaturamento(jsonFilePath);

                // Exibindo os resultados
                lblFaturamentoMenor.Text = $"Menor valor de faturamento: {resultado.menor:C}";
                lblFaturamentoMaior.Text = $"Maior valor de faturamento: {resultado.maior:C}";
                lblDiasAcimaMedia.Text = $"Dias acima da média: {resultado.diasAcimaMedia}";
            }
            else
            {
                lblFaturamentoMenor.Text = "Arquivo de faturamento não encontrado!";
                lblFaturamentoMaior.Text = string.Empty;
                lblDiasAcimaMedia.Text = string.Empty;
            }
        }

        static string InverterString(string str)
        {
            char[] invertido = new char[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                invertido[i] = str[str.Length - 1 - i];
            }

            return new string(invertido);
        }

        private void OnInverterTextoClicked(object sender, EventArgs e)
        {
            string texto = txtTexto.Text;

            if (!string.IsNullOrEmpty(texto))
            {
                string textoInvertido = InverterString(texto);
                lblTextoInvertido.Text = $"Texto invertido: {textoInvertido}";
            }
            else
            {
                lblTextoInvertido.Text = "Por favor, digite um texto.";
            }
        }
    }
}
