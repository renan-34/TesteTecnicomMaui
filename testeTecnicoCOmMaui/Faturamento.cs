using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace testeTecnicoCOmMaui
{
    class Faturamento
    {
        public int Dia { get; set; }
        public double Valor { get; set; }

        public static (double menor, double maior, int diasAcimaMedia) ProcessarFaturamento(string caminhoArquivo)
        {
            try
            {
                // Lendo o JSON do arquivo
                string jsonContent = File.ReadAllText(caminhoArquivo);

                Console.WriteLine("JSON Lido: " + jsonContent); // Verifica o conteúdo do JSON

                // Desserializando o JSON corretamente
                List<FaturamentoDia> faturamentos = JsonSerializer.Deserialize<List<FaturamentoDia>>(jsonContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // Filtrando os dias úteis (faturamento maior que zero)
                List<FaturamentoDia> diasUteis = new List<FaturamentoDia>();

                foreach (var faturamento in faturamentos)
                {
                    if (faturamento.Valor > 0)
                    {
                        diasUteis.Add(faturamento);
                    }
                }
                
                if (diasUteis.Count == 0)
                    return (0, 0, 0);
                
                double menorFaturamento = diasUteis[0].Valor;
                double maiorFaturamento = diasUteis[0].Valor;
                double somaValores = 0;

                foreach (var faturamento in diasUteis)
                {
                    if (faturamento.Valor < menorFaturamento)
                    {
                        menorFaturamento = faturamento.Valor;
                    }
                    if (faturamento.Valor > maiorFaturamento)
                    {
                        maiorFaturamento = faturamento.Valor;
                    }
                    somaValores += faturamento.Valor;
                }

                double media = somaValores / diasUteis.Count;
                int diasAcimaMedia = 0;

                foreach (var faturamento in diasUteis)
                {
                    if (faturamento.Valor > media)
                    {
                        diasAcimaMedia++;
                    }
                }

                return (menorFaturamento, maiorFaturamento, diasAcimaMedia);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar faturamento: {ex.Message}");
                return (0, 0, 0);
            }
        }
    }

    public class FaturamentoDia
    {
        [JsonPropertyName("dia")]
        public int Dia { get; set; }

        [JsonPropertyName("valor")]
        public double Valor { get; set; }
    }
}
