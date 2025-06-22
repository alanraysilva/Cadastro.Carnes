using System.Text.Json;

namespace Cadastro.Carnes.WebUI.Helpers
{
    public static class JsonHelper
    {
        public static string GetMessage(string json)
        {
            try
            {
                var errorObj = JsonSerializer.Deserialize<Resposta>(json);
                return errorObj?.mensagem ?? "Erro ao extrair mensagem.";
            }
            catch
            {
                return "Erro ao processar JSON.";
            }
        }

        private class Resposta
        {
            public bool sucesso { get; set; }
            public string mensagem { get; set; } = string.Empty;
        }
    }
}