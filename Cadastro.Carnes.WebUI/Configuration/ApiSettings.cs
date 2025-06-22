namespace Cadastro.Carnes.WebUI.Configuration
{
    /// <summary>
    /// Representa as configurações da API externa usadas no sistema.
    /// </summary>
    public class ApiSettings
    {
        /// <summary>
        /// URL base para chamadas à API (exemplo: http://localhost:5081/api).
        /// </summary>
        public string BaseUrl { get; set; } = string.Empty;
    }
}
