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
            // Caminho do arquivo JSON 
            string jsonFilePath = @"C:\Users\R\source\repos\testeTecnicoCOmMaui\dados.json";

            // Verificando se o arquivo existe
            if (File.Exists(jsonFilePath))
            {
                // Chamando a função de processamento de faturamento
                var resultado = Faturamento.ProcessarFaturamento(jsonFilePath);

                
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



        private void OnCalcularPercentualClicked(object sender, EventArgs e)
        {
            var faturamentoEstados = new List<(string Estado, double Faturamento)>
            {
                ("SP", 67836.43),
                ("RJ", 36678.66),
                ("MG", 29229.88),
                ("ES", 27165.48),
                ("Outros", 19849.53)
            };

            // Calculando o total de faturamento
            double totalFaturamento = 0;
            foreach (var estado in faturamentoEstados)
            {
                totalFaturamento += estado.Faturamento;
            }

            // Calculando o percentual de cada estado
            lblFaturamentoSP.Text = $"SP: {CalcularPercentual(faturamentoEstados[0].Faturamento, totalFaturamento)}%";
            lblFaturamentoRJ.Text = $"RJ: {CalcularPercentual(faturamentoEstados[1].Faturamento, totalFaturamento)}%";
            lblFaturamentoMG.Text = $"MG: {CalcularPercentual(faturamentoEstados[2].Faturamento, totalFaturamento)}%";
            lblFaturamentoES.Text = $"ES: {CalcularPercentual(faturamentoEstados[3].Faturamento, totalFaturamento)}%";
            lblFaturamentoOutros.Text = $"Outros: {CalcularPercentual(faturamentoEstados[4].Faturamento, totalFaturamento)}%";
        }

        private string CalcularPercentual(double faturamento, double totalFaturamento)
        {
            double percentual = (faturamento / totalFaturamento) * 100;
            return percentual.ToString("F2");
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