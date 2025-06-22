using System.Text.Json;

namespace Cadastro.Carnes.WebUI.Helpers
{
    public static class JsonHelper
    {
        // Método que extrai a mensagem de erro de uma string JSON
        public static string GetMessage(string json)
        {
            try
            {
                // Tenta desserializar o JSON para o objeto Resposta
                var errorObj = JsonSerializer.Deserialize<Resposta>(json);

                // Retorna a mensagem contida no objeto ou mensagem padrão caso seja nula
                return errorObj?.mensagem ?? "Erro ao extrair mensagem.";
            }
            catch
            {
                // Retorna mensagem padrão caso ocorra algum erro no processamento do JSON
                return "Erro ao processar JSON.";
            }
        }

        // Classe interna que representa a estrutura esperada do JSON de resposta de erro
        private class Resposta
        {
            // Indica se a operação foi bem sucedida (não utilizado diretamente aqui)
            public bool sucesso { get; set; }

            // Mensagem de erro ou informação extraída do JSON
            public string mensagem { get; set; } = string.Empty;
        }
    }
}
